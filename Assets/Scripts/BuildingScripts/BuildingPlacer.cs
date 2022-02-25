using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public GameObject placementIndicator;
    [SerializeField] private PlacementManager placementManager;

    public static BuildingPlacer inst;
    private bool currentlyPlacing;
    private BuildingPreset curBuildingPreset;
    private float placementIndicatorUpdateRate = 0.05f;
    private float lastUpdateTime;
    private Vector3 curPlacementPos;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool firstClick = true;
    private List<Vector3Int> temporaryPlacementPositions = new List<Vector3Int>();
    private Vector3Int startingPosition;

    void Awake()
    {
        inst = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            CancelBuildingPlacement();

        if (Time.time - lastUpdateTime > placementIndicatorUpdateRate && currentlyPlacing)
        {
            lastUpdateTime = Time.time;
            curPlacementPos = Selector.inst.GetCurTilePosition();
            placementIndicator.transform.position = curPlacementPos;
        }

        /*

        if (currentlyPlacing && Input.GetMouseButtonDown(0))
        {
            if (!curBuildingPreset.allowMultiple)
            {
                print("ne dozvolja se vise ovakvih zgrada "+curBuildingPreset.allowMultiple+" "+curBuildingPreset.name);
                PlaceBuilding();
            }
            else
            {
                print("Usao u ovu drugu metodu ");
                //CancelBuildingPlacement();
                if (firstClick)
                {
                    // Record starting position
                    startPosition = curPlacementPos;
                    firstClick = false;
                }
                else
                {
                    endPosition = curPlacementPos;
                    //PlacementManager.GetPath();

                    //CancelBuildingPlacement(startPosition, endPosition);
                    //PlaceBuilding();
                }

                // Record end position
                //Debug.Log(curPlacementPos);
            }

        }
        */
    }

    public void DraggingHouses(Vector3 pos)
    {
        placementManager.RemoveAllTemporaryStructures();
        temporaryPlacementPositions.Clear();
        MakeAListBetweenPositions(startingPosition,new Vector3Int((int) pos.x,0,(int) pos.z));

        foreach (var temporaryPosition in temporaryPlacementPositions)
        {
            if (placementManager.CheckIfPositionIsFree(temporaryPosition) == false)
            {
                continue;
            }
            placementManager.PlaceTemporaryStructure(temporaryPosition, curBuildingPreset.prefab, CellType.Structure);
        }
    }

    private void MakeAListBetweenPositions(Vector3Int start, Vector3Int end)
    {
        var xDifference = start.x - end.x;
        var xAbsolute = Mathf.Abs(xDifference);
        var zDifference = start.z - end.z;
        var zAbsolute = Math.Abs(zDifference);
        var xFactor = 1;
        if (xDifference < 0)  xFactor = -1;
        var zFactor = 1;
        if (zDifference < 0)  zFactor = -1;

        for (int i = 0; i < xAbsolute + 1; i++)
        {
            for (int j = 0; j < zAbsolute + 1; j++)
            {
                temporaryPlacementPositions.Add(new Vector3Int(start.x - i*xFactor, 0, start.z - j*zFactor));
            }
        }
    }

    public void BeginNewBuildingPlacement(BuildingPreset buildingPreset, Vector3Int pos)
    {
        if (City.inst.money < buildingPreset.cost) return;
        currentlyPlacing = true;
        curBuildingPreset = buildingPreset;
        placementIndicator.SetActive(true);
        startingPosition = pos;
    }

    public void CancelBuildingPlacement()
    {
        print("kanselujem building placement");
        currentlyPlacing = false;
        placementIndicator.SetActive(false);
    }

    void PlaceBuilding()
    {
        print("postavljam zgradu ");
        GameObject buildingObj = Instantiate(curBuildingPreset.prefab, curPlacementPos, Quaternion.identity);
        City.inst.OnPlaceBuilding(curBuildingPreset);
        CancelBuildingPlacement();
    }
}
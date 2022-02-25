using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Source https://github.com/SunnyValleyStudio/SimpleCityBuilder

public class InputManager : MonoBehaviour
{
	public LayerMask groundMask;
	[SerializeField] private Camera mainCamera;
	[SerializeField] private RoadManager roadManager;
	[SerializeField] private StructureManager structureManager;
	[SerializeField] private GameManager gameManager;
	[SerializeField] private BuildingPlacer buildingPlacer;
	
	private Vector2 cameraMovementVector;
	public Vector2 CameraMovementVector
	{
		get { return cameraMovementVector; }
	}

	private void Update()
	{
		CheckClickDownEvent();
		CheckClickUpEvent();
		CheckClickHoldEvent();
		CheckArrowInput();
	}

	public Vector3Int? RaycastGround()
	{
		RaycastHit hit;
		Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
		{
			Vector3Int positionInt = Vector3Int.RoundToInt(hit.point);
			return positionInt;
		}
		return null;
	}

	private void CheckArrowInput()
	{
		cameraMovementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
	}

	private void CheckClickHoldEvent()
	{
		if (!Input.GetMouseButton(0)) return;
		if (GameManager.instance.GetGameState()== GameManager.GameState.RoadBuilding && EventSystem.current.IsPointerOverGameObject() == false)
		{
			var position = RaycastGround();
			if (position != null)
			{
				gameManager.ClearInputActions();
				var pos = (Vector3Int) position;
				roadManager.PlaceRoad(pos);
			}
		}
		if (GameManager.instance.GetGameState()== GameManager.GameState.HouseBuilding && EventSystem.current.IsPointerOverGameObject() == false)
		{
			var position = RaycastGround();
			if (position != null)
			{
				gameManager.ClearInputActions();
				var pos = (Vector3Int) position;
				buildingPlacer.DraggingHouses(pos);
			}
		}
	}

	private void CheckClickUpEvent()
	{
		if (!Input.GetMouseButtonUp(0)) return;
		if (GameManager.instance.GetGameState()== GameManager.GameState.RoadBuilding)
		{
			gameManager.ClearInputActions();
			roadManager.FinishPlacingRoad();
		}
	}

	private void CheckClickDownEvent()
	{
		if (!Input.GetMouseButtonDown(0)) return;
		if (GameManager.instance.GetGameState()== GameManager.GameState.HouseBuilding )
		{
			var position = RaycastGround();
			if (position != null)
			{
				BuildingPlacer.inst.BeginNewBuildingPlacement(City.inst.buildings[0],(Vector3Int) position);
			}
		}
	}
}

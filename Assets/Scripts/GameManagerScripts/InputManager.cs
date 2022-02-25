using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Source https://github.com/SunnyValleyStudio/SimpleCityBuilder

public class InputManager : MonoBehaviour
{
	public Action<Vector3Int> OnMouseClick, OnMouseHold;
	public Action OnMouseUp;
	private Vector2 cameraMovementVector;

	[SerializeField] private Camera mainCamera;
	[SerializeField] private RoadManager roadManager;
	[SerializeField] private StructureManager structureManager;
	[SerializeField] private GameManager gameManager;

	public LayerMask groundMask;

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
		if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false)
		{
			var position = RaycastGround();
			if (position != null)
			{
				gameManager.ClearInputActions();
				var pos = (Vector3Int) position;
				roadManager.PlaceRoad(pos);
			}

		}
	}

	private void CheckClickUpEvent()
	{
		//&& EventSystem.current.IsPointerOverGameObject() == false
		if (Input.GetMouseButtonUp(0) )
		{
			gameManager.ClearInputActions();
			roadManager.FinishPlacingRoad();
			
			//OnMouseUp?.Invoke();

			//print("saljem event drzanja misa");
		}
	}

	private void CheckClickDownEvent()
	{
		//&& EventSystem.current.IsPointerOverGameObject() == false
		if (Input.GetMouseButtonDown(0) )
		{
			var position = RaycastGround();
			if (position != null)
			{
				gameManager.ClearInputActions();
				var pos = (Vector3Int) position;
				structureManager.PlaceSpecial(pos);
				structureManager.PlaceHouse(pos);
			}
				
			//print("saljem event drzanja misa " + position);
		}
	}
}

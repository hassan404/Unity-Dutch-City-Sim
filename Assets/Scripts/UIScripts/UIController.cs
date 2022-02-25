using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	public Button placeRoadButton, placeHouseButton, placeSpecialButton;

	public Color outlineColor;
	List<Button> buttonList;

	private void Start()
	{
		buttonList = new List<Button> { placeHouseButton, placeRoadButton, placeSpecialButton };

		placeRoadButton.onClick.AddListener(() =>
		{
			GameManager.instance.ClearInputActions();
			GameManager.instance.UpdateGameState(GameManager.GameState.RoadBuilding);

		});
		placeHouseButton.onClick.AddListener(() =>
		{
			GameManager.instance.ClearInputActions();
			GameManager.instance.UpdateGameState(GameManager.GameState.HouseBuilding);

		});
		placeSpecialButton.onClick.AddListener(() =>
		{
			

		});
	}

	private void ModifyOutline(Button button)
	{
		/*var outline = button.GetComponent<Outline>();
		outline.effectColor = outlineColor;
		outline.enabled = true;*/
	}

	private void ResetButtonColor()
	{
		/*foreach (Button button in buttonList)
		{
			button.GetComponent<Outline>().enabled = false;
		}*/
	}
}

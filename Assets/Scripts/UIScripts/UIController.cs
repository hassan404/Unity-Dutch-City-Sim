using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	public Button placeRoadButton, placeHouseButton;
	public Button[] singleStructures;

	public Color outlineColor;
	List<Button> buttonList;

	private void Start()
	{
		buttonList = new List<Button> { placeHouseButton, placeRoadButton};

		placeRoadButton.onClick.AddListener(() =>
		{
			GameManager.instance.ClearInputActions();
			GameManager.instance.UpdateGameState(GameManager.GameState.RoadBuilding);
			City.inst.indexOfSelectedBuilding = 0;

		});
		placeHouseButton.onClick.AddListener(() =>
		{
			GameManager.instance.ClearInputActions();
			GameManager.instance.UpdateGameState(GameManager.GameState.HouseBuilding);
			City.inst.indexOfSelectedBuilding = 1;

		});
		var counter = 0;
		for (int i = 0; i < singleStructures.Length; i++)
		{
			var counter1 = counter;
			singleStructures[i].onClick.AddListener(() =>
			{
				GameManager.instance.ClearInputActions();
				GameManager.instance.UpdateGameState(GameManager.GameState.SingleStructure);
				City.inst.indexOfSelectedBuilding = counter1 + 2;
			});
			counter++;
		}
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

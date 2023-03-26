using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
     
public class CanvasController : MonoBehaviour
{
	public static CanvasController Instance;

	[SerializeField]
	private CanvasControllerData _properties;
	
	void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else if(Instance != this)
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		SetUndoButton();
		SetDoneButton(AnswerHandler.IsStringInList(""));
	}

	public void SetLevelTitle(string title)
	{
		_properties.LevelTitleText.text = title;
	}

	public void SetUndoButton()
	{
		_properties.UndoButtonObject.SetActive(PlayerController.Instance.CanUndo);
	}

	public void UndoButtonDown()
	{
		//Debug.LogError("Undo");
		PlayerController.Instance.UndoPlacedObject();
	}

	public void SetDoneButton(bool value)
	{
		_properties.DoneButtonObject.SetActive(value);
	}

	public void DoneButtonDown()
	{
		PlayerController.Instance.DoneButtonDown();
	}
}

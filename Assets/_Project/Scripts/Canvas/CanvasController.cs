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

	private List<string> _correctWords;

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
		
		_correctWords = new List<string>();
		_correctWords.Add("Answers");	
		_properties.CorrectAnswersText.text = "Answers";
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

	public void SetTrigger(string trigger)
	{
		_properties.MyAnimator.SetTrigger(trigger);
	}

	public void AddCorrectWord(string word)
	{
		if(_correctWords.Contains(word)) return;

		_correctWords.Add(word);
		string text = "";
		foreach (string correctWord in _correctWords)
		{
			text += correctWord + "\n";
		}

		_properties.CorrectAnswersText.text = text;
	}

	public void NextLevelButtonDown()
	{
		if(GameManager.Instance.GameState!=GameState.Won) return;
		
		SetTrigger("NextLevel");
		GameManager.Instance.StartGame(SaveManager.Instance.GameSaveState.LastLevel);
	}
}

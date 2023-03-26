using System;
using System.Collections;
using System.Collections.Generic;
using LeonBrave;
using UnityEngine;
     
public class PlayerController : MonoBehaviour
{
	public static PlayerController Instance;

	private List<TileObject> _placedObjects;

	private string _answerString;
	
		
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

		_placedObjects = new List<TileObject>();
		_answerString = "";
	}
	
	private void Start()
	{
		LeonBrave.UserInput.Instance.TouchEvent += Touch;
	}

	public bool CanUndo
	{
		get
		{
			return _placedObjects.Count > 0;
		}
	}

	public void AddPlacedObject(TileObject tileObject)
	{
		if(_placedObjects.Contains(tileObject) ) return;
		_placedObjects.Add(tileObject);
		CanvasController.Instance.SetUndoButton();

		_answerString += tileObject.TileChar;
		SetAnswerString();
	}

	public void UndoPlacedObject()
	{
		if(_placedObjects.Count<=0) return;
		
		_placedObjects[^1].TileObjectState = TileObjectState.Selectable;
		_placedObjects[^1].TouchUp();
		_placedObjects.RemoveAt(_placedObjects.Count-1);

		PlacementTrigger.Instance.PlacedIndex--;
		CanvasController.Instance.SetUndoButton();
		
		_answerString = _answerString.Remove(_answerString.Length - 1);
		SetAnswerString();
	}

	private TileObject _selectedTileObject;

	public TileObject SelectedTileObject
	{
		set
		{
			_selectedTileObject = value;
		}
	}
	
	private void Touch(LeonBrave.UserInput.TouchType touchType)
	{
		if( GameManager.Instance.GameState!=GameState.Playing || _selectedTileObject==null || touchType!=UserInput.TouchType.Up) return;
		
		_selectedTileObject.TouchUp();
		_selectedTileObject = null;
		DragDrop.Instance.CanDrag = true;
	}

	public void SetAnswerString()
	{
		CanvasController.Instance.SetDoneButton(AnswerHandler.IsStringInList(_answerString));

	}

	public void DoneButtonDown()
	{
		foreach (TileObject placedObject in _placedObjects)
		{
			placedObject.BlowYourSelf();
		}
		
		_placedObjects.Clear();

		PlacementTrigger.Instance.PlacedIndex = -1;
		
	    CanvasController.Instance.AddCorrectWord(_answerString);
	    CanvasController.Instance.SetUndoButton();
		CanvasController.Instance.SetDoneButton(false);
		
		Debug.Log(_answerString);
		ScoreManager.Instance.SetScore(_answerString);
		ScoreManager.Instance.SaveScore();
		
		GameManager.Instance.CheckIsGameEnd();
		
		_answerString = "";
	}


}

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

		_answerString += char.ToLower(tileObject.TileChar);;
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

	private void Touch(LeonBrave.UserInput.TouchType touchType)
	{
		if(_selectedTileObject==null || touchType!=UserInput.TouchType.Up) return;
		
		_selectedTileObject.TouchUp();
		_selectedTileObject = null;
		DragDrop.Instance.CanDrag = true;
	}

	public void SetAnswerString()
	{
		CanvasController.Instance.SetDoneButton(AnswerHandler.IsStringInList(_answerString));

	}


}

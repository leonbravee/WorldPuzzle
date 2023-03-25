using System;
using System.Collections;
using System.Collections.Generic;
using LeonBrave;
using UnityEngine;
     
public class PlayerController : MonoBehaviour
{
	public static PlayerController Instance;

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
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
     
public class SaveManager : MonoBehaviour
{
	public static SaveManager Instance;

	private GameSaveState _gameSaveState;

	public GameSaveState GameSaveState
	{
		get
		{
			return _gameSaveState;
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

		_gameSaveState = new GameSaveState();
		_gameSaveState.SetSaveState();
	}
	
	
	
}


using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class TileBuilder : MonoBehaviour
{
	public static TileBuilder Instance;
	
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
	[SerializeField]
	private LevelData _levelData;

	private List<TileObject> _tileObjects;

	private void Start()
	{
		_tileObjects = new List<TileObject>();
		string path = "Levels/level_1";
		TextAsset jsonFile = Resources.Load<TextAsset>(path);
        
		_levelData = JsonConvert.DeserializeObject<LevelData>(jsonFile.text);
		
		
		CanvasController.Instance.SetLevelTitle(_levelData.title);

		foreach (TileLevelData tileLevelData in _levelData.tiles)
		{
			TileObject tileObjectTemp = TileObjectPool.Instance.TakeObject();
			tileObjectTemp.TileLevelData = tileLevelData;
			_tileObjects.Add(tileObjectTemp);
			tileObjectTemp.transform.parent = transform;
		}
		foreach (TileObject tileObject in _tileObjects)
		{
			if (tileObject.ChildrenIds.Count > 0)
			{
				foreach (int childrenId in tileObject.ChildrenIds)
				{
					GetTileObjectFromId(childrenId).TileObjectState = TileObjectState.UnSelectable;
				}
			}
		}
		foreach (TileObject tileObject in _tileObjects)
		{
			if (tileObject.TileObjectState == TileObjectState.None)
			{
				tileObject.TileObjectState = TileObjectState.Selectable;
			}
			tileObject.gameObject.SetActive(true);
		}
		AnswerHandler.BuildAnswerLibrary();
        
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Debug.LogError(AnswerHandler.IsStringInList("cable"));
		}
		else if (Input.GetKeyDown(KeyCode.W))
		{
			Debug.LogError(AnswerHandler.IsStringInList("water"));
		}
		else if (Input.GetKeyDown(KeyCode.S))
		{
			Debug.LogError(AnswerHandler.IsStringInList("expensive"));
		}
	}

	public TileObject GetTileObjectFromId(int id)
	{
		foreach (TileObject tileObject in _tileObjects)
		{
			if (id == tileObject.TileId)
			{
				return tileObject;
			}
		}

		return null;
	}
}

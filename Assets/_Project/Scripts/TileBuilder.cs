using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class TileBuilder : MonoBehaviour
{
    public static TileBuilder Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private LevelData _levelData;

    private List<TileObject> _tileObjects;

    private void Start()
    {
      BuildLevel(1);
    }

    private void BuildLevel(int levelIndex)
    {
        _tileObjects = new List<TileObject>();
        string path = "Levels/level_"+levelIndex;
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
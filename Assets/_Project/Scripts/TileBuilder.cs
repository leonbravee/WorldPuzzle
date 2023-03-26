using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class TileBuilder : MonoBehaviour
{
    
    /*
     * Json dosyasından gelen veriye level içerisindeki tile'ların oluşturulması.
     * Levelde kullanılan tilelar üzerinde yapılan işlemler için fonsiyonlar
     */
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
    
    public void BuildLevel(int levelIndex)
    {
        if ( _tileObjects!=null && _tileObjects.Count > 0)
        {
            foreach (TileObject tileObject in _tileObjects)
            {
                Destroy(tileObject);
            }
        }
        levelIndex++;
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

    public char[] GetUseFulTiles()
    {
        List<char> usefulChars = new List<char>();
        foreach (TileObject tileObject in _tileObjects)
        {
            if (tileObject.TileObjectState == TileObjectState.Selectable)
            {
                usefulChars.Add(tileObject.TileChar);
            }
        }

        return usefulChars.ToArray();
    }
    public char[] GetUnUsedTiles()
    {
        List<char> usefulChars = new List<char>();
        foreach (TileObject tileObject in _tileObjects)
        {
            if (tileObject.TileObjectState == TileObjectState.Selectable || tileObject.TileObjectState==TileObjectState.UnSelectable)
            {
                usefulChars.Add(tileObject.TileChar);
            }
        }

        return usefulChars.ToArray();
    }

    public void AutoMove(string answer)
    {
        for (var i = 0; i < answer.Length; i++)
        {
           FindForBlow(answer[i]);
        }
        PlayerController.Instance.DoneButtonDown();
    }

    private void FindForBlow(char answer)
    {
        for (var j = 0; j < _tileObjects.Count; j++)
        {
            if (_tileObjects[j].TileObjectState == TileObjectState.Selectable && _tileObjects[j].TileChar ==answer)
            {
                PlayerController.Instance.AddPlacedObject(_tileObjects[j]);
                //Debug.Log(_tileObjects[j].gameObject.name);
                break;
            }
        }
      
    }
    
    
}
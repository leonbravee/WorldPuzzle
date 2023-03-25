using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class Test : MonoBehaviour
{


    [SerializeField]
    private LevelData _levelData;

    private void Start()
    {

        string path = "Levels/level_1";
        TextAsset jsonFile = Resources.Load<TextAsset>(path);
        
         _levelData = JsonConvert.DeserializeObject<LevelData>(jsonFile.text);

    
      /*  Debug.Log("Title: " + _levelData.title);

        foreach (Tile tile in _levelData.tiles)
        {
            Debug.Log("Tile Id: " + tile.id);
            Debug.Log("Tile Position: (" + tile.position.x + ", " + tile.position.y + ", " + tile.position.z + ")");
            Debug.Log("Tile Character: " + tile.character);

            if (tile.children.Count > 0)
            {
                Debug.Log("Tile Children: " + string.Join(", ", tile.children));
            }
        }
        */
    }
}




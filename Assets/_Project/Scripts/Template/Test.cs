
using UnityEngine;
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

    
       Debug.Log("Title: " + _levelData.title);

        foreach (Tile tile in _levelData.tiles)
        {
            Debug.Log("Tile Id: " + tile.Id);
            Debug.Log("Tile Position: (" + tile.Position.X + ", " + tile.Position.Y + ", " + tile.Position.Zvalue + ")");
            Debug.Log("Tile Character: " + tile.Character);

            if (tile.Children.Count > 0)
            {
                Debug.Log("Tile Children: " + string.Join(", ", tile.Children));
            }
        }
        
    }
}




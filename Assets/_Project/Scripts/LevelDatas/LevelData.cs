using System.Collections.Generic;

[System.Serializable]
public class LevelData
{
    public string title { get; set; }
    public List<TileLevelData> tiles { get; set; }
}

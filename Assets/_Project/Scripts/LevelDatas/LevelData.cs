using System.Collections.Generic;

[System.Serializable]
public class LevelData
{
    /*
     * Jsondan gelen bilgilerin ayarlanmasını sağlar
     */
    public string title { get; set; }
    public List<TileLevelData> tiles { get; set; }
}

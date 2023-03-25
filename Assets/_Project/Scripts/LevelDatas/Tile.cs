using System.Collections.Generic;

[System.Serializable]
public class Tile
{
    public int id { get; set; }
    public Position position { get; set; }
    public string character { get; set; }
    public List<int> children { get; set; }
}


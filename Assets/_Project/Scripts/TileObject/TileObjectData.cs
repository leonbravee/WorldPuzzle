using TMPro;
using UnityEngine;

[System.Serializable]
public class TileObjectData
{
    public TileLevelData TileLevelData;
    public TileObjectState TileObjectState;
    [Header("** Other Settings **")]
    public Color SelectableColor;
    public Color UnSelectableColor;
    [Header("** Other Objects**")]
    public TextMeshPro TileText;
}

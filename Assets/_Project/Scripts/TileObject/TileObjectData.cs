using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class TileObjectData
{
    public TileLevelData TileLevelData;
    public TileObjectState TileObjectState;
   [Header("** Other Settings **")]
    public float ColorChangeTime=.25f;
    public float MoveToFirstPosTime = .25f;
    public Color SelectableColor;
    public Color UnSelectableColor;
    public Color SelectedColor=Color.green;
    public Color PlacedColor;
    [Header("** Other Objects**")]
    public TextMeshPro TileText;
    public MeshRenderer TileRenderer;
}

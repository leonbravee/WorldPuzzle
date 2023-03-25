using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    [SerializeField] private TileObjectData _properties;

    public TileLevelData TileLevelData
    {
        set { _properties.TileLevelData = value; }
    }

    public TileObjectState TileObjectState
    {
        set
        {
            _properties.TileObjectState = value;

            if (value == TileObjectState.UnSelectable)
            {
                transform.GetComponent<MeshRenderer>().material.color = _properties.UnSelectableColor;
            }
            else if (value == TileObjectState.Selectable)
            {
                transform.GetComponent<MeshRenderer>().material.color = _properties.SelectableColor;
            }
        }
        get
        {
            return _properties.TileObjectState;
        }
    }

    public int TileId
    {
        get
        {
            return _properties.TileLevelData.Id;
        }
    }

    public List<int> ChildrenIds
    {
        get
        {
            return _properties.TileLevelData.Children;
        }
    }

    private void Awake()
    {
        gameObject.name = "" + _properties.TileLevelData.Id;
        transform.localPosition = _properties.TileLevelData.Position.WorldPosition;
        _properties.TileText.text = _properties.TileLevelData.Character;
    }

    private void OnMouseDown()
    {
        if(_properties.TileObjectState==TileObjectState.UnSelectable) return;
        
        transform.GetComponent<MeshRenderer>().material.color = Color.green;

    }

    private void OnMouseUp()
    {
        if (_properties.TileObjectState == TileObjectState.UnSelectable)
        {
            transform.GetComponent<MeshRenderer>().material.color = _properties.UnSelectableColor;
        }
        else if (_properties.TileObjectState == TileObjectState.Selectable)
        {
            transform.GetComponent<MeshRenderer>().material.color = _properties.SelectableColor;
        }
    }
}
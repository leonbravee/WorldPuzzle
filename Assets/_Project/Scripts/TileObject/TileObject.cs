using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    [SerializeField] 
    private TileObjectData _properties;

    private Vector3 _firstPos;

    public TileLevelData TileLevelData
    {
        set
        {
            _properties.TileLevelData = value;
        }
        
    }

    public Char TileChar
    {
        get
        {
            if (_properties.TileLevelData.Character[0] == 'I') return 'i';
           return char.ToLower(_properties.TileLevelData.Character[0]);
        }
    }

    public TileObjectState TileObjectState
    {
        set
        {
            _properties.TileObjectState = value;

            if (value == TileObjectState.UnSelectable)
            {
               _properties.TileRenderer.material.color = _properties.UnSelectableColor;
            }
            else if (value == TileObjectState.Selectable)
            {
                _properties.TileRenderer.material.color = _properties.SelectableColor;
            }
            else if (value == TileObjectState.Selected)
            {
                OnSelected();
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
        _firstPos = _properties.TileLevelData.Position.WorldPosition;
    }

    private void OnSelected()
    {
        if(_properties.TileObjectState==TileObjectState.UnSelectable) return;
        
        _properties.TileRenderer.material.DOColor(_properties.SelectedColor, _properties.ColorChangeTime);
        PlayerController.Instance.SelectedTileObject = this;


    }

    public void TouchUp()
    {
        if(_properties.TileObjectState==TileObjectState.Placed) return;
        
        _properties.TileObjectState = TileObjectState.Selectable;
        
        
        _properties.TileRenderer.material.DOColor(_properties.SelectableColor, _properties.ColorChangeTime);

        transform.DOLocalMove(_firstPos, _properties.MoveToFirstPosTime);
    }

    public void Place(Transform placeSlotTransform)
    {
        _properties.TileObjectState = TileObjectState.Placed;
        transform.DOLocalMove(placeSlotTransform.position, .35f);
        LeonBrave.DragDrop.Instance.CanDrag = false;
        PlayerController.Instance.AddPlacedObject(this);
    }

    public void BlowYourSelf()
    {
        if (_properties.TileLevelData.Children.Count > 0)
        {
            foreach (int childId in _properties.TileLevelData.Children)
            {
                TileBuilder.Instance.GetTileObjectFromId(childId).TileObjectState = TileObjectState.Selectable;
            }
        }

        _properties.TileObjectState = TileObjectState.Done;
        
        gameObject.SetActive(false);
    }
    
    
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
     
public class PlacementTrigger : MonoBehaviour
{
 /*
  * Kendisine çarpan harfleri yerleştirildi sayar
  * Boşta olan slota göre sırayla yerleştirir.
  * Player tarafından yönetilir
  */
    public static PlacementTrigger Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    private List<Transform> _placeSlots;

    private int _placedIndex = -1;

    public int PlacedIndex
    {
        get
        {
            return _placedIndex;
        }
        set
        {
            _placedIndex = value;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tile") && other.TryGetComponent(out TileObject tileObject) && tileObject.TileObjectState==TileObjectState.Selected)
        {
            _placedIndex++;
            tileObject.Place(_placeSlots[_placedIndex]);
        }
    }
}

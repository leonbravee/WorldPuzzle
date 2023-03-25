using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class TileObjectPool : MonoBehaviour
{
    public static TileObjectPool Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private List<TileObject> _tileObjectsList;

    [SerializeField] private TileObject _tileObjectPrefab;

    public void AddObject(TileObject tileObject)
    {
        if (!_tileObjectsList.Contains(tileObject))
        {
            _tileObjectsList.Add(tileObject);

            tileObject.transform.parent = transform;
            tileObject.transform.localPosition = Vector3.zero;

            tileObject.gameObject.SetActive(false);
        }
    }

    public TileObject TakeObject()
    {
        TileObject tileObject;
        if (_tileObjectsList.Count > 0)
        {
            tileObject = _tileObjectsList[0];
            _tileObjectsList.Remove(tileObject);
        }
        else
        {
            tileObject = Instantiate(_tileObjectPrefab);
        }

        //tileObject.gameObject.SetActive(true);
        tileObject.transform.parent = null;
        return tileObject;
    }
}
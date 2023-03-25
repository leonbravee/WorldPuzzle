using UnityEngine;

[System.Serializable]
public class Position
{

    private float _x=0f;

    public float X
    {
        set
        {
            _x = value;
        }
        get
        {
            return _x;
        }
    }

    private float _y=0f;

    public float Y
    {
        set
        {
            _y = value;
        }

        get
        {
            return _y;
        }
    }

    private float _z=0f;

    public float Z
    {
        set
        {
            _z = value;
        }
        get
        {
            return _z;
        }
    }

    public Vector3 WorldPosition
    {
        get
        {
            return new Vector3(_x, _y, _z);
        }
    }
}
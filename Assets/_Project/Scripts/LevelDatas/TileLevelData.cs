using System.Collections.Generic;

[System.Serializable]
public class TileLevelData
{

    private int _id;

    public int Id
    {
        set
        {
            _id = value;
        }
        get
        {
            return _id;
        }
    }
    private Position _position;
    
    public Position Position
    {
        set
        {
            _position = value;
        }
        get
        {
            return _position;
        }
    }

    private string _character;


    public string Character
    {
        set
        {
            _character = value;
        }
        get
        {
            return _character;
        }
    }


    private List<int> _children;

    public List<int> Children
    {
        set
        {
            _children = value;
        }
        get
        {
            return _children;
        }
    }
    
}


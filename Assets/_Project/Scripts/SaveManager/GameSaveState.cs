using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class GameSaveState
{
    private List<int> _levelScores = new List<int> { 0, 0, 0, 0 };

    private int _lastLevel;
    
    public void SetSaveState()
    {
        
        _lastLevel = PlayerPrefs.GetInt("LastLevel", 0);
        string loadedIntArrayString = PlayerPrefs.GetString("LevelScore");
        string[] loadedIntArrayStrings = loadedIntArrayString.Split(',');
        int[] loadedIntArray = new int[loadedIntArrayStrings.Length];
        
        if(loadedIntArrayStrings.Length<=1) return;
        
        for (int i = 0; i < loadedIntArrayStrings.Length; i++)
        { 
            loadedIntArray[i] = int.Parse(loadedIntArrayStrings[i]);
        }

        _levelScores = loadedIntArray.ToList();

    }

    public void SaveLevelScore(int levelId,int score)
    {
        string intArrayString = "";

        if (_levelScores.Count < levelId)
        {
            _levelScores.Insert(levelId,score);
        }
        else
        {
            _levelScores[levelId] = score;
        }
      
        foreach (var t in _levelScores)
        {
            intArrayString += t+ ",";
        }
        PlayerPrefs.SetString("LevelScore", intArrayString);
        PlayerPrefs.Save();
        
    }

    public int GetLevelScore(int levelId)
    {
        if ( _levelScores.Count==0 || levelId >= _levelScores.Count)
        {
            return 0;
        }
        
        return _levelScores[levelId];
        
    }

    public int LastLevel
    {
        set
        {
            _lastLevel = value;
            PlayerPrefs.SetInt("LastLevel",value);
        }
        get
        {
            return _lastLevel;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class GameSaveState
{
    
    /*
     * Basit bir kayıt sınıfı
     * her level'in puanını ve en son oynanılan level'in index'ini tutar
     */
    private List<int> _levelScores;

    private int _lastLevel;
    
    public void SetSaveState()
    {
        
        _lastLevel = PlayerPrefs.GetInt("LastLevel", 0);
        string loadedIntArrayString = PlayerPrefs.GetString("LevelScore","0,0,0,0");
        
        if(loadedIntArrayString.Length<=1) return;
        
        string[] loadedIntArrayStrings = loadedIntArrayString.Split(',');
        int[] loadedIntArray = new int[loadedIntArrayStrings.Length];
        
       
        
        for (int i = 0; i < loadedIntArrayStrings.Length; i++)
        { 
            if(loadedIntArrayStrings[i]=="") continue;
            
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

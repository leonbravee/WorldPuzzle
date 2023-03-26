using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollButton : MonoBehaviour
{
    [SerializeField]
    private int _levelId;

    
    [SerializeField]
    private TextMeshProUGUI _buttonText;

    [SerializeField]
    private Button _button;

    private bool _canPlayable = false;
    
  
    

    private void Awake()
    { 
        SetButtonView(); 
        SetText();
    }

    private void SetButtonView()
    {
        int lastLevel = SaveManager.Instance.GameSaveState.LastLevel;

        if (lastLevel >= _levelId)
        {
            _button.GetComponent<Image>().color=Color.green;
            _canPlayable = true;
        }
        else  if (lastLevel <= _levelId && _levelId-2<=lastLevel)
        {
            _button.GetComponent<Image>().color=Color.green;
            _canPlayable = true;
        }
        else
        {
            _button.GetComponent<Image>().color = Color.red;
        }
    }

    private void SetText()
    {
        _buttonText.text = "Level : " + (_levelId+1) + " \n" + "Score : " +SaveManager.Instance.GameSaveState.GetLevelScore(_levelId);
    }

    public void StartButtonDown()
    {
        if(!_canPlayable) return;

        SaveManager.Instance.GameSaveState.LastLevel = _levelId;
       GameManager.Instance.StartGame(_levelId);
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
     
public class ScoreManager : MonoBehaviour
{
	public static ScoreManager Instance;

	[SerializeField]
	private ScoreManagerData _properties;

	private int _currentScore = 0;
	
	void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else if(Instance != this)
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		CanvasController.Instance.ShowScore(_currentScore);
	}

	public void SetScore(string correctWord)
	{
		int earnScore = 0;
		for (int i = 0; i < correctWord.Length; i++)
		{
			int jTarget = GetLetterScore(correctWord[i]);
			
			for (int j = 0; j < jTarget; j++)
			{
				earnScore += 10;
			}
		}

		_currentScore += earnScore;
		CanvasController.Instance.ShowScore(_currentScore);
	}
	

	public void SaveScore()
	{
		if(SaveManager.Instance.GameSaveState.GetLevelScore(SaveManager.Instance.GameSaveState.LastLevel)>_currentScore) return;
		
		SaveManager.Instance.GameSaveState.SaveLevelScore(SaveManager.Instance.GameSaveState.LastLevel,_currentScore);
	}

	public void ResetScore()
	{
		_currentScore = 0;
		CanvasController.Instance.ShowScore(_currentScore);
	}
	
	public void AddUnUsedWordsTheScore()
	{
		char[] unUsedChars = TileBuilder.Instance.GetUnUsedTiles();

		_currentScore -= unUsedChars.Length * 100;
		if (_currentScore < 0)
		{
			_currentScore = 0;
		}
		CanvasController.Instance.ShowScore(_currentScore);
	}


	private int GetLetterScore(char targetLetter)
	{
		foreach (ScoreData scoreData in _properties.ScoreDatas)
		{
			if (scoreData.IsInTargetLetters(targetLetter))
			{
				return scoreData.Score;
			}
		}

		return 1;
	}
}

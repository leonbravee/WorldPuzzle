using System.Collections;
using System.Collections.Generic;
using UnityEngine;
     
public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	
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
		AnswerHandler.BuildAnswerLibrary();
	}

	private GameState _gameState = GameState.None;

	private int _currentLevelId;

	public GameState GameState
	{
		get
		{
			return _gameState;
		}
	}
	

	public void StartGame(int levelId)
	{
		CanvasController.Instance.SetTrigger("Game_IN_OUT");
		_currentLevelId = levelId;
		TileBuilder.Instance.BuildLevel(levelId);
		_gameState = GameState.Playing;
	}

	private int _maxLength;
	private bool GenerateWords(char[] characters, string currentWord, int index, int maxLength)
	{
		if (index == maxLength)
		{
			if (AnswerHandler.IsStringInList(currentWord))
			{
				return true;
			}

			return false;
		}

		for (int i = 0; i < characters.Length; i++)
		{
			string newWord = currentWord + characters[i];
			bool found = GenerateWords(characters, newWord, index + 1, maxLength);
			if (found)
			{
				return true;
			}
		}

		return false;
	}

	public void  CheckIsGameEnd()
	{
		char[] characters = TileBuilder.Instance.GetUseFulTiles();
		_maxLength = characters.Length;
		if (_maxLength > 5)
		{
			return;
		}
		
		if (!GenerateWords(characters, "", 0, _maxLength))
		{
			UpdateGameState(GameState.Won);
		}
	}

	private void UpdateGameState(GameState gameState)
	{
		if(_gameState==GameState.Won) return;
		
		_gameState = gameState;

		if (gameState == GameState.Won)
		{
			CanvasController.Instance.SetTrigger("OUT");
			SaveManager.Instance.GameSaveState.LastLevel=(_currentLevelId+1);
			ScoreManager.Instance.ResetScore();
		}
	}
}

public enum GameState
{
	None,
	Playing,
	Won,
	
}

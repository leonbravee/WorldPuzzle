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
		TileBuilder.Instance.BuildLevel(levelId);
		_gameState = GameState.Playing;
	}

	private int maxLength;
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
}

public enum GameState
{
	None,
	Playing,
	End,
	
}

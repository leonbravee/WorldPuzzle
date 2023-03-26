using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        AnswerHandler.BuildAnswerLibrary();
    }

    private GameState _gameState = GameState.None;

    private int _currentLevelId;

    public GameState GameState
    {
        get { return _gameState; }
    }


    public void StartGame(int levelId)
    {
        CanvasController.Instance.SetTrigger("Game_IN_OUT");

        _currentLevelId = levelId;

        TileBuilder.Instance.BuildLevel(levelId);

        _gameState = GameState.Playing;
    }


    public void CheckIsGameEnd()
    {
        char[] characters = TileBuilder.Instance.GetUseFulTiles();

        string characterString = "";

        foreach (char character in characters)
        {
            characterString += character;
        }

        _stopFlag = false;
        if (!HaveWordInCurrentGame(characterString, ref _stopFlag))
        {
            UpdateGameState(GameState.Won);
        }
    }

    private void UpdateGameState(GameState gameState)
    {
        if (_gameState == GameState.Won) return;

        _gameState = gameState;

        if (gameState == GameState.Won)
        {
            ScoreManager.Instance.AddUnUsedWordsTheScore();
            CanvasController.Instance.SetTrigger("OUT");
            SaveManager.Instance.GameSaveState.LastLevel = (_currentLevelId + 1);
            ScoreManager.Instance.ResetScore();
        }
    }

    

    private bool _stopFlag = false;

    private List<string> words;


    /*
     * Aşağıda kelime üreten fonksiyonu internet üzerinde araştırıp case'e uygun şekle getirip kullandım.
     */
    private bool HaveWordInCurrentGame(string characters, ref bool stopFlag)
    {
        char[] charArray = characters.ToCharArray();
        for (int i = 3; i <= charArray.Length; i++)
        {
            GetPermutations(charArray, i, 0, ref stopFlag);
            if (stopFlag)
            {
                break;
            }
        }

        return stopFlag;
    }
    
    private void GetPermutations(char[] charArray, int length, int index, ref bool stopFlag)
    {
        if (stopFlag)
        {
            return;
        }
        if (index == length)
        {
            if (AnswerHandler.IsStringInList(new string(charArray, 0, length))) //yazılan olasılık en listesinde varsa kelime üretmenin kesilmesi
            {
                stopFlag = true;
               // Debug.LogError(new string(charArray, 0, length));
            }

            return;
        }

        for (int i = index; i < charArray.Length; i++)
        {
            Swap(ref charArray[index], ref charArray[i]); //elemanların yerlerinin değiştirilmesi

            GetPermutations(charArray, length, index + 1,
                ref stopFlag); //geçerli dizi için yazılabilecek olasıklara bakılması

            Swap(ref charArray[index], ref charArray[i]);

            if (stopFlag)
            {
                return;
            }
        }
    }

    private void Swap(ref char a, ref char b)
    {
        (a, b) = (b, a);
    }
}

public enum GameState
{
    None,
    Playing,
    Won,
}
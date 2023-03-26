using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class AutoSolver : MonoBehaviour
{

	[SerializeField]
	private bool _useAutoSolver;
	

#if UNITY_EDITOR
	private void Update()
	{
		if (GameManager.Instance.GameState != GameState.Playing) return;
		if (Input.GetKeyDown(KeyCode.W))
		{

			List<char> characters = TileBuilder.Instance.GetUseFulTiles().ToList();

			string characterString = "";

			_stopFlag = false;

			if (characters.Count > 7) //7 karakterin üstünde çalışmayı kesiyor bu nedenle böyle yaptım.
			{
				Shuffle(characters);
				int endIndex = characters.Count - 7;
				for (int i = 0; i < endIndex; i++)
				{
					characters.RemoveAt(0);
				}
			}

			foreach (char character in characters)
			{
				characterString += character;
			}


			_highWord = "";
			_highScore = 0;
			GetHighPointWord(characterString, ref _stopFlag);
			Debug.Log("Word :" + _highWord);
			Debug.Log("Point " + _highScore);

			TileBuilder.Instance.AutoMove(_highWord);
		}

	}
	
	    private int _highScore=0;
    	private string _highWord="";
        private bool _stopFlag;
        
        private void GetHighPointWord(string characters, ref bool stopFlag)
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
    
    	  
        }
        
        private void GetPermutations(char[] charArray, int length, int index, ref bool stopFlag)
        {
    	    if (stopFlag)
    	    {
    		    return;
    	    }
    	    if (index == length)
    	    {
    		    if (AnswerHandler.IsStringInList(new string(charArray, 0, length))) 
    		    {
    			    CheckScore(new string(charArray, 0, length));
    		    }
    
    		    return;
    	    }
    
    	    for (int i = index; i < charArray.Length; i++)
    	    {
    		    Swap(ref charArray[index], ref charArray[i]);
    
    		    GetPermutations(charArray, length, index + 1, ref stopFlag); 
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
    
            private void CheckScore(string word)
            {
                int score = ScoreManager.Instance.GetScore(word);
                if (score > _highScore)
                {
                    _highScore = score;
                    _highWord = word;
                }
            }
           private  void Shuffle<T>(List<T> list)
            {
    	        Random rng = new Random();
    	        int n = list.Count;
    	        while (n > 1)
    	        {
    		        n--;
    		        int k = rng.Next(n + 1);
    		        T value = list[k]; 
    		        list[k] = list[n]; 
    		        list[n] = value;
    	        }
            }
	
#endif



 
}

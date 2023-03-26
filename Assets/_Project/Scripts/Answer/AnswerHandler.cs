using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
     
public static class AnswerHandler
{
	private static List<AnswerLibrary> _answerLibraries = new List<AnswerLibrary>();

	private static bool _isBuilded = false;
	public  static  void BuildAnswerLibrary()
	{
		
		if(_isBuilded) return;

		_isBuilded = true;
		
		string filePath = Path.Combine(Application.dataPath, "Resources", "en.txt");
		

		if (File.Exists(filePath))
		{
			string fileContent = File.ReadAllText(filePath);
			string[] lines = fileContent.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
			foreach (string line in lines)
			{
				Char firstLetter = line[0];
				if (GetAnswerLibrary(LetterToId(firstLetter))==null)
				{
					GetAnswerLibrary(LetterToId(firstLetter)).AddWord(line);
					continue;
				}
				GetAnswerLibrary(LetterToId(firstLetter)).AddWord(line);
			}
		}
		
	}
	
	private static int LetterToId(char letter)//aschi tablosuna harflere id atanması
	{
		return  (int)letter-97;
	}
	
	private static AnswerLibrary GetAnswerLibrary(int letterId)//aranan harfin ilk harfine göre o harfle başlayanların listesinin elde edilmesi
	{
		if (_answerLibraries.Count - 1 < letterId)
		{
			AnswerLibrary answerLibraryTemp = new AnswerLibrary(letterId);
			_answerLibraries.Add(answerLibraryTemp);
			return null;
		}

		return _answerLibraries[letterId];
	}

	public static bool IsStringInList(string word) // gelen string txt dosyasında var mı yok mu
	{
		if (word.Length < 3) return false;
		
		return GetAnswerLibrary(LetterToId(word[0])).IsStringInList(word);
	}



}

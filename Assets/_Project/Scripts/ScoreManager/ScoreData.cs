using System;
using System.Collections.Generic;

[System.Serializable]
public class ScoreData
{
  public int Score;
  public List<Char> TargetLetters;

  public bool IsInTargetLetters(char targetLetter)
  {

      return TargetLetters.Contains(targetLetter);

  }
}

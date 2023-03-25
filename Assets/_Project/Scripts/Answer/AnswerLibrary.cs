using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class AnswerLibrary
{
    public int LetterId;
    public AnswerLibrary(int letterId)
    {
        LetterId = letterId;
        WordList = new List<string>();
    }
    public List<string> WordList;

    public void AddWord(string word)
    { 
        /*
 
 * Harfler eklenirken ikili arama ile uzunlukluğunun yerleşeceği index bulunup öyle eklenir.
 * Böylelikle uzunluğa göre sıralanmış kelime listesi elde edilir.
         * */
        int index = WordList.BinarySearch(word, Comparer<string>.Create((x, y) => x.Length.CompareTo(y.Length)));
        if (index < 0) 
        {
            index = ~index;
        }
        WordList.Insert(index, word);
    }
    
    public  bool IsStringInList(string item)
    {
/*
 * Kelimenin listede aranması
 * Kelimenin uzunluğunun başlangıç ve bitiş index'i bulunup bu iki index arasında aranır.
 * Aranma liner olarak gerçekleşir.
 */
        int startIndex = GetStartIndex(item.Length);
        int endIndex = GetStartIndex(item.Length + 1);
        
      //  Debug.Log(item);
     //   Debug.Log("start index :"+ startIndex);
      //  Debug.Log("end index : "+endIndex);
        if (startIndex >= 0)
        {
            for (int i = startIndex; i <=endIndex; i++)
            {
                if (WordList[i]==item) return true;
            }
        }
        
        return false;
        
    }
    private int GetStartIndex(int length)
   {
       /*
 *Aranacak harfin uzunluğunun başlangıç index'inin bulunması
 */
       int startIndex = 0;
       int endIndex = WordList.Count - 1;

       while (startIndex <= endIndex)
       {
           int middleIndex = (startIndex + endIndex) / 2;

           if (WordList[middleIndex].Length < length)
           {
               startIndex = middleIndex + 1;
           }
           else if (middleIndex > 0 && WordList[middleIndex - 1].Length >= length)
           {
               endIndex = middleIndex - 1;
           }
           else
           {
               return middleIndex;
           }
       }

       return -1;
    }
}


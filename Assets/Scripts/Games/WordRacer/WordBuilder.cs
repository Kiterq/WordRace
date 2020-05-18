using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

public class WordBuilder 
{
    public string word;
    public char[] wordChars;
    public bool[] revealed;
    public List<char> noChars;
    public List<char> okChars;
    private int roundIndex = 0;
    private static Vector2[] ROUNDS = new Vector2[] {
            new Vector2(2,3),
            new Vector2(2,4),
            new Vector2(1,3),
            new Vector2(2,5),
            new Vector2(1,4),
            new Vector2(2,6),
            new Vector2(1,5),
            new Vector2(1,6),
            new Vector2(2,7),
            new Vector2(1,7)
        };
    
    public string AddLetter(string trackContents, char character)
    {
        var sb = new StringBuilder();

        sb.Append(trackContents);

        var firstUnderscore = trackContents.IndexOf('_');

        var lastLetterOfPreviousWord = sb[firstUnderscore - 1];

        var newWordStringBuilder = new StringBuilder();

        newWordStringBuilder.Append(lastLetterOfPreviousWord);
        newWordStringBuilder.Append(character);

        var newWord = newWordStringBuilder.ToString();

        if (IsAWord(newWord))
        {
            // Raise event to allow summit
        }
        else
        {
            // Keep going...
        }

        sb.Replace('_', character, firstUnderscore, 1);

        trackContents = sb.ToString();

        return trackContents;
    }

    ////public string AddLetter(string trackContents, char character)
    ////{
    ////    var sb = new StringBuilder();

    ////    sb.Append(trackContents);

    ////    var firstUnderscore = trackContents.IndexOf('_');

    ////    var lastLetterOfPreviousWord = sb[firstUnderscore - 1];

    ////    var newWordStringBuilder = new StringBuilder();

    ////    newWordStringBuilder.Append(lastLetterOfPreviousWord);
    ////    newWordStringBuilder.Append(character);

    ////    var newWord = newWordStringBuilder.ToString();

    ////    if (IsAWord(newWord))
    ////    {
    ////        // Raise event to allow summit
    ////    }
    ////    else
    ////    {
    ////        // Keep going...
    ////    }

    ////    sb.Replace('_', character, firstUnderscore, 1);

    ////    trackContents = sb.ToString();

    ////    return trackContents;
    ////}

    public bool IsAWord(string word)
    {
        // TODO Check if is a word using the dictionary.
        return false;
    }

    public static WordBuilder Create()
    {
        return new WordBuilder();
    }

    public bool HasChar(char c)
    {
        if (okChars.IndexOf(c) == -1) return false;

        var index = okChars.IndexOf(c);
        okChars.RemoveAt(index);

        for (var i = 0; i < wordChars.Length; i++)
        {
            if (wordChars[i] == c)
            {
                revealed[i] = true;
            }
        }
        return true;
    }

    public char[] GetHints()
    {
        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

        var result = new List<char>();

        var correct = ROUNDS[roundIndex].x;
        var total = ROUNDS[roundIndex].y;

        if (correct > okChars.Count)
            correct = okChars.Count;

        var i = 0;
        while (result.Count < total)
        {
            if (result.Count < correct)
            {
                var c = okChars[Random.Range(0, okChars.Count)];
                if (!result.Contains(c))
                {
                    result.Add(c);
                }
            }
            else
            {
                var c = noChars[Random.Range(0, noChars.Count)];
                if (!result.Contains(c))
                {
                    result.Add(c);
                }
            }
        }

        roundIndex++;
        if (roundIndex == ROUNDS.Length)
            roundIndex = ROUNDS.Length - 1;

        result.Clear();
        result.Add('g');
        result.Add('f');
        result.Add('e');
        result.Add('d');
        result.Add('c');
        result.Add('b');
        result.Add('a');

        return result.ToArray();
    }
}

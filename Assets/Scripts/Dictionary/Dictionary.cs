using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dictionary : IDictionary
{
    public List<string> Words { get; private set; }

    public Dictionary(List<string> words)
    {
        this.Words = words;
    }

    public bool IsAWord(string word)
    {
        return this.Words.Any(x => x.ToLower() == word.ToLower());
    }

    public static Dictionary Create(List<string> words)
    {
        return new Dictionary(words);
    }
}

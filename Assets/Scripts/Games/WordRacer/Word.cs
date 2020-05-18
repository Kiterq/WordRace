using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word
{
    public List<Word> Collection { get; private set; }
    public string Value { get; private set; }
    public string Colour { get; private set; }

    public Word()
    {
        this.Collection = new List<Word>();
    }

    public Word(string value) : this()
    {
        this.Value = value;
    }

    public static Word Create(string value)
    {
        return new Word(value);
    }

    public void Add(Word word)
    {
        this.Collection.Add(word);
    }

    public int GetTrackUsed()
    {
        var trackUsed = 0;
        foreach (var word in this.Collection)
        {
            trackUsed = trackUsed + word.Value.Length;
        }

        return trackUsed - this.Collection.Count + 1;
    }
}
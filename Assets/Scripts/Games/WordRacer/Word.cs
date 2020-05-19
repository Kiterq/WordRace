using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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

    public int Length
    {
        get
        {
            return this.Collection.Sum(x => x.Value.Length);
        }
    }

    public int Count
    {
        get
        {
            return this.Collection.Count;
        }
    }

    public char LastLetter
    {
        get
        {
            return this.Value.ToCharArray()[this.Value.Length - 1];
        }
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
    public override string ToString()
    {
        if (this.Collection.Count() > 0)
        {
            var sb = new List<string>();
            foreach (var word in this.Collection)
            {
                sb.Add(word.Value);
            }

            return string.Join(", ", from item in sb select item);
        }

        return this.Value.ToString();
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Letter //: MonoBehaviour
{
    public char Value { get; set; }

    public List<char> LetterDeck { get; private set; }

    public string PossibleWord { get; private set; }

    public List<char> Collection { get; private set; }

    public Letter()
    {
        this.Collection = new List<char>();
    }

    public Letter(char value) : this()
    {
        this.Collection = new List<char>();
        this.Value = value;
    }

    public static Letter Create()
    {
        return new Letter();
    }
    
    ////public static Letter Create() 
    ////{
    ////    return new Letter();
    ////}

    public void Add(char word)
    {
        this.Collection.Add(word);
    }

    public int Count
    {
        get
        {
            return this.Collection.Count;
        }
    }

    public override string ToString()
    {
        if (this.Collection.Count() > 0)
        {
            var sb = new StringBuilder();
            foreach (var value in this.Collection)
            {
                sb.Append(value);
            }

            return sb.ToString();
        }

        return this.Value.ToString();
    }
}

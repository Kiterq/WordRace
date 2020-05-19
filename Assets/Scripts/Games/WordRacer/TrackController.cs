using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
//using Random = UnityEngine.Random;

public class TrackController
{
    public Word Word { get; private set; }

    public Letter Letter { get; set; }

    ////public char Letter { get; set; }

    public List<char> LetterDeck { get; private set; }

    ////public string PossibleWord { get; private set; }

    public int TrackExposedLength { get; private set; }

    public int TrackLength { get; private set; }

    public char TrackSpacer { get; private set; }

    public IDictionary Dictionary { get; }

    public string AcceptedTrack { get; private set; }

    public string ActiveTrack
    {
        get
        {
            if (this.TrackIndex == 0)
            {
                return this.Track.Substring(0, this.TrackExposedLength);
            }

            return this.Track.Substring(this.Word.Length - this.Word.Count, this.TrackExposedLength);
        }
    }

    public int TrackIndex { get; private set; }

    public string Track { get; private set; }

    public bool AllowSave { get; private set; }

    public int TrackUsed
    {
        get
        {
            return this.Word.GetTrackUsed();
        }
    }

    public bool IsSpaceAvailable
    {
        get
        {
            return ExposedSpacesAvailable > 0;
        }
    }

    public int ExposedSpacesAvailable
    {
        get
        {
            return this.ActiveTrack.Split(this.TrackSpacer).Length - 1;
        }
    }

    public static TrackController Create(int trackExposed, char trackSpace, IDictionary dictionary, int trackLength)
    {
        return new TrackController(trackExposed, trackSpace, dictionary, trackLength);
    }

    public TrackController(int trackExposedLength, char trackSpacer, IDictionary dictionary, int trackLength)
    {
        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

        this.LetterDeck = new List<char>();

        this.Letter = Letter.Create();

        this.Word = new Word();

        this.TrackSpacer = trackSpacer;
        this.Dictionary = dictionary;
        this.TrackLength = trackLength;
        this.BuildTrack();
        this.TrackExposedLength = trackExposedLength;
        this.AcceptedTrack = this.ActiveTrack;
    }

    public void SetPlayersLetterDeck(List<char> c)
    {
        this.LetterDeck = c;
    }

    public void BuildTrack()
    {
        this.TrackIndex = 0;

        if (this.Letter != null)
        {
            this.Letter.Collection.Clear();
        }
        
        this.IsStartOfGo = true;
        this.Word.Collection.Clear();

        var sb = new StringBuilder();

        for (int i = 0; i < this.TrackLength; i++)
        {
            sb.Append(this.TrackSpacer);
        }

        this.Track = sb.ToString();
    }

    public bool IsStartOfGo { get; set; }

    public void Save()
    {
        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

        if (this.AllowSave)
        {
            this.AcceptedTrack = this.ActiveTrack;

            // Only create word if is a word?.. .
            var word = Word.Create(this.Letter.ToString());

            Word.Add(word);

            this.IsStartOfGo = false;

            //Start of the word.. 
            this.Letter = Letter.Create();
            this.Letter.Add(word.LastLetter); 
        }
    }

    public string AddLetter(char letter)
    {
        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

        ////this.Letter.Value = letter;

        if (this.IsSpaceAvailable)
        {
            var sb = new StringBuilder();

            sb.Append(this.Track);

            var firstUnderscore = this.Track.IndexOf(this.TrackSpacer);

            var newWordStringBuilder = new StringBuilder();

            if (this.IsStartOfGo)
            {
                this.IsStartOfGo = false;

                if (firstUnderscore == 0)
                {
                    //First letter to add to track. 
                    newWordStringBuilder.Append(letter);
                    this.Letter.Add(letter);
                }
                else
                {
                    var lastLetterOfPreviousWord = sb[firstUnderscore - 1];
                    newWordStringBuilder.Append(lastLetterOfPreviousWord);

                    newWordStringBuilder.Append(letter);
                    this.Letter.Add(letter);
                }
            }
            else
            {
                var lastLetterOfPreviousWord = sb[firstUnderscore - 1];
                newWordStringBuilder.Append(lastLetterOfPreviousWord);

                newWordStringBuilder.Append(letter);

                this.Letter.Add(letter);
            }

            //this.LetterDeck.Remove(letter);

            this.TrackIndex++;

            if (this.Dictionary.IsAWord(this.Letter.ToString()))
            {
                // Raise event to offer submit?
                this.AllowSave = true;
                this.Save();
            }
            else
            {
                this.AllowSave = false;
                // Keep going...
            }

            ResetActiveTrack(letter, sb, firstUnderscore);

            this.Track = sb.ToString();
        }

        return this.Track;
    }

    public List<char> GetRemainingDeckLetters(int length)
    {
        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

        var result = new List<char>();

        var randomString = getRandomString(length);

        foreach (var r in randomString)
        {
            result.Add(r);
        }

        return result;
    }

    public string getRandomString(int length)
    {
        // creating a StringBuilder object()
        StringBuilder str_build = new StringBuilder();
        Random random = new Random();

        char letter;

        for (int i = 0; i < length; i++)
        {
            double flt = random.NextDouble();
            int shift = Convert.ToInt32(Math.Floor(25 * flt));
            letter = Convert.ToChar(shift + 65);
            str_build.Append(letter.ToString().ToLower());
        }
        return str_build.ToString();
    }

    public string GetPlayersLetterDeck(int deckTileNumber)
    {
        var newLetterDeck = new List<char>();

        if (this.AllowSave)
        {
            // TODO Keep unused letters
            var remainingLetterCount = deckTileNumber - this.LetterDeck.Count;
            
            foreach (var remainingLetter in this.LetterDeck)
            {
                newLetterDeck.Add(remainingLetter);
            }

            var playersLetterDeck = GetRemainingDeckLetters(remainingLetterCount);
            foreach (var newLetter in playersLetterDeck)
            {
                newLetterDeck.Add(newLetter);
            }

            ;

            this.SetPlayersLetterDeck(newLetterDeck);
        }
        else
        {
            this.BuildTrack();
        }

        return newLetterDeck.ToString();
    }

    private void ResetActiveTrack(char character, StringBuilder sb, int firstUnderscore)
    {
        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

        sb.Replace(this.TrackSpacer, character, firstUnderscore, 1);
    }
}



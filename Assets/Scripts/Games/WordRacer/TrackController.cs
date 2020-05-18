using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

public class TrackController
{
    public Word Word { get; private set; }

    public char Character { get; set; }

    public string PossibleWord { get; private set; }

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

            return this.Track.Substring(this.TrackIndex - this.PossibleWord.Length, this.TrackExposedLength);

            return this.Track.Substring(this.TrackIndex - 1, this.TrackExposedLength);
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

        this.Word = new Word();

        this.TrackSpacer = trackSpacer;
        this.Dictionary = dictionary;
        this.TrackLength = trackLength;
        this.BuildTrack(trackLength);
        this.TrackExposedLength = trackExposedLength;
        this.AcceptedTrack = this.ActiveTrack;
        this.IsStartOfGo = true;
    }

    private void BuildTrack(int trackLength)
    {
        var sb = new StringBuilder();

        for (int i = 0; i < trackLength; i++)
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

            var word = Word.Create(PossibleWord);

            Word.Add(word);

            this.IsStartOfGo = false;

            //Start of the word.. 
            this.PossibleWord = this.PossibleWord.Substring(this.PossibleWord.Length - 1);
        }
    }

    public string AddLetter(char character)
    {
        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

        this.Character = character;

        if (this.IsSpaceAvailable)
        {
            var sb = new StringBuilder();

            sb.Append(this.Track);

            var firstUnderscore = this.Track.IndexOf(this.TrackSpacer);

            var newWordStringBuilder = new StringBuilder();

            if (this.IsStartOfGo)
            {
                PossibleWord = string.Empty;
                this.IsStartOfGo = false;

                if (firstUnderscore == 0)
                {
                    //First letter to add to track. 
                    newWordStringBuilder.Append(character);
                    PossibleWord = newWordStringBuilder.ToString();
                }
                else
                {
                    var lastLetterOfPreviousWord = sb[firstUnderscore - 1];
                    newWordStringBuilder.Append(lastLetterOfPreviousWord);

                    newWordStringBuilder.Append(character);
                    PossibleWord = newWordStringBuilder.ToString();
                }
            }
            else
            {
                var lastLetterOfPreviousWord = sb[firstUnderscore - 1];
                newWordStringBuilder.Append(lastLetterOfPreviousWord);

                newWordStringBuilder.Append(character);

                PossibleWord = PossibleWord + character;
            }

            this.TrackIndex++;

            if (this.Dictionary.IsAWord(PossibleWord))
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

            ResetActiveTrack(character, sb, firstUnderscore);

            this.Track = sb.ToString();
        }

        return this.Track;
    }

    private void ResetActiveTrack(char character, StringBuilder sb, int firstUnderscore)
    {
        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

        sb.Replace(this.TrackSpacer, character, firstUnderscore, 1);
    }
}



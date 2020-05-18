using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Reflection;
using System.Threading;
using Random = System.Random;

public partial class WordRacer : MonoBehaviour
{
    public Text word;
    ////public WordRacerPuzzleData puzzleData;
    private WordRacerPanel tiles;
    public Dictionary dictionary;



    ////private Level level;

    ////private AddLetter addLetter;
    private int lives = 5;
    private bool gameActive;

    private WordBuilder wordBuilder;

    private TrackController trackController;

    void Awake()
    {
        tiles = GetComponent<WordRacerPanel>();

        WordRacerGameEvents.OnGameLoaded += HandleGameLoaded;
        WordRacerGameEvents.OnLetterSelected += HandleLetterSelected;
    }

    void Start()
    {
        Utils.MyLog("WORD RACER HAS STARTED");
        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

        //BY Added.
        Utils.ClearLogConsole();

        var fileName =
            @"C:\Users\brett\Downloads\UnityWordGames-master\UnityWordGames-master\Assets\StreamingAssets\words\1k.txt";

        var result = System.IO.File.ReadAllText(fileName);

        var words = result.Split('\n');

        wordBuilder = WordBuilder.Create();

        //New
        var trackLength = 50;
        var trackExposed = 10;
        var trackSpace = '_';
        var letter = 'k';
        var secondLetter = 'o';
        var thirdLetter = 'a';
        var fourthLetter = 'l';
        var fifthLetter = 'a';

        ////var dictionary = Dictionary.Create(new List<string>() { "KOALA", "ALP" });
        var dictionary = Dictionary.Create(words.ToList());

        trackController = TrackController.Create(trackExposed, trackSpace, dictionary, trackLength);

        word.text = trackController.ActiveTrack;

        // End new... 

        ////puzzleData.LoadData();

        WordRacerGameEvents.GameLoaded();
    }

    void HandleGameLoaded()
    {
        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));
        NewRound();
    }

    void NewRound()
    {
        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));
        SelectWord();
        ShowWord();
        ////tiles.ShowPanel(level.GetPlayersLetterDeck1());
        tiles.ShowPanel(GetPlayersLetterDeck());
        gameActive = true;
    }

    public char[] GetPlayersLetterDeck()
    {
        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

        var result = new List<char>();

        var randomString = getRandomString();

        foreach (var r in randomString)
        {
            result.Add(r);
        }

        return result.ToArray();
    }

    public string getRandomString()
    {
        int length = 7;

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

    void HandleLetterSelected(char letter)
    {
        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

        if (!gameActive)
            return;
        
        trackController.AddLetter(letter);
        word.text = trackController.ActiveTrack;

        tiles.ShowPanel(GetPlayersLetterDeck());
        ////tiles.ShowPanel(level.GetPlayersLetterDeck1());
        return;
    }

    void SelectWord()
    {
        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

        ////var word = puzzleData.GetWord();
        ////level = new Level(word);
        var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        ////level = new Level(alphabet.ToLower());
    }

    void ShowWord()
    {
        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

        ////var sb = new StringBuilder();
        ////for (var i = 0; i < level.wordChars.Length; i++)
        ////{
        ////    if (level.revealed[i] == true)
        ////    {
        ////        sb.Append(level.wordChars[i]);
        ////    }
        ////    else
        ////    {
        ////        sb.Append("_");
        ////    }
        ////}

        ////word.text = sb.ToString();

        ////word.text = "KOALA____";

        word.text = trackController.ActiveTrack;

        Utils.MyLog(string.Format("'{0}'", word.text));
    }
}
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

    private WordRacerPanel tiles;

    private bool gameActive;

    private TrackController trackController;
    public int deckTileNumber;
    public int trackLength;
    public int trackExposed;
    public char trackSpace;

    void Awake()
    {
        tiles = GetComponent<WordRacerPanel>();

        WordRacerGameEvents.OnGameLoaded += HandleGameLoaded;
        WordRacerGameEvents.OnLetterSelected += HandleLetterSelected;
    }

    void Start()
    {
        Utils.MyLog("WORD RACING HAS STARTED");
        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

        //BY Added.
        Utils.ClearLogConsole();

        var fileName =
            @"C:\Users\brett\Downloads\UnityWordGames-master\UnityWordGames-master\Assets\StreamingAssets\words\1k.txt";

        var result = System.IO.File.ReadAllText(fileName);

        var words = result.Split('\n');

        //New
        deckTileNumber = 7;
        trackLength = 50;
        trackExposed = 10;
        trackSpace = '_';

        ////var dictionary = Dictionary.Create(new List<string>() { "KOALA", "ALP" });
        var dictionary = Dictionary.Create(words.ToList());

        trackController = TrackController.Create(trackExposed, trackSpace, dictionary, trackLength);

        word.text = trackController.ActiveTrack;

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

        word.text = trackController.ActiveTrack;

        var letterDeck = trackController.GetPlayersLetterDeck(deckTileNumber).ToArray();
        tiles.ShowPanel(letterDeck);

        trackController.SetPlayersLetterDeck(letterDeck.ToList());

        gameActive = true;
    }

    void HandleLetterSelected(char letter)
    {
        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

        if (!gameActive)
            return;
        
        trackController.AddLetter(letter);

        var letterDeck = trackController.GetPlayersLetterDeck(deckTileNumber);

        tiles.ShowPanel(letterDeck.ToArray());
        
        word.text = trackController.ActiveTrack;
        
        return;
    }
}
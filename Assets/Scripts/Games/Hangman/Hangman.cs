using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Reflection;

public class Hangman : MonoBehaviour {

	public Text word;
	public HangmanPuzzleData puzzleData;
	private HangmanPanel tiles;
	private Level level;
	private int lives = 5;
	private bool gameActive;

	void Awake () {
		tiles = GetComponent<HangmanPanel> ();

		HangmanGameEvents.OnGameLoaded += HandleGameLoaded;
		HangmanGameEvents.OnLetterSelected += HandleLetterSelected;
	}

	void Start () {
		Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

		//BY Added.
		Utils.ClearLogConsole();

		puzzleData.LoadData ();
	}

	void HandleGameLoaded () {
		Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));
		NewRound ();	
	}

	void NewRound () {
		Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));
		SelectWord ();
		ShowWord ();
		tiles.ShowPanel (level.GetHints ());
		gameActive = true;
	}

	void HandleLetterSelected (char letter) {

		Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

		if (!gameActive)
			return;

		if (lives <= 0)
			return;

		if (level.HasChar (letter)) {
			ShowWord ();
			if (System.Array.IndexOf (level.revealed, false) == -1) {
				Utils.MyLog ("CORRECT");
				gameActive = false;

				Utils.DelayAndCall (this, 2, () => {
					NewRound ();
				});
				return;
			}
		} else {
			lives--;
			if (lives <= 0) Utils.MyLog ("GAME OVER");
		}

		tiles.ShowPanel (level.GetHints ());
	}

	void SelectWord () {

		Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

		var word = puzzleData.GetWord ();
		level = new Level (word);
	}

	void ShowWord () {

		Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

		var sb = new StringBuilder ();
		for (var i = 0; i < level.wordChars.Length; i++) {
			if (level.revealed [i] == true) {
				sb.Append (level.wordChars[i]);
			} else {
				sb.Append ("_");
			}
		}
		word.text = sb.ToString ();

		Utils.MyLog(string.Format("'{0}'", word.text));
	}

	internal class Level  {

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

		public Level (string word) {
            
            Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));
			
            this.word = word.ToLower();
			this.wordChars = word.ToCharArray();
			revealed = new bool[this.wordChars.Length];

			noChars = new List<char>();
			okChars = new List<char>();

			foreach (var c in TileChar.chars) {
				if (this.word.IndexOf(c) == -1) 
					noChars.Add(c);
				else 
					okChars.Add(c);
			}
		}

		public bool HasChar (char c) {
			if (okChars.IndexOf(c) == -1) return false;

			var index = okChars.IndexOf (c);
			okChars.RemoveAt (index);

			for (var i = 0; i < wordChars.Length; i++) {
				if ( wordChars [i] == c ) {
					revealed [i] = true;
				}
			}
			return true;
		}

		public char[] GetHints () {

            Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

			var result = new List<char>();

			var correct = ROUNDS [roundIndex].x;
			var total = ROUNDS [roundIndex].y;

			if (correct > okChars.Count)
				correct = okChars.Count;

			var i = 0;
			while (result.Count < total) {
				if (result.Count < correct) {
					var c = okChars [Random.Range (0, okChars.Count)];
					if (!result.Contains (c)) {
						result.Add (c);
					}
				} else {
					var c = noChars [Random.Range (0, noChars.Count)];
					if (!result.Contains (c)) {
						result.Add (c);
					}
				}
			}

			roundIndex++;
			if (roundIndex == ROUNDS.Length)
				roundIndex = ROUNDS.Length - 1;
			
			return result.ToArray ();
		}
	}
}	
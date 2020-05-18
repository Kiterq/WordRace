﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Reflection;

public class HangmanPuzzleData : MonoBehaviour {


	private Dictionary<int, List<string>> puzzles;
	private int difficulty = 10;

	public void LoadData () {

        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

		StartCoroutine("LoadPuzzleData");
	}

	public string GetWord () {

        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

		var word = "";
		var list = puzzles [difficulty];
		word = list [UnityEngine.Random.Range (0, list.Count)];

		UpdateLevel ();


		return word;
	}

	public void UpdateLevel () {
		GetNextDifficulty();
	}

	private void GetNextDifficulty () {

        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

		for (var i = difficulty + 1; i < 1000; i++) {
			if (puzzles.ContainsKey (i)) {
				difficulty = i;
				return;
			}
		}
	}

	IEnumerator LoadPuzzleData () {

		Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

		string dataPath = System.IO.Path.Combine (Application.streamingAssetsPath, "hangman.txt");

		string result = null;

		if (dataPath.Contains ("://")) {
			WWW www = new WWW (dataPath);
			yield return www;
			result = www.text;
		} else
			result = System.IO.File.ReadAllText (dataPath);


		var data = result.Split ('\n');

		puzzles = new Dictionary<int, List<string>> ();

		foreach (var entry in data) {
			var e = entry.TrimEnd ();
			var d = e.Split ('|');
			if (d.Length == 2) {
				var key = Int32.Parse (d [0]);
				if (key > 2) {
					if (!puzzles.ContainsKey (key))
						puzzles.Add (key, new List<string> ());
					puzzles [key].Add (d [1]);
				}
			}
		}

		////foreach (var d in data)
		////{
		////	Utils.MyLog(string.Format("LoadPuzzleData value {0}", d.ToString()));
		////}

		HangmanGameEvents.GameLoaded ();
	}
}

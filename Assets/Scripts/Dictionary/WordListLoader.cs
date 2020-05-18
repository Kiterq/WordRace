﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class WordListLoader : MonoBehaviour {

	public string fileName ="words/1k.txt";

	void Start () {
		StartCoroutine ("LoadWordList");
	}

    public IEnumerator LoadWordList () {

		string filePath = System.IO.Path.Combine (Application.streamingAssetsPath, fileName);
		string result = null;
		if (filePath.Contains ("://")) {
			WWW www = new WWW (filePath);
			yield return www;
			result = www.text;
		} else
			result = System.IO.File.ReadAllText (filePath);

		var words = result.Split ('\n');

		foreach (var w in words) {
			var word = w.TrimEnd ();
			if (string.IsNullOrEmpty (word))
				continue;
			Utils.MyLog(word);
		}
	}

    // Attempt to not use obsolete WWW
    public IEnumerator LoadWordList1()
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
        string result = null;
        if (filePath.Contains("://"))
        {
            UnityWebRequest www = new UnityWebRequest(filePath);
            yield return www;
            ////result = www.text;
        }
        else
            result = System.IO.File.ReadAllText(filePath);

        var words = result.Split('\n');

        foreach (var w in words)
        {
            var word = w.TrimEnd();
            if (string.IsNullOrEmpty(word))
                continue;
            Utils.MyLog(word);
        }
    }
}
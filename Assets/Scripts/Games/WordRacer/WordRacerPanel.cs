﻿using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class WordRacerPanel : MonoBehaviour, IInputHandler {

	public GameObject tileGO;

	public GameObject container;

	private List<TileButton> tiles;

	private TileButton selectedTile;


	void Awake () {
		tiles = new List<TileButton> ();
	}

	public void ShowPanel (char[] chars) {

        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

		ClearPanel();

		container.transform.localScale = Vector2.one;
		container.transform.position = Vector2.zero;

        foreach (var c in chars)
        {
            Utils.MyLog(string.Format("Char '{0}'", c.ToString()));
        }

        for (var i = 0; i < chars.Length; i++) {
			var go = Instantiate (tileGO) as GameObject;
			var tile = go.GetComponent<TileButton> ();
			tile.SetTileData (chars [i]);
			tile.SetColor (Color.black, Color.yellow);
			tile.transform.parent = container.transform;

			tile.transform.position = new Vector2 (
				(i * 3.5f),
				-1.5f
			);

			tiles.Add (tile);
		}

		var size = tiles [1].transform.position.x - tiles [0].transform.position.x;
		var scale = 1.0f;
		var panelWidth = (chars.Length -1) * size;
		var stageWidth = 3.5f;

		if (panelWidth > stageWidth ) {
			scale = stageWidth / panelWidth;
			container.transform.localScale = new Vector2(scale, scale);
		}

		container.transform.localPosition = new Vector2 ((panelWidth * scale) * -0.5f , -0.5f);
	}

	private void ClearPanel () {

        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

		foreach (var tile in tiles)
			Destroy (tile.gameObject);
		tiles.Clear ();
	}


	public void HandleTouchDown (Vector2 touch) 	{

        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

		if (selectedTile != null) {
			selectedTile.Select(false);
		}

		selectedTile = TileCloseToPoint (touch);

		if (selectedTile != null) {
			selectedTile.Select(true);
		}
	}
		
	public void HandleTouchUp (Vector2 touch) {

        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

		if (selectedTile != null) {
			selectedTile.Select(false);
			SubmitTile ();
		}
		selectedTile = null;
	}

	public void HandleTouchMove (Vector2 touch) {
	}

	private TileButton TileCloseToPoint (Vector2 point){

        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

		var t = Camera.main.ScreenToWorldPoint (point);
		t.z = 0;

		var minDistance = 0.6f;
		TileButton closestTile = null;
		foreach (var tile in tiles) {
			var distanceToTouch = Vector2.Distance (tile.transform.position, t);
			if (distanceToTouch < minDistance) {
				minDistance = distanceToTouch;
				closestTile = tile;
			}
		}
			
		return closestTile;
	}

	private void SubmitTile () {

        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

		WordRacerGameEvents.LetterSelected (selectedTile.TypeChar);
	}
}

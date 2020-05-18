﻿using UnityEngine;
using System;
using System.Collections;
using System.Reflection;

[RequireComponent(typeof(Collider2D))]
public class TileButton : TileChar {

	[HideInInspector]
	public bool selected;
	
	[HideInInspector]
	public bool touched;

	public void Select (bool value)
	{
        Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

		selected = value;

		if (selected) {
			SetColor (Color.white, Color.black);

		} else {
			SetColor (Color.black, Color.white);
			touched = false; 
		}
	}

	void OnMouseDown () {
		touched = true;
	}

	void OnMouseOver () {
		touched = true;
	}

	void OnMouseOut () {
		touched = false;
	}

	void OnMouseUp () {
		touched = false;
	}

		
}

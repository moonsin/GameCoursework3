﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diary1 : photos {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("f") && nearPhoto) {
			GameManager.instance.myplayer.playerRigidbody.velocity = new Vector2 (0f, 0f);
			GameManager.instance.myplayer.busy = true;
			GameManager.instance.GetComponent<IndicatorText> ().showKeyInformation ("key2", "key2_end");
			GameManager.instance.GetComponent<IndicatorText> ().showIndicatorImage("diary1");
		}

		base.Update ();
	}
}
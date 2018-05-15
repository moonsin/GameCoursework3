﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proof : photos {
	public bool talked = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("f") && nearPhoto) {
			GameManager.instance.myplayer.playerRigidbody.velocity = new Vector2 (0f, 0f);
			GameManager.instance.myplayer.busy = true;
			GameManager.instance.GetComponent<IndicatorText> ().showKeyInformation ("key3", "key3_end");
			GameManager.instance.GetComponent<IndicatorText> ().showIndicatorImage("proof");
		}

		if (Input.GetKeyDown ("q")) {
			if (GameManager.instance.GetComponent<IndicatorText> ().keyImageStuffText.enabled == true) {
				GameManager.instance.myplayer.busy = false;
				GameManager.instance.GetComponent<IndicatorText> ().hideIndicatorImage ();

				if (!talked) {
					talked = true;
					GameManager.instance.GetComponent<IndicatorText> ().showFollowingTalk ("key3found", "key3found_end");
				}

			}
		}
	}
}

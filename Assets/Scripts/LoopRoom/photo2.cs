using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class photo2 : photos {

	// Use this for initialization
	void Start () {
		
	}
		

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("f") && nearPhoto) {
			GameManager.instance.myplayer.busy = true;
			GameManager.instance.GetComponent<IndicatorText> ().showIndicatorImage("PhotoP2");
		}

		base.Update ();
	}
}
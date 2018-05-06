using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class photo3 : photos {
	private bool laughed = false;
	// Use this for initialization
	void Start () {
		
	}
		
	void unlockDoor(){
		GameObject.Find ("DoorL").GetComponent<LooproomDoor3> ().locked = false;
		SoundManager.instance.open_door_3.Play ();
	}

	// Update is called once per frame
	void Update () {
		//base.Update ();

		if (Input.GetKeyDown ("f") && nearPhoto) {
			GameManager.instance.myplayer.busy = true;
			GameManager.instance.GetComponent<IndicatorText> ().showIndicatorImage("PhotoP3");
		}

		if (Input.GetKeyDown ("q")) {
			if (GameManager.instance.GetComponent<IndicatorText> ().keyImageStuffText.enabled == true) {
				GameManager.instance.myplayer.busy = false;
				GameManager.instance.GetComponent<IndicatorText> ().hideIndicatorImage ();
				if (laughed == false) {
					SoundManager.instance.shaonvdexiao1.Play ();
					laughed = true;
					Invoke ("unlockDoor", 5f);
				}
			}
		}

	}
}

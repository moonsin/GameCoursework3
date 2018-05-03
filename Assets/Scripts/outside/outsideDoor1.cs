using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outsideDoor1 : commonDoor {

	// Use this for initialization
	void Start () {
		
	}


	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("f") && nearDoor) {
			nearDoor = false;
			Destroy (GameManager.instance.outsideInstance);
			GameManager.instance.GetComponent<IndicatorText> ().hideIndicator ();
			GameManager.instance.myplayer.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			GameManager.instance.myplayer.busy = true;
			GameManager.instance.inOutside = false;
			SoundManager.instance.door_creak_closing.Play ();
			GameManager.instance.showLoopRoom ();

		}
		
	}
}

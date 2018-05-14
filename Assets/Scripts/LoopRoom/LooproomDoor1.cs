using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooproomDoor1 : commonDoor {

	// Use this for initialization
	void Start () {
		
	}
		

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("f") && nearDoor) {
			nearDoor = false;
			GameManager.instance.myplayer.playerRigidbody.velocity = new Vector2 (0f, 0f);
			GameManager.instance.myplayer.busy = true;

			GameManager.instance.GetComponent<IndicatorText> ().hideIndicator ();
			Destroy (GameManager.instance.Looproom1Instance);

			GameManager.instance.myplayer.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			GameManager.instance.myplayer.transform.position =  new Vector3 (-18f, -1.5f);
			SoundManager.instance.door_creak_closing.Play ();
			GameManager.instance.showLoopRoom2 ();

		}
	}
}

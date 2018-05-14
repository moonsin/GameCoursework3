using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooproomDoor3 : commonDoor {



	// Use this for initialization
	void Start () {
		locked = true;
	}
		
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("f") && nearDoor && !locked) {
			nearDoor = false;
			GameManager.instance.myplayer.playerRigidbody.velocity = new Vector2 (0f, 0f);
			GameManager.instance.myplayer.busy = true;

			GameManager.instance.GetComponent<IndicatorText> ().hideIndicator ();
			Destroy (GameManager.instance.Looproom3Instance);

			GameManager.instance.myplayer.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			GameManager.instance.myplayer.transform.position = new Vector3 (-18f, -0.7f);
			SoundManager.instance.door_creak_closing.Play ();
			GameManager.instance.showHouseRoom ();

		}
	}
}

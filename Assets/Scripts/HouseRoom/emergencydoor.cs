using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emergencydoor : MonoBehaviour {

	public bool locked = true;
	public bool lockSoundShowed = false;
	protected bool nearDoor = false;


	// Use this for initialization
	void Start () {

	}

	protected void OnTriggerStay2D( Collider2D other){
		if (other.tag == "Player") {
			nearDoor = true;

			if (GameManager.instance.myplayer.hasKey == true) {
				locked = false;
			}

			if (locked) {
				if (!lockSoundShowed) {
					lockSoundShowed = true;
					SoundManager.instance.door_lock_1.Play ();
				}
				GameManager.instance.GetComponent<IndicatorText> ().showIndicator ("The door is locked");
			} else {
				GameManager.instance.GetComponent<IndicatorText> ().showIndicator ("Press F to open the door");
			}
		}
	}

	protected void OnTriggerExit2D( Collider2D other){
		nearDoor = false;
		lockSoundShowed = false;
		GameManager.instance.GetComponent<IndicatorText> ().hideIndicator ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("f") && nearDoor &&!locked) {
			nearDoor = false;
			GameManager.instance.myplayer.playerRigidbody.velocity = new Vector2 (0f, 0f);
			GameManager.instance.myplayer.busy = true;
			GameManager.instance.GetComponent<IndicatorText> ().hideIndicator ();
			GameManager.instance.myplayer.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			SoundManager.instance.door_creak_closing.Play ();

			GameManager.instance.GetComponent<IndicatorText> ().showHpText ();
		}
		
	}
}

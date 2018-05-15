using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commonDoor : MonoBehaviour {

	protected bool nearDoor = false;
	public bool locked = false;
	public bool lockSoundShowed = false;

	// Use this for initialization
	void Start () {
		
	}

	protected void OnTriggerStay2D( Collider2D other){
		if (other.tag == "Player") {
			nearDoor = true;

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
		
	}
}

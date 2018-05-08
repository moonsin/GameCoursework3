using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outsideDoor2 : MonoBehaviour {
	public bool lockSoundShowed = false;
	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerStay2D( Collider2D other){
		if (other.tag == "Player") {
			if (!lockSoundShowed) {
				lockSoundShowed = true;
				SoundManager.instance.door_lock_1.Play ();
			}
			GameManager.instance.GetComponent<IndicatorText> ().showIndicator ("This Door is locked");
		}
	}


	void OnTriggerExit2D( Collider2D other){
		lockSoundShowed = false;
		GameManager.instance.GetComponent<IndicatorText> ().hideIndicator ();
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}

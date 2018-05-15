using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour {
	
	protected bool nearPhoto = false;
	public bool alreadyGet = false;
	// Use this for initialization
	void Start () {
		
	}


	protected void OnTriggerStay2D( Collider2D other){
		if (other.tag == "Player" && !alreadyGet) {
			nearPhoto = true;
			GameManager.instance.GetComponent<IndicatorText> ().showIndicator ("Press F to get the key");
		}
	}

	protected void OnTriggerExit2D( Collider2D other){
		nearPhoto = false;
		GameManager.instance.GetComponent<IndicatorText> ().hideIndicator ();
	}

	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("f") && nearPhoto) {
			
			GameManager.instance.myplayer.playerRigidbody.velocity = new Vector2 (0f, 0f);
			GameManager.instance.myplayer.busy = true;

			GameManager.instance.GetComponent<IndicatorText> ().showKeyInformation ("tutorial4", "tutorial4_end");
			GameManager.instance.GetComponent<IndicatorText> ().showIndicatorImage("key");


			Instantiate (HouseroomManager.instance.GhostObj, GameObject.Find ("Ghosts").transform);
	

			alreadyGet = true;
			nearPhoto = false;

			GameManager.instance.myplayer.hasKey = true;
			GameManager.instance.GetComponent<IndicatorText> ().hideIndicator ();
		}

		if (Input.GetKeyDown ("q")) {
			if (GameManager.instance.GetComponent<IndicatorText> ().keyImageStuffText.enabled == true) {
				GameManager.instance.myplayer.busy = false;
				GameManager.instance.GetComponent<IndicatorText> ().hideIndicatorImage ();
			}
		}

	}
}

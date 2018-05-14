using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class photos : MonoBehaviour {
	protected bool nearPhoto = false;

	// Use this for initialization
	void Start () {
		
	}

	protected void OnTriggerStay2D( Collider2D other){
		if (other.tag == "Player") {
			nearPhoto = true;
			GameManager.instance.GetComponent<IndicatorText> ().showIndicator ("Press F to view the document");
		}
	}

	protected void OnTriggerExit2D( Collider2D other){
		nearPhoto = false;
		GameManager.instance.GetComponent<IndicatorText> ().hideIndicator ();
	}

	// Update is called once per frame
	protected void Update () {
		
		if (Input.GetKeyDown ("q")) {
			if (GameManager.instance.GetComponent<IndicatorText> ().keyImageStuffText.enabled == true) {
				GameManager.instance.myplayer.busy = false;
				GameManager.instance.GetComponent<IndicatorText> ().hideIndicatorImage ();
			}
		}
	}

}

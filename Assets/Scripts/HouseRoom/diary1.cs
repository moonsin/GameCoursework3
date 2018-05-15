using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diary1 : photos {
	public bool talked = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("f") && nearPhoto) {
			GameManager.instance.myplayer.playerRigidbody.velocity = new Vector2 (0f, 0f);
			GameManager.instance.myplayer.busy = true;
			GameManager.instance.GetComponent<IndicatorText> ().showKeyInformation ("key2", "key2_end");
			GameManager.instance.GetComponent<IndicatorText> ().showIndicatorImage("diary1");
		}

		if (Input.GetKeyDown ("q")) {
			if (GameManager.instance.GetComponent<IndicatorText> ().keyImageStuffText.enabled == true) {
				GameManager.instance.myplayer.busy = false;
				GameManager.instance.GetComponent<IndicatorText> ().hideIndicatorImage ();

				if (!talked) {
					talked = true;
					GameManager.instance.GetComponent<IndicatorText> ().showFollowingTalk ("key2found", "key2found_end");
				}

			}
		}
	}
}

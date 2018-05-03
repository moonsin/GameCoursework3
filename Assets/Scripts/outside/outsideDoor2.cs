using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outsideDoor2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerStay2D( Collider2D other){
		if (other.tag == "Player") {
			GameManager.instance.GetComponent<IndicatorText> ().showIndicator ("This Door is locked");
		}
	}


	void OnTriggerExit2D( Collider2D other){
		GameManager.instance.GetComponent<IndicatorText> ().hideIndicator ();
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}

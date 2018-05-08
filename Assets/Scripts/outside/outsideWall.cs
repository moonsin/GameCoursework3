using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class outsideWall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerStay2D( Collider2D other){
		if (other.tag == "Player") {
			GameManager.instance.GetComponent<IndicatorText> ().showIndicator ("One Wall Stopped you");
		}
	}

	void OnTriggerExit2D( Collider2D other){
		GameManager.instance.GetComponent<IndicatorText> ().hideIndicator ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

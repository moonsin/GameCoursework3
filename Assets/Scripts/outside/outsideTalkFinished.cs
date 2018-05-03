using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outsideTalkFinished : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void finished(){
		GameManager.instance.myplayer.busy = false;
		Destroy(GameObject.Find("OutsideTalkContent"));
		Destroy(GameObject.Find("TalkBackGround"));
		Destroy (GameObject.FindGameObjectWithTag ("Talk"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

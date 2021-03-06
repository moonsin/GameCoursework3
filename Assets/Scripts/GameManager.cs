﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public GameObject player;
	private StartStory startStoryScript;
	public Player myplayer;

	public GameObject outside;
	public GameObject outsideInstance;
	public bool inOutside = false;

	public GameObject Looproom1;
	public GameObject Looproom1Instance;
	public bool inLoopRoom = false;

	public GameObject Looproom2;
	public GameObject Looproom2Instance;
	public GameObject Looproom3;
	public GameObject Looproom3Instance;

	public bool inHouseRoom = false;


	void Start(){
		init ();
		startStoryScript.showBeginingStory ();
		//OutSideStart();
		//showHouseRoom();

	}

	// Use this for initialization

	void init(){
		
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		startStoryScript = GetComponent<StartStory>();
	}

	public void OutSideStart(){
		
		outsideInstance = Instantiate (outside) as GameObject;
		Instantiate (player);
		inOutside = true;
		this.GetComponent<IndicatorText>().showKeyInformation ("tutorial1", "tutorial1_end");

		this.myplayer.busy = true;
		this.transform.localScale = new Vector3 (0.9f, 0.9f);
	}

	public void showLoopRoom(){
		Invoke ("LoopRoomStart", 2.4f);
	}

	public void showLoopRoom2(){
		Invoke ("LoopRoom2Start", 2.4f);
	}

	public void showLoopRoom3(){
		Invoke ("LoopRoom3Start", 2.4f);
	}

	public void LoopRoomStart(){
		myplayer.transform.localScale = new Vector3 (1.2f, 1.2f);
		Looproom1Instance = Instantiate (Looproom1) as GameObject;
		myplayer.transform.position = new Vector3 (-18f, -1.5f);
		inLoopRoom = true;
		myplayer.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		myplayer.busy = false;

		this.GetComponent<IndicatorText> ().showFollowingTalk ("looproom1", "looproom1_end");
	}

	public void LoopRoom2Start(){
		Looproom2Instance = Instantiate (Looproom2) as GameObject;
		myplayer.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		myplayer.busy = false;
		this.GetComponent<IndicatorText> ().showFollowingTalk ("looproom2", "looproom2_end");
	}

	public void LoopRoom3Start(){
		Looproom3Instance = Instantiate (Looproom3) as GameObject;
		myplayer.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		myplayer.busy = false;
	}

	public void showHouseRoom(){
		//测试代码
		//Instantiate (player);
		//this.myplayer.busy = true;
		//myplayer.transform.localScale = new Vector3 (1.2f, 1.2f);
		//GameManager.instance.myplayer.transform.position = new Vector3 (-19f, -2f);
		//


		inLoopRoom = false;

		GetComponent<IndicatorText> ().initRoomInMap ();

		inHouseRoom = true;

		SoundManager.instance.rain1.Play ();

		HouseroomManager.instance.HouseRoomStart ();
		myplayer.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		this.GetComponent<IndicatorText>().showKeyInformation ("tutorial2", "tutorial2_end");
		myplayer.busy = false;
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
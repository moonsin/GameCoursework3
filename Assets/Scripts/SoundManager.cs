﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {
	public static SoundManager instance = null;
	public AudioSource door_creak_closing;
	public AudioSource shaonvdexiao1;
	public AudioSource open_door_3;

	// Use this for initialization
	void Start () {

		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}


		door_creak_closing = GameObject.Find ("Door_creak_closing").GetComponent<AudioSource> ();
		shaonvdexiao1 = GameObject.Find ("shaonvdexiao1").GetComponent<AudioSource> ();
		open_door_3 = GameObject.Find ("open_door_3").GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

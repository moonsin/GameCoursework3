using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {
	public static SoundManager instance = null;
	public AudioSource door_creak_closing;
	public AudioSource shaonvdexiao1;
	public AudioSource open_door_3;
	public AudioSource openWindow;
	public AudioSource openMap;
	public AudioSource door_lock_1;
	public AudioSource shaonvdexiao2;
	public AudioSource nextPage;
	public AudioSource nico;
	public AudioSource blood;
	public AudioSource cry;
	public AudioSource rain1;

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
		openWindow = GameObject.Find ("openWindow").GetComponent<AudioSource> ();
		openMap= GameObject.Find ("openMap").GetComponent<AudioSource> ();
		door_lock_1 = GameObject.Find ("door_lock_1").GetComponent<AudioSource> ();
		shaonvdexiao2 = GameObject.Find ("shaonvdexiao2").GetComponent<AudioSource> ();
		nextPage = GameObject.Find ("nextPage").GetComponent<AudioSource> ();
		nico = GameObject.Find ("nico").GetComponent<AudioSource> ();
		blood = GameObject.Find ("blood").GetComponent<AudioSource> ();
		cry = GameObject.Find ("cry").GetComponent<AudioSource> ();
		rain1 = GameObject.Find ("rain1").GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

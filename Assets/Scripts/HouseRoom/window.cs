using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class window : MonoBehaviour {


	private bool isOpen = false;
	public Animator animator; 

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isOpen) {
			if (GameManager.instance.myplayer.transform.position.x > this.transform.position.x-0.5f && GameManager.instance.myplayer.transform.position.x < this.transform.position.x +0.5f) {
				animator.SetTrigger ("windowOpen");
				isOpen = true;
				SoundManager.instance.openWindow.Play ();
			}
		}	
	}
}

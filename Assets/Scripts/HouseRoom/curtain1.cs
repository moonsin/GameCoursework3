using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class curtain1 : MonoBehaviour {

	private bool isOpen = false;
	public Animator animator; 
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isOpen) {
			if (GameManager.instance.myplayer.transform.position.x > this.transform.position.x-1f && GameManager.instance.myplayer.transform.position.x < this.transform.position.x + 1f) {
				animator.SetTrigger ("show");
				isOpen = true;
				SoundManager.instance.openWindow.Play ();
			}
		}	
	}
}

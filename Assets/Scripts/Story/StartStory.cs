using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartStory : MonoBehaviour {

	public GameObject beginingStoryCanvas;
	protected Text legend;
	protected Text story;
	protected Text skipText;
	protected bool Showstory = false;
	protected float transparency = 0f;
	protected  AudioSource BeginingAudio;

	// Use this for initialization
	void Start () {

	}

	public void showBeginingStory(){
		Instantiate (beginingStoryCanvas);
	}
		
	// Update is called once per frame
	void Update () {

	}
}

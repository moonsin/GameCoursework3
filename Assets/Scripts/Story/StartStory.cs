using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartStory : MonoBehaviour {

	public GameObject beginingStoryCanvas;
	private Text legend;
	private Text story;
	private bool Showstory = false;
	private float transparency = 0f;

	// Use this for initialization
	void Start () {
		Instantiate (beginingStoryCanvas);
		legend = GameObject.Find("legend").GetComponent<Text>();
		story = GameObject.Find("Story").GetComponent<Text>();
		legend.enabled = false;
		story.enabled = false;

		legend.color = new Color (255, 0, 0, transparency);
		legend.enabled = true;
		Showstory = true;

	}
	
	// Update is called once per frame
	void Update () {
		
		if (Showstory) {
			transparency += 0.005f;
			legend.color = new Color (255, 0, 0, transparency);
			if (transparency == 1f) {
				Showstory = false;
			}
		}
	}
}

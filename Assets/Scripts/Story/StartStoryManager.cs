using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartStoryManager : StartStory {

	// Use this for initialization
	void Awake () {
		legend = GameObject.Find("legend").GetComponent<Text>();
		story = GameObject.Find("Story").GetComponent<Text>();
		skipText = GameObject.Find("skipText").GetComponent<Text>();
		BeginingAudio = GameObject.Find ("background").GetComponent<AudioSource> ();
		warning = GameObject.Find ("warning");

		legend.enabled = false;
		story.enabled = false;
		skipText.enabled = false;

		legend.color = new Color (255, 0, 0, transparency);

		Invoke ("showLegend", 12f);

	}

	void showLegend(){
		warning.SetActive (false);
		legend.enabled = true;
		Showstory = true;
		BeginingAudio.Play ();

		Invoke ("showStory", 7f);
	}

	void showStory(){
		legend.enabled = false;
		story.enabled = true;
		Invoke ("BeginingStoryFinished", 7f);
	}

	protected void BeginingStoryFinished(){
		Destroy(GameObject.FindGameObjectWithTag("BeginingStory"));
		this.GetComponent<StartStory> ().enabled = false;
		GameManager.instance.OutSideStart ();
	}

	
	// Update is called once per frame
	void Update () {

		if (Showstory && legend) {
			transparency += 0.005f;
			legend.color = new Color (255, 0, 0, transparency);
			if (transparency == 1f) {
				Showstory = false;
			}
		}

		if (Input.anyKeyDown) {
			skipText.enabled = true;
			if (skipText.enabled == true && Input.GetKeyDown ("space")) {
				BeginingStoryFinished ();
			}
		}
	}
}

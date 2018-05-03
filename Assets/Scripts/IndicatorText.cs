using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorText : MonoBehaviour {
	public Text indicator;
	public Image IndicatorImage;
	public Text keyImageStuffText;
	public GameObject FollowTalk;
	public bool FollowTalkShowed = false;

	// Use this for initialization
	void Start () {
		indicator = GameObject.Find ("IndicatorText").GetComponent<Text> ();
		indicator.enabled = false;

		IndicatorImage = GameObject.Find ("keyImageStuff").GetComponent<Image> ();
		IndicatorImage.enabled = false;

		keyImageStuffText = GameObject.Find ("keyImageStuffText").GetComponent<Text> ();
		keyImageStuffText.enabled = false;

		FollowTalk = GameObject.Find ("FollowTalk");
		FollowTalk.SetActive (false);

	}

	public void showIndicator(string massage){
		print (massage);
		indicator.text = massage;

		Vector3 newPos = Camera.main.WorldToScreenPoint (GameManager.instance.myplayer.transform.position);
		newPos.y += 220f;
		indicator.transform.position = newPos;
		indicator.enabled = true;
	}

	public void hideIndicator(){
		indicator.enabled = false;
	}

	public void showIndicatorImage(string imageName){
		IndicatorImage.sprite = Resources.Load<Sprite> (imageName);
		IndicatorImage.enabled = true;
		keyImageStuffText.enabled = true;
	}

	public void hideIndicatorImage(){
		IndicatorImage.enabled = false;
		keyImageStuffText.enabled = false;
	}

	public void showFollowingTalk(string startTitle, string endTitle){
		FollowTalk.SetActive (true);
		FollowTalkShowed = true;
		FollowTalk.GetComponent<RPGTalk> ().NewTalk (startTitle, endTitle);

	}

	public void hideFollowingTalk(){
		FollowTalkShowed = false;
		FollowTalk = GameObject.Find ("FollowTalk");
		FollowTalk.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (FollowTalkShowed) {
			Vector3 newPos = Camera.main.WorldToScreenPoint (GameManager.instance.myplayer.transform.position);
			newPos.y += 190f;
			newPos.x += 140f;
			FollowTalk.transform.position = newPos;
		}
	}
}

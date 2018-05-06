using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorText : MonoBehaviour {

	public Text indicator;
	public Image IndicatorImage;
	public Text keyImageStuffText;
	public GameObject FollowTalk;

	public GameObject PlayerMap;
	public bool FollowTalkShowed = false;
	public bool PlayerMapShowed = false;

	public GameObject newRoomInMap;
	public GameObject newRoomInMapName;
	public GameObject mapLine;

	public GameObject MapPlayer;


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

		MapPlayer = GameObject.Find ("MapPlayer");
		PlayerMap = GameObject.Find ("PlayerMap");
		PlayerMap.SetActive (false);



	}

	public void showIndicator(string massage){
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

	public void showPlayerMap(){
		
		GameManager.instance.myplayer.playerRigidbody.velocity = new Vector2 (0f, 0f);
		GameManager.instance.myplayer.busy = true;

		PlayerMapShowed = true;
		PlayerMap.SetActive (true);

		SoundManager.instance.openMap.Play ();

	}

	public void hidePlayerMap(){
		GameManager.instance.myplayer.busy = false;
		PlayerMapShowed = false;
		PlayerMap.SetActive (false);
	}

	public void initRoomInMap(){

		//测试代码
		if (PlayerMap == null) {
			PlayerMap = GameObject.Find ("PlayerMap");
			MapPlayer = GameObject.Find ("MapPlayer");
		}

		MapPlayer.GetComponent<Image>().sprite = Resources.Load<Sprite> ("playerArrow0");
		newRoomInMap.GetComponent<Image>().sprite = Resources.Load<Sprite> ("3-0");

		GameObject room = Instantiate (newRoomInMap,PlayerMap.transform) as GameObject;
		room.GetComponent< RectTransform>().anchoredPosition3D = new Vector3(0.0f, 0.0f, 0.0f); 


	}

	//newRoomRalativePosition: 1:在旧左面 2:在旧房间右面 3:在旧房间下面 4:在旧房间上面
	public void addNewRoomInMap(HouseRoom newroom, int newRoomRalativePosition){

		if (newRoomRalativePosition == 1) {
			
			mapLine.GetComponent<Image>().sprite = Resources.Load<Sprite> ("line0");
			GameObject line = Instantiate (mapLine,PlayerMap.transform) as GameObject;
			line.GetComponent< RectTransform> ().sizeDelta = new Vector2 (100f, 50f);
			line.GetComponent< RectTransform>().anchoredPosition3D = 
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition.x -85f, MapPlayer.GetComponent< RectTransform>().anchoredPosition.y, 0.0f);  


			newRoomInMap.GetComponent<Image>().sprite = Resources.Load<Sprite> (newroom.doorNumber.ToString() + "-" + newroom.direction.ToString());
			GameObject room = Instantiate (newRoomInMap,PlayerMap.transform) as GameObject;
			room.GetComponent< RectTransform>().anchoredPosition3D = 
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition.x - 165f, MapPlayer.GetComponent< RectTransform>().anchoredPosition.y, 0.0f);


			MapPlayer.GetComponent< RectTransform>().anchoredPosition = 	
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition.x - 165f, MapPlayer.GetComponent< RectTransform>().anchoredPosition.y, 0.0f);
			
		}

		if (newRoomRalativePosition == 2) {

			mapLine.GetComponent<Image>().sprite = Resources.Load<Sprite> ("line0");
			GameObject line = Instantiate (mapLine,PlayerMap.transform) as GameObject;
			line.GetComponent< RectTransform> ().sizeDelta = new Vector2 (100f, 50f);
			line.GetComponent< RectTransform>().anchoredPosition3D = 
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition.x +85f, MapPlayer.GetComponent< RectTransform>().anchoredPosition.y, 0.0f);  


			newRoomInMap.GetComponent<Image>().sprite = Resources.Load<Sprite> (newroom.doorNumber.ToString() + "-" + newroom.direction.ToString());
			GameObject room = Instantiate (newRoomInMap,PlayerMap.transform) as GameObject;
			room.GetComponent< RectTransform>().anchoredPosition3D = 
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition.x + 165f, MapPlayer.GetComponent< RectTransform>().anchoredPosition.y, 0.0f);


			MapPlayer.GetComponent< RectTransform>().anchoredPosition = 	
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition.x + 165f, MapPlayer.GetComponent< RectTransform>().anchoredPosition.y, 0.0f);
		}

		if (newRoomRalativePosition == 3) {

			mapLine.GetComponent<Image>().sprite = Resources.Load<Sprite> ("line1");
			GameObject line = Instantiate (mapLine,PlayerMap.transform) as GameObject;
			line.GetComponent< RectTransform> ().sizeDelta = new Vector2 (50, 100f);
			line.GetComponent< RectTransform>().anchoredPosition3D = 
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition.x, MapPlayer.GetComponent< RectTransform>().anchoredPosition.y - 80, 0.0f);  


			newRoomInMap.GetComponent<Image>().sprite = Resources.Load<Sprite> (newroom.doorNumber.ToString() + "-" + newroom.direction.ToString());
			GameObject room = Instantiate (newRoomInMap,PlayerMap.transform) as GameObject;
			room.GetComponent< RectTransform>().anchoredPosition3D = 
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition.x, MapPlayer.GetComponent< RectTransform>().anchoredPosition.y - 170f, 0.0f);


			MapPlayer.GetComponent< RectTransform>().anchoredPosition = 	
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition.x, MapPlayer.GetComponent< RectTransform>().anchoredPosition.y - 170f, 0.0f);
		}

		if (newRoomRalativePosition == 4) {

			mapLine.GetComponent<Image>().sprite = Resources.Load<Sprite> ("line1");
			GameObject line = Instantiate (mapLine,PlayerMap.transform) as GameObject;
			line.GetComponent< RectTransform> ().sizeDelta = new Vector2 (50, 100f);
			line.GetComponent< RectTransform>().anchoredPosition3D = 
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition.x, MapPlayer.GetComponent< RectTransform>().anchoredPosition.y + 90f, 0.0f);  


			newRoomInMap.GetComponent<Image>().sprite = Resources.Load<Sprite> (newroom.doorNumber.ToString() + "-" + newroom.direction.ToString());
			GameObject room = Instantiate (newRoomInMap,PlayerMap.transform) as GameObject;
			room.GetComponent< RectTransform>().anchoredPosition3D = 
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition.x , MapPlayer.GetComponent< RectTransform>().anchoredPosition.y + 170f, 0.0f);


			MapPlayer.GetComponent< RectTransform>().anchoredPosition = 	
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition.x ,MapPlayer.GetComponent< RectTransform>().anchoredPosition.y +170f, 0.0f);
		}

		MapPlayer.GetComponent<Image>().sprite = Resources.Load<Sprite> ("playerArrow" + newroom.direction.ToString());

	}



	// Update is called once per frame
	void Update () {
		
		if (FollowTalkShowed) {
			Vector3 newPos = Camera.main.WorldToScreenPoint (GameManager.instance.myplayer.transform.position);
			newPos.y += 190f;
			newPos.x += 140f;
			FollowTalk.transform.position = newPos;
		}

		if (Input.GetKeyDown ("m") && !PlayerMapShowed) {
			showPlayerMap();
		}else if (Input.GetKeyDown ("m") && PlayerMapShowed) {
			hidePlayerMap();
		}
			
	}
}

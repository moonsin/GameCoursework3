using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorText : MonoBehaviour {

	public Text indicator;
	public Image IndicatorImage;
	public Text keyImageStuffText;
	public GameObject FollowTalk;
	public GameObject keyInformation;

	public GameObject PlayerMap;
	public bool FollowTalkShowed = false;
	public bool PlayerMapShowed = false;

	public GameObject newRoomInMap;
	public GameObject newRoomInMapName;
	public GameObject mapLine;
	public GameObject Ghost;
	public int GhostIndex = 0;
	public GameObject[] MapGhosts = new GameObject[50];

	public int[,] ConnectedMetrix = new int[100, 100];

	const string HORIZONTAL = "Horizontal";
	const string VERTICAL = "Vertical";

	public float horizontalDirection;//-1 to 1
	public float VerticalDirection;//-1 to 1

	public Image Ghosthappen1;
	public Image Ghosthappen2;
	public Image Ghosthappen3;
	public Image Ghosthappen4;
	public Image Ghosthappen5;
	public Image Ghosthappen6;
	private bool hideGhost = false;
	public float Ghosttransparency =255f;

	public Image blackCurtain;

	public GameObject MapPlayer;
	public Text SanValue;


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

		keyInformation = GameObject.Find ("keyInformation");
		keyInformation.SetActive (false);

		MapPlayer = GameObject.Find ("MapPlayer");
		PlayerMap = GameObject.Find ("PlayerMap");
		PlayerMap.SetActive (false);

		Ghosthappen1 = GameObject.Find ("Ghosthappen1").GetComponent<Image> ();
		Ghosthappen1.enabled = false;

		Ghosthappen2 = GameObject.Find ("Ghosthappen2").GetComponent<Image> ();
		Ghosthappen2.enabled = false;

		Ghosthappen3 = GameObject.Find ("Ghosthappen3").GetComponent<Image> ();
		Ghosthappen3.enabled = false;

		Ghosthappen4 = GameObject.Find ("Ghosthappen4").GetComponent<Image> ();
		Ghosthappen4.enabled = false;

		Ghosthappen5 = GameObject.Find ("Ghosthappen5").GetComponent<Image> ();
		Ghosthappen5.enabled = false;

		Ghosthappen6 = GameObject.Find ("Ghosthappen6").GetComponent<Image> ();
		Ghosthappen6.enabled = false;

		SanValue = GameObject.Find ("SanValue").GetComponent<Text> ();

		blackCurtain = GameObject.Find ("blackCurtain").GetComponent<Image> ();
		blackCurtain.enabled = false;
	}

	public void updateGhostsInMap(){

		for (int i = 0; i < MapGhosts.Length; i++) {
			Destroy (MapGhosts [i]);
			GhostIndex = 0;
		}

		for(int i = 0 ; i < HouseroomManager.instance.Ghosts.Count;i++){
			print (HouseroomManager.instance.Ghosts [i]);
			if (HouseroomManager.instance.Ghosts[i] != null) {
				GameObject GhostInstance = Instantiate (Ghost, PlayerMap.transform) as GameObject;
				MapGhosts [GhostIndex] = GhostInstance;
				GhostIndex += 1;
				GhostInstance.GetComponent< RectTransform>().anchoredPosition3D = 
					new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.x + 
						(215f * (HouseroomManager.instance.Ghosts[i].GhostPosition[0] - 
							GameObject.FindGameObjectWithTag("CurrentRoom").GetComponent<HouseRoom>().RoomPosition[0])), 
						MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.y +
						(201f * (HouseroomManager.instance.Ghosts[i].GhostPosition[1] - 
							GameObject.FindGameObjectWithTag("CurrentRoom").GetComponent<HouseRoom>().RoomPosition[1])), 0.0f);
			}
		}
	}

	public void initMapPlayer(){
	
		for (int x = 0; x < 100; x++) {
			for (int y = 0; y < 100; y++) {
				ConnectedMetrix [x, y] = 0;
			}
		}
		
	}

	public void showIndicator(string massage){
		indicator.text = massage;

		Vector3 newPos = Camera.main.WorldToScreenPoint (GameManager.instance.myplayer.transform.position);
		newPos.y += 290f;
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
		

	public void showKeyInformation(string startTitle, string endTitle){
		keyInformation.SetActive (true);
		keyInformation.GetComponent<RPGTalk> ().NewTalk (startTitle, endTitle);
	}

	public void hideKeyInformation(){
		keyInformation = GameObject.Find ("keyInformation");
		keyInformation.SetActive (false);
	}

	public void showFollowingTalk(string startTitle, string endTitle){
		
		Vector3 newPos = Camera.main.WorldToScreenPoint (GameManager.instance.myplayer.transform.position);
		newPos.y += 190f;
		newPos.x += 140f;
		FollowTalk.transform.position = newPos;

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
	public void addNewRoomInMap(int newRoomIndex, int currentRoomIndex,HouseRoom newroom, int newRoomRalativePosition){
		newRoomInMapName.GetComponent<Text>().text = newroom.name;

		if (newRoomRalativePosition == 1) {
			
			mapLine.GetComponent<Image>().sprite = Resources.Load<Sprite> ("line0");
			GameObject line = Instantiate (mapLine,PlayerMap.transform) as GameObject;
			line.GetComponent< RectTransform> ().sizeDelta = new Vector2 (100f, 50f);
			line.GetComponent< RectTransform>().anchoredPosition3D = 
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.x -105f, MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.y, 0.0f);  


			newRoomInMap.GetComponent<Image>().sprite = Resources.Load<Sprite> (newroom.doorNumber.ToString() + "-" + newroom.direction.ToString());
			GameObject room = Instantiate (newRoomInMap,PlayerMap.transform) as GameObject;
			room.GetComponent< RectTransform>().anchoredPosition3D = 
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.x - 215f, MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.y, 0.0f);


			MapPlayer.GetComponent< RectTransform>().anchoredPosition3D = 	
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.x - 215f, MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.y, 0.0f);


			GameObject roomname = Instantiate (newRoomInMapName ,room.transform) as GameObject;
		}

		if (newRoomRalativePosition == 2) {

			mapLine.GetComponent<Image>().sprite = Resources.Load<Sprite> ("line0");
			GameObject line = Instantiate (mapLine,PlayerMap.transform) as GameObject;
			line.GetComponent< RectTransform> ().sizeDelta = new Vector2 (100f, 50f);
			line.GetComponent< RectTransform>().anchoredPosition3D = 
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.x +105f, MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.y, 0.0f);  


			newRoomInMap.GetComponent<Image>().sprite = Resources.Load<Sprite> (newroom.doorNumber.ToString() + "-" + newroom.direction.ToString());
			GameObject room = Instantiate (newRoomInMap,PlayerMap.transform) as GameObject;
			room.GetComponent< RectTransform>().anchoredPosition3D = 
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.x + 215f, MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.y, 0.0f);


			MapPlayer.GetComponent< RectTransform>().anchoredPosition3D = 	
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.x + 215f, MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.y, 0.0f);

			GameObject roomname = Instantiate (newRoomInMapName ,room.transform) as GameObject;

		}

		if (newRoomRalativePosition == 3) {

			mapLine.GetComponent<Image>().sprite = Resources.Load<Sprite> ("line1");
			GameObject line = Instantiate (mapLine,PlayerMap.transform) as GameObject;
			line.GetComponent< RectTransform> ().sizeDelta = new Vector2 (50, 100f);
			line.GetComponent< RectTransform>().anchoredPosition3D = 
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.x, MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.y - 110, 0.0f);  


			newRoomInMap.GetComponent<Image>().sprite = Resources.Load<Sprite> (newroom.doorNumber.ToString() + "-" + newroom.direction.ToString());
			GameObject room = Instantiate (newRoomInMap,PlayerMap.transform) as GameObject;
			room.GetComponent< RectTransform>().anchoredPosition3D = 
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.x, MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.y - 210f, 0.0f);


			MapPlayer.GetComponent< RectTransform>().anchoredPosition3D = 	
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.x, MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.y - 210f, 0.0f);
		
			GameObject roomname = Instantiate (newRoomInMapName ,room.transform) as GameObject;
		}

		if (newRoomRalativePosition == 4) {

			mapLine.GetComponent<Image>().sprite = Resources.Load<Sprite> ("line1");
			GameObject line = Instantiate (mapLine,PlayerMap.transform) as GameObject;
			line.GetComponent< RectTransform> ().sizeDelta = new Vector2 (50, 100f);
			line.GetComponent< RectTransform>().anchoredPosition3D = 
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.x, MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.y + 110f, 0.0f);  


			newRoomInMap.GetComponent<Image>().sprite = Resources.Load<Sprite> (newroom.doorNumber.ToString() + "-" + newroom.direction.ToString());
			GameObject room = Instantiate (newRoomInMap,PlayerMap.transform) as GameObject;
			room.GetComponent< RectTransform>().anchoredPosition3D = 
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.x , MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.y + 210f, 0.0f);


			MapPlayer.GetComponent< RectTransform>().anchoredPosition3D = 	
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.x ,MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.y +210f, 0.0f);
		
			GameObject roomname = Instantiate (newRoomInMapName ,room.transform) as GameObject;
		}

		ConnectedMetrix [currentRoomIndex, newRoomIndex] = 1;
		ConnectedMetrix [newRoomIndex, currentRoomIndex] = 1;


		MapPlayer.GetComponent<Image>().sprite = Resources.Load<Sprite> ("playerArrow" + newroom.direction.ToString());

	}

	public void connectedToRoomInMap(int newRoomIndex, int currentRoomIndex, int newRoomRalativePosition, HouseRoom newroom){


		if (ConnectedMetrix [currentRoomIndex, newRoomIndex] == 0) {
			
			ConnectedMetrix [currentRoomIndex, newRoomIndex] = 1;
			ConnectedMetrix [newRoomIndex, currentRoomIndex] = 1;

		}



		if (newRoomRalativePosition == 1) {

			mapLine.GetComponent<Image>().sprite = Resources.Load<Sprite> ("line0");
			GameObject line = Instantiate (mapLine,PlayerMap.transform) as GameObject;
			line.GetComponent< RectTransform> ().sizeDelta = new Vector2 (100f, 50f);
			line.GetComponent< RectTransform>().anchoredPosition3D = 
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.x -105f, MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.y, 0.0f);  
			

			MapPlayer.GetComponent< RectTransform>().anchoredPosition3D = 	
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.x - 215f, 
					MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.y);

		}

		if (newRoomRalativePosition == 2) {

			mapLine.GetComponent<Image>().sprite = Resources.Load<Sprite> ("line0");
			GameObject line = Instantiate (mapLine,PlayerMap.transform) as GameObject;
			line.GetComponent< RectTransform> ().sizeDelta = new Vector2 (100f, 50f);
			line.GetComponent< RectTransform>().anchoredPosition3D = 
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.x +105f, MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.y, 0.0f);  
			
			MapPlayer.GetComponent< RectTransform>().anchoredPosition3D = 	
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.x + 215f, 
					MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.y);



		}

		if (newRoomRalativePosition == 3) {

			mapLine.GetComponent<Image>().sprite = Resources.Load<Sprite> ("line1");
			GameObject line = Instantiate (mapLine,PlayerMap.transform) as GameObject;
			line.GetComponent< RectTransform> ().sizeDelta = new Vector2 (50, 100f);
			line.GetComponent< RectTransform>().anchoredPosition3D = 
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.x, MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.y - 110f, 0.0f);  
			

			MapPlayer.GetComponent< RectTransform>().anchoredPosition3D = 	
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.x,
					MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.y - 210f);
		
		}

		if (newRoomRalativePosition == 4) {

			mapLine.GetComponent<Image>().sprite = Resources.Load<Sprite> ("line1");
			GameObject line = Instantiate (mapLine,PlayerMap.transform) as GameObject;
			line.GetComponent< RectTransform> ().sizeDelta = new Vector2 (50, 100f);
			line.GetComponent< RectTransform>().anchoredPosition3D = 
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.x, MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.y + 110f, 0.0f);  
			

			print (MapPlayer);
			MapPlayer.GetComponent< RectTransform>().anchoredPosition3D = 	
				new Vector3(MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.x ,
					MapPlayer.GetComponent< RectTransform>().anchoredPosition3D.y +210f);
		
		}

	
		MapPlayer.GetComponent<Image>().sprite = Resources.Load<Sprite> ("playerArrow" + newroom.direction.ToString());


	}

	public void showGhostHappen(){
		GameManager.instance.myplayer.playerRigidbody.velocity = new Vector2 (0f, 0f);
		GameManager.instance.myplayer.busy = true;
		Ghosthappen1.enabled = true;
		GameObject.Find ("unstable1").GetComponent<AudioSource> ().Play();
		Ghosthappen6.color = new Color (255, 255, 255,  255);
		Ghosthappen6.GetComponent<Animator> ().SetTrigger ("finishi");
		Invoke ("showGhostHappen2", 1.5f);
	}

	void showGhostHappen2(){
		SoundManager.instance.nextPage.Play ();
		SoundManager.instance.nico.Play ();
		Ghosthappen2.enabled = true;

		Invoke ("showGhostHappen3", 1.5f);
	}

	void showGhostHappen3(){
		SoundManager.instance.nextPage.Play ();
		Ghosthappen3.enabled = true;
		Invoke ("showGhostHappen4", 1.5f);
	}

	void showGhostHappen4(){
		SoundManager.instance.nextPage.Play ();
		SoundManager.instance.shaonvdexiao2.Play ();
		Ghosthappen4.enabled = true;
		Invoke ("showGhostHappen5", 2f);
	}
	void showGhostHappen5(){
		SoundManager.instance.nextPage.Play ();
		Ghosthappen5.enabled = true;
		Ghosthappen6.enabled = true;
		Ghosthappen6.GetComponent<Animator> ().SetTrigger ("hide");
		Invoke ("hideGhostHappen", 4f);
	}
	void hideGhostHappen(){
		Ghosthappen1.enabled = false;
		Ghosthappen2.enabled = false;
		Ghosthappen3.enabled = false;
		Ghosthappen4.enabled = false;
		Ghosthappen5.enabled = false;
		Ghosthappen6.enabled = false;
		GameManager.instance.myplayer.busy = false;
	}

	public void showHpText(){
		blackCurtain.enabled = true;
		showKeyInformation ("hpend", "hpend_end");
	}

	public void showBdText(){
		blackCurtain.enabled = true;
		showKeyInformation ("bdend", "bdend_end");
	}



	// Update is called once per frame
	void Update () {

		if (HouseroomManager.instance.attackPlayer == true) {
			HouseroomManager.instance.attackPlayer = false;
			showGhostHappen ();
			GameManager.instance.myplayer.SAN -= 20;
			SanValue.text = "SAN : " + GameManager.instance.myplayer.SAN.ToString ();
		}
		
		if (FollowTalkShowed) {
			Vector3 newPos = Camera.main.WorldToScreenPoint (GameManager.instance.myplayer.transform.position);
			newPos.y += 290f;
			newPos.x += 160f;
			FollowTalk.transform.position = newPos;
		}

		if (Input.GetKeyDown ("m") && !PlayerMapShowed) {
			showPlayerMap();
		}else if (Input.GetKeyDown ("m") && PlayerMapShowed) {
			hidePlayerMap();
		}

		VerticalDirection = Input.GetAxis (VERTICAL);
		horizontalDirection = Input.GetAxis (HORIZONTAL);

		if (VerticalDirection!=0) {
			GameObject[] MapObjects = GameObject.FindGameObjectsWithTag ("MapObject");
			GameObject[] GhostMapObjects = GameObject.FindGameObjectsWithTag ("Ghost");

			if (VerticalDirection >0.5) {
				for (int i = 0; i < MapObjects.Length; i++) {
					MapObjects [i].GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (MapObjects [i].GetComponent<RectTransform> ().anchoredPosition3D.x, MapObjects [i].GetComponent<RectTransform> ().anchoredPosition3D.y + 5f);
				} 
				for (int i = 0; i < GhostMapObjects.Length; i++) {
					GhostMapObjects [i].GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (GhostMapObjects [i].GetComponent<RectTransform> ().anchoredPosition3D.x, GhostMapObjects [i].GetComponent<RectTransform> ().anchoredPosition3D.y + 5f);
				} 
			}else if (VerticalDirection <-0.5) {
				for (int i = 0; i < MapObjects.Length; i++) {
					MapObjects [i].GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (MapObjects [i].GetComponent<RectTransform> ().anchoredPosition3D.x, MapObjects [i].GetComponent<RectTransform> ().anchoredPosition3D.y - 5f);
				} 
				for (int i = 0; i < GhostMapObjects.Length; i++) {
					GhostMapObjects [i].GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (GhostMapObjects [i].GetComponent<RectTransform> ().anchoredPosition3D.x, GhostMapObjects [i].GetComponent<RectTransform> ().anchoredPosition3D.y - 5f);
				} 
			}
				
		}

		if (horizontalDirection != 0) {
			GameObject[] MapObjects = GameObject.FindGameObjectsWithTag ("MapObject");
			GameObject[] GhostMapObjects = GameObject.FindGameObjectsWithTag ("Ghost");
			if (horizontalDirection >0.5) {
				for (int i = 0; i < MapObjects.Length; i++) {
					MapObjects [i].GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (MapObjects [i].GetComponent<RectTransform> ().anchoredPosition3D.x +5f, MapObjects [i].GetComponent<RectTransform> ().anchoredPosition3D.y);
				} 

				for (int i = 0; i < GhostMapObjects.Length; i++) {
					GhostMapObjects [i].GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (GhostMapObjects [i].GetComponent<RectTransform> ().anchoredPosition3D.x +5f, GhostMapObjects [i].GetComponent<RectTransform> ().anchoredPosition3D.y);
				} 

			}else if (horizontalDirection <-0.5) {
				for (int i = 0; i < MapObjects.Length; i++) {
					MapObjects [i].GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (MapObjects [i].GetComponent<RectTransform> ().anchoredPosition3D.x -5f, MapObjects [i].GetComponent<RectTransform> ().anchoredPosition3D.y);
				} 
				for (int i = 0; i < GhostMapObjects.Length; i++) {
					GhostMapObjects [i].GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (GhostMapObjects [i].GetComponent<RectTransform> ().anchoredPosition3D.x -5f, GhostMapObjects [i].GetComponent<RectTransform> ().anchoredPosition3D.y);
				} 
			}
		
		}
			
	}
}

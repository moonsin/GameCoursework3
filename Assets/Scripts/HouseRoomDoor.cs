using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseRoomDoor : MonoBehaviour {
	
	protected bool nearDoor = false;
	public GameObject NearRoom;
	public bool locked = false;
	public bool isConnectedtoRoom =false;
	public GameObject CurrentRoom;
	string ConnectedDoorTag;
	public bool changingRoom = false;
	public bool lockSoundShowed = false;
	public bool isNearRoom = false;
	public bool showTutorial3 = false;
	public GameObject belongRoom;

	// Use this for initialization
	void Start () {
		CurrentRoom = GameObject.FindGameObjectWithTag ("CurrentRoom");
		HouseroomManager.instance.addDoorTolist (this);
		belongRoom = GameObject.FindGameObjectWithTag ("CurrentRoom");
	}

	//return doorTag
	string checkConnected(){
		CurrentRoom = GameObject.FindGameObjectWithTag ("CurrentRoom");
		if (this.tag == "DoorL" || this.tag == "DoorR") {
			if (NearRoom.GetComponent<HouseRoom> ().direction == CurrentRoom.GetComponent<HouseRoom> ().direction) {
				if (this.tag == "DoorL" && NearRoom.GetComponent<HouseRoom> ().HasRightDoor == 1) {
					return "DoorR";
				} 

				if (this.tag == "DoorR" && NearRoom.GetComponent<HouseRoom> ().HasLeftDoor == 1) {
					return "DoorL";
				} 
			} else {

				if ((CurrentRoom.GetComponent<HouseRoom> ().direction == 0 && NearRoom.GetComponent<HouseRoom> ().direction == 1) ||
				    (CurrentRoom.GetComponent<HouseRoom> ().direction == 1 && NearRoom.GetComponent<HouseRoom> ().direction == 0) ||
				    (CurrentRoom.GetComponent<HouseRoom> ().direction == 2 && NearRoom.GetComponent<HouseRoom> ().direction == 3) ||
				    (CurrentRoom.GetComponent<HouseRoom> ().direction == 3 && NearRoom.GetComponent<HouseRoom> ().direction == 2)) {
					if (this.tag == "DoorL" && NearRoom.GetComponent<HouseRoom> ().HasLeftDoor == 1) {
						return "DoorL";
					}
					if (this.tag == "DoorR" && NearRoom.GetComponent<HouseRoom> ().HasRightDoor == 1) {
						return "DoorR";
					}
				} else{
					if (NearRoom.GetComponent<HouseRoom> ().HasTopDoor == 1) {

						//if (CurrentRoom.GetComponent<HouseRoom> ().direction == 1 && NearRoom.GetComponent<HouseRoom> ().direction == 2 && this.tag == "DoorR") {
						//	return null;
						//}

						return "DoorU";
					
					}
				}
			}
		} else {
			if (NearRoom.GetComponent<HouseRoom> ().direction == CurrentRoom.GetComponent<HouseRoom> ().direction) {
				return null;
			} else {
				
				if (CurrentRoom.GetComponent<HouseRoom> ().direction == 0 && 
					NearRoom.GetComponent<HouseRoom> ().direction == 2&&
				    NearRoom.GetComponent<HouseRoom> ().HasLeftDoor == 1) {
					return "DoorL";
				}

				if (CurrentRoom.GetComponent<HouseRoom> ().direction == 0 &&
				   NearRoom.GetComponent<HouseRoom> ().direction == 1 &&
					NearRoom.GetComponent<HouseRoom> ().HasTopDoor == 1) {
					return "DoorU";
				}

				if (CurrentRoom.GetComponent<HouseRoom> ().direction == 0 &&
					NearRoom.GetComponent<HouseRoom> ().direction == 3 &&
					NearRoom.GetComponent<HouseRoom> ().HasRightDoor == 1) {
					return "DoorR";
				}


				if (CurrentRoom.GetComponent<HouseRoom> ().direction == 1 &&
					NearRoom.GetComponent<HouseRoom> ().direction == 0 &&
					NearRoom.GetComponent<HouseRoom> ().HasTopDoor == 1) {
					return "DoorU";
				}

				if (CurrentRoom.GetComponent<HouseRoom> ().direction == 1 &&
					NearRoom.GetComponent<HouseRoom> ().direction == 2 &&
					NearRoom.GetComponent<HouseRoom> ().HasRightDoor == 1) {
					return "DoorR";
				}

				if (CurrentRoom.GetComponent<HouseRoom> ().direction == 1 &&
					NearRoom.GetComponent<HouseRoom> ().direction == 3 &&
					NearRoom.GetComponent<HouseRoom> ().HasLeftDoor == 1) {
					return "DoorL";
				}

				if (CurrentRoom.GetComponent<HouseRoom> ().direction == 2 &&
					NearRoom.GetComponent<HouseRoom> ().direction == 0 &&
					NearRoom.GetComponent<HouseRoom> ().HasRightDoor == 1) {
					return "DoorR";
				}
				if (CurrentRoom.GetComponent<HouseRoom> ().direction == 2 &&
					NearRoom.GetComponent<HouseRoom> ().direction == 1 &&
					NearRoom.GetComponent<HouseRoom> ().HasLeftDoor == 1) {
					return "DoorL";
				}
				if (CurrentRoom.GetComponent<HouseRoom> ().direction == 2 &&
					NearRoom.GetComponent<HouseRoom> ().direction == 3 &&
					NearRoom.GetComponent<HouseRoom> ().HasTopDoor == 1) {
					return "DoorU";
				}

				if (CurrentRoom.GetComponent<HouseRoom> ().direction == 3 &&
					NearRoom.GetComponent<HouseRoom> ().direction == 2 &&
					NearRoom.GetComponent<HouseRoom> ().HasTopDoor == 1) {
					return "DoorU";
				}
				if (CurrentRoom.GetComponent<HouseRoom> ().direction == 3 &&
					NearRoom.GetComponent<HouseRoom> ().direction == 0 &&
					NearRoom.GetComponent<HouseRoom> ().HasLeftDoor == 1) {
					return "DoorL";
				}
				if (CurrentRoom.GetComponent<HouseRoom> ().direction == 3 &&
					NearRoom.GetComponent<HouseRoom> ().direction == 1 &&
					NearRoom.GetComponent<HouseRoom> ().HasRightDoor == 1) {
					return "DoorR";
				}

			}
		}


		return null;
	}


	protected void OnTriggerStay2D( Collider2D other){
		if (other.tag == "Player") {
			nearDoor = true;

			NearRoom = HouseroomManager.instance.checkNearRoom (this.tag);
			//TODO
			if (NearRoom != null) {
				ConnectedDoorTag = checkConnected ();
				if (ConnectedDoorTag == null) {
					locked = true;
				} else {
					isConnectedtoRoom = true;
				}
			}

			if (locked) {
				if (!lockSoundShowed) {
					lockSoundShowed = true;
					SoundManager.instance.door_lock_1.Play ();
				}
				GameManager.instance.GetComponent<IndicatorText> ().showIndicator ("The door is locked");
			} else {
				GameManager.instance.GetComponent<IndicatorText> ().showIndicator ("Press F to open the door");
			}
		}
	}

	protected void OnTriggerExit2D( Collider2D other){
		nearDoor = false;
		lockSoundShowed = false;
		GameManager.instance.GetComponent<IndicatorText> ().hideIndicator ();
	}

	public void initPlayerPosition(string newRoomDoor, int Roomtype){

		if (newRoomDoor == null) {
			if(Roomtype == 0){
				if (this.tag == "DoorL") {
					GameManager.instance.myplayer.transform.position = new Vector3 (1.5f, -1.1f);
				} else {
					GameManager.instance.myplayer.transform.position = new Vector3 (-18f, -0.7f);
				}
			}

			if (Roomtype == 1) {
				if (this.tag == "DoorL") {
					GameManager.instance.myplayer.transform.position = new Vector3 (14.5f, -0.5f);
				} else {
					GameManager.instance.myplayer.transform.position = new Vector3 (-18f, -0.7f);
				}
			}

		}else if (newRoomDoor == "DoorL") {
			if(Roomtype == 0){
				GameManager.instance.myplayer.transform.position = new Vector3 (-18f, -0.7f);
			}

			if (Roomtype == 1) {
				GameManager.instance.myplayer.transform.position = new Vector3 (-18f, -0.7f);
			}

		}else if( newRoomDoor == "DoorR"){
			if(Roomtype == 0){
				GameManager.instance.myplayer.transform.position = new Vector3 (1.5f, -1.1f);
			}	
			if (Roomtype == 1) {
				GameManager.instance.myplayer.transform.position = new Vector3 (14.5f, -0.5f);
			}
		}else{
			if(Roomtype == 0){
				GameManager.instance.myplayer.transform.position = new Vector3 (-9f, -0.1f);
			}

			if(Roomtype == 1){
				GameManager.instance.myplayer.transform.position = new Vector3 (-9f, -0.1f);
			}


		}

	}

	//TODO 不使用递归
	public GameObject selectNewRoom(){
		GameObject newRoom = HouseroomManager.instance.RoomRandomList [Random.Range (0, HouseroomManager.instance.RoomRandomList.Length)];

		if (HouseroomManager.instance.currentKeyRoomIndex < HouseroomManager.instance.keyRoomList.Length) {
			if (HouseroomManager.instance.currentRoomIndex < (HouseroomManager.instance.currentKeyRoomIndex + 1) * 5) {
				
				if (HouseroomManager.instance.isOneDoorLeft()) {
					if(newRoom.GetComponent<HouseRoom>().repeateable == false && newRoom.GetComponent<HouseRoom>().alreadyExist == true){
						return selectNewRoom();
					}

					if (newRoom.GetComponent<HouseRoom> ().doorNumber == 1 ||newRoom.GetComponent<HouseRoom> ().doorNumber ==2) {
						return (HouseroomManager.instance.RoomRandomList [0]);
					}
					return newRoom;
				}

				if (Random.Range (0, 9) < 2) {
					HouseroomManager.instance.currentKeyRoomIndex += 1;

					if (HouseroomManager.instance.currentKeyRoomIndex == 1) {
						HouseroomManager.instance.GhostBegin = true;
						Instantiate (HouseroomManager.instance.GhostObj, GameObject.Find ("Ghosts").transform);

						showTutorial3 = true;

					}

					return (HouseroomManager.instance.keyRoomList [HouseroomManager.instance.currentKeyRoomIndex - 1]);
				}
			
			} else if (HouseroomManager.instance.currentRoomIndex >= (HouseroomManager.instance.currentKeyRoomIndex + 1) * 5) {
				
				if (HouseroomManager.instance.isOneDoorLeft()) {
					if(newRoom.GetComponent<HouseRoom>().repeateable == false && newRoom.GetComponent<HouseRoom>().alreadyExist == true){
						return selectNewRoom();
					}

					if (newRoom.GetComponent<HouseRoom> ().doorNumber == 1 ||newRoom.GetComponent<HouseRoom> ().doorNumber ==2) {
						return (HouseroomManager.instance.RoomRandomList [0]);
					}

					return newRoom;
				}

				HouseroomManager.instance.currentKeyRoomIndex += 1;

				if (HouseroomManager.instance.currentKeyRoomIndex == 1) {
					HouseroomManager.instance.GhostBegin = true;
					Instantiate (HouseroomManager.instance.GhostObj, GameObject.Find ("Ghosts").transform);

					showTutorial3 = true;


				}

				return (HouseroomManager.instance.keyRoomList [HouseroomManager.instance.currentKeyRoomIndex - 1]);
			}
		}



		if(newRoom.GetComponent<HouseRoom>().repeateable == false && newRoom.GetComponent<HouseRoom>().alreadyExist == true){
			return selectNewRoom();
		}

		if (newRoom.GetComponent<HouseRoom> ().doorNumber == 1 && HouseroomManager.instance.isOneDoorLeft()) {
			return selectNewRoom();
		}

		return newRoom;
	}

	void changeingRoom(){
		GameManager.instance.GetComponent<IndicatorText> ().blackCurtain.enabled = false;
		GameManager.instance.myplayer.busy = false;
		changingRoom = false;

		for (int i2 = 0; i2 < HouseroomManager.instance.Ghosts.Count; i2++) {
			if (HouseroomManager.instance.Ghosts [i2] != null) {
				if (HouseroomManager.instance.Ghosts [i2].GhostPosition [0] == GameObject.FindGameObjectWithTag ("CurrentRoom").GetComponent<HouseRoom> ().RoomPosition [0] &&
					HouseroomManager.instance.Ghosts [i2].GhostPosition [1] == GameObject.FindGameObjectWithTag ("CurrentRoom").GetComponent<HouseRoom> ().RoomPosition [1]) {
					HouseroomManager.instance.attackPlayer = true;
					DestroyImmediate (HouseroomManager.instance.Ghosts [i2].gameObject);

				} 
			}
		}

		GameManager.instance.GetComponent<IndicatorText> ().updateGhostsInMap ();

		if (showTutorial3) {
			
			GameManager.instance.GetComponent<IndicatorText> ().showKeyInformation ("tutorial3", "tutorial3_end");
			showTutorial3 = false;
			GameManager.instance.GetComponent<IndicatorText> ().useRtoRest.enabled = true;

		}

		int doorNumber = 0;
		for (int i = 0; i < HouseroomManager.instance.Doors.Count; i++) {
			if(HouseroomManager.instance.Doors[i].isNearRoom == false){
				doorNumber +=1;
			}
		}
		if (doorNumber == 0) {
			GameManager.instance.GetComponent<IndicatorText> ().showKeyInformation ("nodoor", "nodoor_end");
		}

	}
	
	// Update is called once per frame
	void Update () {

		//if (HouseroomManager.instance.checkNearRoomWithRoom (this.tag,belongRoom) != null && !isNearRoom) {
		//	isNearRoom = true;
		//}


		if (!changingRoom && !GameManager.instance.myplayer.busy) {

			int NewRoomrelatedPosition = new int ();

			//RoomDirection : 0:水平向右 1:水平向左 2:垂直向上 3 垂直向下
			if ((CurrentRoom.GetComponent<HouseRoom> ().direction == 0 && this.tag == "DoorR") ||
				(CurrentRoom.GetComponent<HouseRoom> ().direction == 1 && this.tag == "DoorL") ||
				(CurrentRoom.GetComponent<HouseRoom> ().direction == 3 && this.tag == "DoorU")) {
				NewRoomrelatedPosition = 2;
			} else if ((CurrentRoom.GetComponent<HouseRoom> ().direction == 0 && this.tag == "DoorL") ||
				(CurrentRoom.GetComponent<HouseRoom> ().direction == 1 && this.tag == "DoorR") ||
				(CurrentRoom.GetComponent<HouseRoom> ().direction == 2 && this.tag == "DoorU")) {
				NewRoomrelatedPosition = 1;
			} else if ((CurrentRoom.GetComponent<HouseRoom> ().direction == 0 && this.tag == "DoorU") ||
				(CurrentRoom.GetComponent<HouseRoom> ().direction == 2 && this.tag == "DoorR") ||
				(CurrentRoom.GetComponent<HouseRoom> ().direction == 3 && this.tag == "DoorL")) {
				NewRoomrelatedPosition = 4;
			} else if ((CurrentRoom.GetComponent<HouseRoom> ().direction == 1 && this.tag == "DoorU") ||
				(CurrentRoom.GetComponent<HouseRoom> ().direction == 2 && this.tag == "DoorL") ||
				(CurrentRoom.GetComponent<HouseRoom> ().direction == 3 && this.tag == "DoorR")) {
				NewRoomrelatedPosition = 3;
			}
			
			if (Input.GetKeyDown ("f") && nearDoor && NearRoom == null && !locked) {

				isNearRoom = true;

				changingRoom = true;
				CurrentRoom = GameObject.FindGameObjectWithTag ("CurrentRoom");
				nearDoor = false;

				GameManager.instance.myplayer.playerRigidbody.velocity = new Vector2 (0f, 0f);
				GameManager.instance.myplayer.busy = true;

				GameManager.instance.GetComponent<IndicatorText> ().hideIndicator ();


				GameObject newRoom = selectNewRoom();


				if (this.tag == "DoorR" || this.tag == "DoorL") {
					newRoom.GetComponent<HouseRoom> ().direction = CurrentRoom.GetComponent<HouseRoom> ().direction;
				} else {
					if (CurrentRoom.GetComponent<HouseRoom> ().direction == 0) {
						newRoom.GetComponent<HouseRoom> ().direction = 2;
					} else if (CurrentRoom.GetComponent<HouseRoom> ().direction == 1) {
						newRoom.GetComponent<HouseRoom> ().direction = 3;
					} else if (CurrentRoom.GetComponent<HouseRoom> ().direction == 2) {
						newRoom.GetComponent<HouseRoom> ().direction = 1;
					} else if (CurrentRoom.GetComponent<HouseRoom> ().direction == 3) {
						newRoom.GetComponent<HouseRoom> ().direction = 0;
					}
				}

				///test
				initPlayerPosition(null,newRoom.GetComponent<HouseRoom> ().Roomtype);

				HouseroomManager.instance.allGhostsMove ();
				HouseroomManager.instance.AddNewRoom (newRoom.GetComponent<HouseRoom> (), NewRoomrelatedPosition);

				SoundManager.instance.door_creak_closing.Play ();
				GameManager.instance.GetComponent<IndicatorText> ().blackCurtain.enabled = true;

				Invoke ("changeingRoom", 2f);


			} else if (!changingRoom && Input.GetKeyDown ("f") && nearDoor && isConnectedtoRoom &&!locked) {
				
				changingRoom = true;
				CurrentRoom = GameObject.FindGameObjectWithTag ("CurrentRoom");
				nearDoor = false;
				isNearRoom = true;

				GameManager.instance.myplayer.playerRigidbody.velocity = new Vector2 (0f, 0f);
				GameManager.instance.myplayer.busy = true;

				///test
				initPlayerPosition (ConnectedDoorTag,NearRoom.GetComponent<HouseRoom>().Roomtype);

				CurrentRoom.tag = "HouseRoom";
				NearRoom.tag = "CurrentRoom";

				CurrentRoom.SetActive (false);
				NearRoom.SetActive (true);

				GameManager.instance.GetComponent<IndicatorText> ().connectedToRoomInMap (NearRoom.GetComponent<HouseRoom> ().roomIndex, CurrentRoom.GetComponent<HouseRoom> ().roomIndex, NewRoomrelatedPosition,NearRoom.GetComponent<HouseRoom>());

				SoundManager.instance.door_creak_closing.Play ();
				GameManager.instance.GetComponent<IndicatorText> ().blackCurtain.enabled = true;

				HouseroomManager.instance.allGhostsMove ();
				Invoke ("changeingRoom", 2f);
			}
		}
	  
	
	}
}

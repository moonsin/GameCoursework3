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

	// Use this for initialization
	void Start () {
		CurrentRoom = GameObject.FindGameObjectWithTag ("CurrentRoom");
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
				if (NearRoom.GetComponent<HouseRoom> ().HasTopDoor == 1) {
					return "DoorU";
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
					GameManager.instance.myplayer.transform.position = new Vector3 (12f, -2.3f);
				} else {
					GameManager.instance.myplayer.transform.position = new Vector3 (-16f, -3f);
				}
			}

		}else if (newRoomDoor == "DoorL") {
			if(Roomtype == 0){
				GameManager.instance.myplayer.transform.position = new Vector3 (-17f, -3f);
			}
		}else if( newRoomDoor == "DoorR"){
			if(Roomtype == 0){
				GameManager.instance.myplayer.transform.position = new Vector3 (12f, -2.3f);
			}	
		}else{
			if(Roomtype == 0){
				GameManager.instance.myplayer.transform.position = new Vector3 (-8f, -1f);
			}	
		}

	}


	
	// Update is called once per frame
	void Update () {
		if (!changingRoom) {

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

				changingRoom = true;
				CurrentRoom = GameObject.FindGameObjectWithTag ("CurrentRoom");
				nearDoor = false;

				GameManager.instance.myplayer.playerRigidbody.velocity = new Vector2 (0f, 0f);
				GameManager.instance.myplayer.busy = true;

				GameManager.instance.GetComponent<IndicatorText> ().hideIndicator ();
				//TODO

				GameObject newRoom = HouseroomManager.instance.RoomRandomList [Random.Range (0, HouseroomManager.instance.RoomRandomList.Length)];


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
				
				HouseroomManager.instance.AddNewRoom (newRoom.GetComponent<HouseRoom> (), NewRoomrelatedPosition);

				GameManager.instance.myplayer.busy = false;


				changingRoom = false;
			} else if (Input.GetKeyDown ("f") && nearDoor && isConnectedtoRoom &&!locked) {
				
				changingRoom = true;
				CurrentRoom = GameObject.FindGameObjectWithTag ("CurrentRoom");
				nearDoor = false;


				///test
				initPlayerPosition (ConnectedDoorTag,NearRoom.GetComponent<HouseRoom>().Roomtype);

				CurrentRoom.tag = "HouseRoom";
				NearRoom.tag = "CurrentRoom";

				CurrentRoom.SetActive (false);
				NearRoom.SetActive (true);

				GameManager.instance.GetComponent<IndicatorText> ().connectedToRoomInMap (NearRoom.GetComponent<HouseRoom> ().roomIndex, CurrentRoom.GetComponent<HouseRoom> ().roomIndex, NewRoomrelatedPosition,NearRoom.GetComponent<HouseRoom>());

				changingRoom = false;
			}
		}
	  
	
	}
}

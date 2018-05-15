using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseroomManager : MonoBehaviour {

	public static HouseroomManager instance = null;

	public GameObject[,] RoomInstancesMap;
	public int MaxRoomNumber = 100;
	public int currentRoomIndex = 0;
	public int currentKeyRoomIndex = 0;
	public GameObject CurrentRoom;
	public int[,] ConnectedMatrix;
	public GameObject FirstRoom;
	public GameObject[] RoomRandomList;
	public GameObject[] keyRoomList;
	public List<HouseRoomDoor> Doors;
	public bool GhostBegin = false;
	public List<HouseRoom> HouseRooms;
	public List<Ghost> Ghosts;
	public GameObject GhostObj;
	public bool playerAttaced = false;
	public bool attackPlayer = false;
	public bool notInFirstKeyroom = false;

	public bool addGhost = false;
	public bool addRoom = false;

	void Awake(){

		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		for (int i = 0; i < RoomRandomList.Length; i++) {
			RoomRandomList [i].GetComponent<HouseRoom> ().alreadyExist = false;
		}
	}

	public void addDoorTolist(HouseRoomDoor door){
		Doors.Add (door);
	}

	public void addRoomTolist(HouseRoom room){
		HouseRooms.Add (room);
	}

	public void HouseRoomStart(){
		initRoomMapMatrix (MaxRoomNumber);
		initFirstRoom ();
	}

	public bool ExistGhost(int[] position){
		for(int i = 0 ; i< Ghosts.Count; i++){
			if (Ghosts [i].GhostPosition [0] == position [0] &&
			   Ghosts [i].GhostPosition [1] == position [1]) {
				return true;
			}
		}
		return false;
	}

	public void addNewGhostToList(Ghost ghost){
		
		int index = Random.Range (0, HouseRooms.Count);
		if (HouseRooms [index].tag == "CurrentRoom") {
			
			addNewGhostToList (ghost);
			return; 
		} else if(ExistGhost(HouseRooms [index].RoomPosition)) {
			
			addNewGhostToList (ghost);
			return; 
		}else{
			ghost.GhostPosition[0] = HouseRooms [index].RoomPosition[0];
			ghost.GhostPosition[1] = HouseRooms [index].RoomPosition[1];
			Ghosts.Add(ghost);

			SoundManager.instance.cry.Play ();
			addGhost = true;

			GameManager.instance.GetComponent<IndicatorText> ().updateGhostsInMap ();
		}

	}

	public int[] isGhostMoveAble(Ghost ghost){
		int[] newGhostPosition = new int[2]; 
		bool moveable = true;
		int[] moveableDirectionList = new int[4]{100,100,100,100};

		for (int i1 = 0; i1 < 4; i1++) {
			moveable = true;
			if (i1 == 0) {
				newGhostPosition [0] = ghost.GhostPosition [0] + 1;
				newGhostPosition[1]=ghost.GhostPosition[1];
			}else if(i1 == 1) {

				newGhostPosition [0] = ghost.GhostPosition [0] - 1;
				newGhostPosition[1]=ghost.GhostPosition[1];
			}else if(i1 == 2) {

				newGhostPosition [0] = ghost.GhostPosition [0];
				newGhostPosition[1]=ghost.GhostPosition[1]+1;
			}else if(i1 == 3) {
				newGhostPosition [0] = ghost.GhostPosition [0] ;
				newGhostPosition[1]=ghost.GhostPosition[1]- 1;
			}

			for (int i = 0; i < Ghosts.Count; i++) {
				if (Ghosts [i] != null) {
					if (Ghosts [i].GhostPosition [0] == newGhostPosition [0] &&
					   Ghosts [i].GhostPosition [1] == newGhostPosition [1]) {
						moveable = false;
					}
				}
			}
			if (moveable != false) {
				for (int i2 = 0; i2 < HouseRooms.Count; i2++) {
					if (HouseRooms [i2].RoomPosition [0] == newGhostPosition [0] &&
					   HouseRooms [i2].RoomPosition [1] == newGhostPosition [1]) {
						moveableDirectionList [i1] = i1;
					}
				}
			}

		}

		return moveableDirectionList;

	}

	public void GhostMove(Ghost ghost){
		
		int[] newGhostPosition = new int[2]; 
		bool GhostMoveable = false;
		int moveAbleLength = 0;
		newGhostPosition[0] = ghost.GhostPosition[0];
		newGhostPosition[1] = ghost.GhostPosition[1];

		int[] moveAbleList = isGhostMoveAble (ghost);

		for (int i = 0; i < 4; i++) {
			if (moveAbleList [i] != 100) {
				GhostMoveable = true;
				moveAbleLength += 1;
			}
		}
		print (GhostMoveable);
		//TODO 从可以移动的地方选择
		if (GhostMoveable) {


			
			int moveDirectionIndex = Random.Range (0, moveAbleLength);
			int moveDirection = 0;

			for (int i = 0; i < 4; i++) {
				if (moveAbleList [i] != 100) {
					moveDirectionIndex -= 1;
				}
				if (moveDirectionIndex == -1) {
					moveDirection = moveAbleList [i];
					moveDirectionIndex -= 1;
				}
			}


			if (moveDirection == 0) {
				newGhostPosition [0] = ghost.GhostPosition [0] + 1;
				newGhostPosition [1] = ghost.GhostPosition [1];
			} else if (moveDirection == 1) {
			
				newGhostPosition [0] = ghost.GhostPosition [0] - 1;
				newGhostPosition [1] = ghost.GhostPosition [1];
			} else if (moveDirection == 2) {
			 
				newGhostPosition [0] = ghost.GhostPosition [0];
				newGhostPosition [1] = ghost.GhostPosition [1] + 1;
			} else if (moveDirection == 3) {
				newGhostPosition [0] = ghost.GhostPosition [0];
				newGhostPosition [1] = ghost.GhostPosition [1] - 1;
			}

		  }
			ghost.GhostPosition [0] = newGhostPosition [0];
			ghost.GhostPosition [1] = newGhostPosition [1];
		}

	public void allGhostsMove(){
		
		for (int i = 0; i < Ghosts.Count; i++) {
			if (Ghosts [i] != null) {
				GhostMove (Ghosts [i]);
			}
		}
			
		GameManager.instance.GetComponent<IndicatorText> ().updateGhostsInMap ();

	}

	void initFirstRoom(){
		GameObject FirstRoomInstance =  Instantiate (FirstRoom) as GameObject;
		RoomInstancesMap [MaxRoomNumber-1, MaxRoomNumber-1] = FirstRoomInstance;
		FirstRoomInstance.GetComponent<HouseRoom> ().RoomPosition [0] = MaxRoomNumber-1;
		FirstRoomInstance.GetComponent<HouseRoom> ().RoomPosition [1] = MaxRoomNumber-1;
	}

	void initRoomMapMatrix(int RoomNumber){

		RoomInstancesMap = new GameObject[RoomNumber*2, RoomNumber*2];

		for (int x = 0; x < RoomNumber; x++) {
			for (int y = 0; y < RoomNumber; y++) {
				RoomInstancesMap [x, y] = null;
			}
		}
	}

	//newRoomRalativePosition: 1:在旧左面 2:在旧房间右面 3:在旧房间下面 4:在旧房间上面
	public void AddNewRoom(HouseRoom newroom, int newRoomRalativePosition){
		CurrentRoom = GameObject.FindGameObjectWithTag ("CurrentRoom");

		currentRoomIndex += 1;
		newroom.roomIndex = currentRoomIndex;
		CurrentRoom.tag = "HouseRoom";

		CurrentRoom.SetActive (false);


		if (newRoomRalativePosition == 1) {

			newroom.RoomPosition [0] = CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0] - 1;
			newroom.RoomPosition [1] = CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [1];
			newroom.tag = "CurrentRoom";
			GameObject newRoomInstance = Instantiate (newroom.gameObject) as GameObject;

			RoomInstancesMap [CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0] - 1, CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [1]] = newRoomInstance;

		}else if (newRoomRalativePosition == 2) {
			newroom.RoomPosition [0] = CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0] + 1;
			newroom.RoomPosition [1] = CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [1];
			newroom.tag = "CurrentRoom";
			GameObject newRoomInstance = Instantiate (newroom.gameObject) as GameObject;

			RoomInstancesMap [CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0] + 1, CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [1]] = newRoomInstance;

		}else if (newRoomRalativePosition == 3) {
			newroom.RoomPosition [0] = CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0];
			newroom.RoomPosition [1] = CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [1] -1;
			newroom.tag = "CurrentRoom";
			GameObject newRoomInstance = Instantiate (newroom.gameObject) as GameObject;

			RoomInstancesMap [CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0], CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [1]-1] = newRoomInstance;

		}else if (newRoomRalativePosition == 4) {

			newroom.RoomPosition [0] = CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0];
			newroom.RoomPosition [1] = CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [1] +1;
			newroom.tag = "CurrentRoom";
			GameObject newRoomInstance = Instantiate (newroom.gameObject) as GameObject;

			RoomInstancesMap [CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0], CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [1]+1] = newRoomInstance;

		}
		GameManager.instance.GetComponent<IndicatorText> ().addNewRoomInMap (newroom.roomIndex, CurrentRoom.GetComponent<HouseRoom> ().roomIndex,newroom, newRoomRalativePosition);

		newroom.alreadyExist = true;

		addRoom = true;

		if (GhostBegin == true ) {
			if (notInFirstKeyroom == true) {
				
				int randomNumber = Random.Range (0, 4);


				if (randomNumber >= 2) {
					if (
						((float)(GameManager.instance.GetComponent<IndicatorText> ().GhostNumber)
						/ (float)(GameManager.instance.GetComponent<IndicatorText> ().RoomNumber))
						< 0.66f) {
						Instantiate (GhostObj, GameObject.Find ("Ghosts").transform);
					}
				} else {
					if (
						((float)(GameManager.instance.GetComponent<IndicatorText> ().GhostNumber)
						/ (float)(GameManager.instance.GetComponent<IndicatorText> ().RoomNumber))
						< 0.2f) {
						Instantiate (GhostObj, GameObject.Find ("Ghosts").transform);
					}
				}

			}
				
			notInFirstKeyroom = true;
		}
	
	}

	public bool isOneDoorLeft(){
		int doorNumber = 0;
		for (int i = 0; i < Doors.Count; i++) {
			if (doorNumber > 1) {
				return false;
			}

			if(Doors[i].isNearRoom == false){
				doorNumber +=1;
			}
		}

		return true;
	}


	//RoomDirection : 0:水平向右 1:水平向左 2:垂直向上 3 垂直向下
	public GameObject checkNearRoom(string doorType){
		
		CurrentRoom = GameObject.FindGameObjectWithTag ("CurrentRoom");

		if ((CurrentRoom.GetComponent<HouseRoom> ().direction == 0 && doorType == "DoorR") || 
			(CurrentRoom.GetComponent<HouseRoom> ().direction == 1 && doorType == "DoorL")||
			(CurrentRoom.GetComponent<HouseRoom> ().direction == 3 && doorType == "DoorU")
		) {
			return (RoomInstancesMap [CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0] + 1, CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [1]]);
		} else if
			((CurrentRoom.GetComponent<HouseRoom> ().direction == 0 && doorType == "DoorL") || 
			(CurrentRoom.GetComponent<HouseRoom> ().direction == 1 && doorType == "DoorR") ||
			(CurrentRoom.GetComponent<HouseRoom> ().direction == 2 && doorType == "DoorU")
		) {
			return (RoomInstancesMap [CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0] - 1, CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [1]]);
		} else if ((CurrentRoom.GetComponent<HouseRoom> ().direction == 0 && doorType == "DoorU")||
			(CurrentRoom.GetComponent<HouseRoom> ().direction == 2 && doorType == "DoorR") ||
			(CurrentRoom.GetComponent<HouseRoom> ().direction == 3 && doorType == "DoorL")
		) {
			return (RoomInstancesMap [CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0], CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [1]+1]);
		}else if ((CurrentRoom.GetComponent<HouseRoom> ().direction == 1 && doorType == "DoorU")||
			(CurrentRoom.GetComponent<HouseRoom> ().direction == 2 && doorType == "DoorL") ||
			(CurrentRoom.GetComponent<HouseRoom> ().direction == 3 && doorType == "DoorR")
		) {
			return (RoomInstancesMap [CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0], CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [1]-1]);
		}

		return null;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		
	}
}

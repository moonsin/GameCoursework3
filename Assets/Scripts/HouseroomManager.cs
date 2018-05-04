using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseroomManager : MonoBehaviour {

	public static HouseroomManager instance = null;

	public GameObject[,] RoomInstancesMap;
	public int MaxRoomNumber = 20;
	public int currentRoomIndex = 0;
	public GameObject CurrentRoom;
	public int[,] ConnectedMatrix;
	public GameObject FirstRoom;


	void Awake(){

		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}


	}

	public void HouseRoomStart(){
		initRoomMapMatrix (MaxRoomNumber);
		initFirstRoom ();
	}


	void initFirstRoom(){
		GameObject FirstRoomInstance =  Instantiate (FirstRoom) as GameObject;
		RoomInstancesMap [19, 19] = FirstRoomInstance;

	}

	void initRoomMapMatrix(int RoomNumber){

		RoomInstancesMap = new GameObject[RoomNumber, RoomNumber];

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

		if (newRoomRalativePosition == 1) {
			RoomInstancesMap [CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0] - 1, CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0]] = newroom.gameObject;
		}else if (newRoomRalativePosition == 2) {
			RoomInstancesMap [CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0] + 1, CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0]] = newroom.gameObject;
		}else if (newRoomRalativePosition == 3) {
			RoomInstancesMap [CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0], CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0]-1] = newroom.gameObject;
		}else if (newRoomRalativePosition == 4) {
			RoomInstancesMap [CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0], CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0]+1] = newroom.gameObject;
		}

		CurrentRoom.tag = "HouseRoom";
		newroom.tag = "CurrentRoom";

	}

	//RoomDirection : 0:水平向右 1:水平向左 2:垂直向上 3 垂直向下
	public GameObject checkConnectedRoom(string doorType){
		
		CurrentRoom = GameObject.FindGameObjectWithTag ("CurrentRoom");

		if ((CurrentRoom.GetComponent<HouseRoom> ().direction == 0 && doorType == "DoorR") || 
			(CurrentRoom.GetComponent<HouseRoom> ().direction == 1 && doorType == "DoorL")||
			(CurrentRoom.GetComponent<HouseRoom> ().direction == 3 && doorType == "DoorU")
		) {
			return (RoomInstancesMap [CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0] + 1, CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0]]);
		} else if
			((CurrentRoom.GetComponent<HouseRoom> ().direction == 0 && doorType == "DoorL") || 
			(CurrentRoom.GetComponent<HouseRoom> ().direction == 1 && doorType == "DoorR") ||
			(CurrentRoom.GetComponent<HouseRoom> ().direction == 2 && doorType == "DoorU")
		) {
			return (RoomInstancesMap [CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0] - 1, CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0]]);
		} else if ((CurrentRoom.GetComponent<HouseRoom> ().direction == 0 && doorType == "DoorU")||
			(CurrentRoom.GetComponent<HouseRoom> ().direction == 2 && doorType == "DoorR") ||
			(CurrentRoom.GetComponent<HouseRoom> ().direction == 3 && doorType == "DoorL")
		) {
			return (RoomInstancesMap [CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0], CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0]+1]);
		}else if ((CurrentRoom.GetComponent<HouseRoom> ().direction == 1 && doorType == "DoorU")||
			(CurrentRoom.GetComponent<HouseRoom> ().direction == 2 && doorType == "DoorL") ||
			(CurrentRoom.GetComponent<HouseRoom> ().direction == 3 && doorType == "DoorR")
		) {
			return (RoomInstancesMap [CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0], CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0]-1]);
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

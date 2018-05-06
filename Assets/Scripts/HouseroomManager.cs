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
	public GameObject[] RoomRandomList;


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
		FirstRoomInstance.GetComponent<HouseRoom> ().RoomPosition [0] = 19;
		FirstRoomInstance.GetComponent<HouseRoom> ().RoomPosition [1] = 19;
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


		print (newRoomRalativePosition);

		if (newRoomRalativePosition == 1) {

			newroom.RoomPosition [0] = CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0] - 1;
			newroom.RoomPosition [1] = CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [1];
			GameObject newRoomInstance = Instantiate (newroom.gameObject) as GameObject;

			RoomInstancesMap [CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0] - 1, CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [1]] = newRoomInstance;

		}else if (newRoomRalativePosition == 2) {
			newroom.RoomPosition [0] = CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0] + 1;
			newroom.RoomPosition [1] = CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [1];
			GameObject newRoomInstance = Instantiate (newroom.gameObject) as GameObject;

			RoomInstancesMap [CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0] + 1, CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [1]] = newRoomInstance;

		}else if (newRoomRalativePosition == 3) {
			newroom.RoomPosition [0] = CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0];
			newroom.RoomPosition [1] = CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [1] -1;
			GameObject newRoomInstance = Instantiate (newroom.gameObject) as GameObject;

			RoomInstancesMap [CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0], CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [1]-1] = newRoomInstance;

		}else if (newRoomRalativePosition == 4) {

			newroom.RoomPosition [0] = CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0];
			newroom.RoomPosition [1] = CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [1] +1;
			GameObject newRoomInstance = Instantiate (newroom.gameObject) as GameObject;

			RoomInstancesMap [CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [0], CurrentRoom.GetComponent<HouseRoom> ().RoomPosition [1]+1] = newRoomInstance;

		}



		newroom.tag = "CurrentRoom";


		GameManager.instance.GetComponent<IndicatorText> ().addNewRoomInMap (newroom, newRoomRalativePosition);

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

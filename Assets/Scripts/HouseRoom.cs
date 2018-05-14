using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseRoom : MonoBehaviour {

	public int roomIndex;
	public int direction;
	public int doorNumber;
	public int HasLeftDoor;
	public int HasRightDoor;
	public int HasTopDoor;
	public int[] RoomPosition = new int[2];
	public int Roomtype; // 0小 1大
	public bool repeateable = true;
	public bool alreadyExist = false;

	// Use this for initialization
	void Start () {
		alreadyExist = false;
		HouseroomManager.instance.addRoomTolist (this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

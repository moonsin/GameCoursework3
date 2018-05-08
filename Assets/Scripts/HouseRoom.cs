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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {
	
	public int[] GhostPosition = new int[2];
	// Use this for initialization
	void Start () {
		HouseroomManager.instance.addNewGhostToList (this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

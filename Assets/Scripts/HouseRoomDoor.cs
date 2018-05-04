using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseRoomDoor : MonoBehaviour {
	
	protected bool nearDoor = false;
	public GameObject ConnectedRoom;
	public bool locked = false;

	// Use this for initialization
	void Start () {
		
	}

	protected void OnTriggerStay2D( Collider2D other){
		if (other.tag == "Player") {
			nearDoor = true;

			ConnectedRoom = HouseroomManager.instance.checkConnectedRoom (this.tag);

			if (ConnectedRoom != null) {
				locked = true;
			}

			if (locked) {
				GameManager.instance.GetComponent<IndicatorText> ().showIndicator ("The door is locked");
			} else {
				GameManager.instance.GetComponent<IndicatorText> ().showIndicator ("Press F to open the door");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown ("f") && nearDoor ) {


			nearDoor = false;
			GameManager.instance.myplayer.playerRigidbody.velocity = new Vector2 (0f, 0f);
			GameManager.instance.myplayer.busy = true;

			GameManager.instance.GetComponent<IndicatorText> ().hideIndicator ();
			//TODO

		
		
		}
	}
}

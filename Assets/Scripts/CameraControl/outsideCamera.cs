using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outsideCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (GameManager.instance.inOutside == true) {
		
			if (GameManager.instance.myplayer.transform.position.x < -9) {
				this.transform.position = new Vector3 (-9f, 0f, -10f);
			} else if (GameManager.instance.myplayer.transform.position.x > 9) {
				this.transform.position = new Vector3 (9f, 0f, -10f);
			} else {
				this.transform.position = new Vector3 (GameManager.instance.myplayer.transform.position.x, 0f, -10f);
			}
		} else if (GameManager.instance.inLoopRoom == true) {
			if (GameManager.instance.myplayer.transform.position.x < -13) {
				this.transform.position = new Vector3 (-13f, 0f, -10f);
			}else if (GameManager.instance.myplayer.transform.position.x > -3) {
				this.transform.position = new Vector3 (-3f, 0f, -10f);
			}else {
				this.transform.position = new Vector3 (GameManager.instance.myplayer.transform.position.x, 0f, -10f);
			}
		}else if (GameManager.instance.inHouseRoom == true) {
			if (GameObject.FindGameObjectWithTag ("CurrentRoom") == null) {
				return ;
			}
			if (GameObject.FindGameObjectWithTag ("CurrentRoom").GetComponent<HouseRoom> ().Roomtype == 0) {
				if (GameManager.instance.myplayer.transform.position.x < -13) {
					this.transform.position = new Vector3 (-13f, 0f, -10f);
				} else if (GameManager.instance.myplayer.transform.position.x > -3) {
					this.transform.position = new Vector3 (-3f, 0f, -10f);
				} else {
					this.transform.position = new Vector3 (GameManager.instance.myplayer.transform.position.x, 0f, -10f);
				}
			}

			if (GameObject.FindGameObjectWithTag ("CurrentRoom").GetComponent<HouseRoom> ().Roomtype == 1) {
				if (GameManager.instance.myplayer.transform.position.x < -13) {
					this.transform.position = new Vector3 (-13f, 0f, -10f);
				} else if (GameManager.instance.myplayer.transform.position.x > 11) {
					this.transform.position = new Vector3 (11f, 0f, -10f);
				} else {
					this.transform.position = new Vector3 (GameManager.instance.myplayer.transform.position.x, 0f, -10f);
				}
			}
		
		}

	}
}

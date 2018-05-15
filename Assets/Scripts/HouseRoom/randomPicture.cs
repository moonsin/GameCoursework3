using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomPicture : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int RandomNumber = Random.Range (0, 11);
		string source = "PF" + RandomNumber.ToString();
		this.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> (source);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

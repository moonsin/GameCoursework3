using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Rigidbody2D playerRigidbody;

	public float currentSppedX;
	public float currentSppedY;

	public float MaxSpeedX;
	public float MaxSpeedY;

	public float horizontalDirection;//-1 to 1
	public float VerticalDirection;//-1 to 1

	const string HORIZONTAL = "Horizontal";
	const string VERTICAL = "Vertical";

	public float xForce;
	public float yForce;

	public bool busy = false;

	void Awake(){
		playerRigidbody = GetComponent<Rigidbody2D> ();
		GameManager.instance.myplayer = this;
	}
	// Use this for initialization
	void Start () {
		
	}

	void MovementX(){
		horizontalDirection = Input.GetAxis (HORIZONTAL);
		playerRigidbody.AddForce(new Vector2(xForce * horizontalDirection,0));
		if (horizontalDirection == 0) {
			playerRigidbody.velocity = new Vector2 (0f, playerRigidbody.velocity.y);
		}

		if (playerRigidbody.velocity.x > MaxSpeedX) {
			playerRigidbody.velocity = new Vector2 (MaxSpeedX, playerRigidbody.velocity.y);
		}

		if (playerRigidbody.velocity.x < 0-MaxSpeedX) {
			playerRigidbody.velocity = new Vector2 (0-MaxSpeedX, playerRigidbody.velocity.y);
		}
	}

	void MovementY(){
		VerticalDirection = Input.GetAxis (VERTICAL);
		playerRigidbody.AddForce(new Vector2(0,yForce * VerticalDirection));

		if (VerticalDirection == 0) {
			playerRigidbody.velocity = new Vector2 (playerRigidbody.velocity.x,0f);
		}

		if (playerRigidbody.velocity.y > MaxSpeedY) {
			playerRigidbody.velocity = new Vector2 (playerRigidbody.velocity.x,MaxSpeedY);
		}

		if (playerRigidbody.velocity.y < 0-MaxSpeedY) {
			playerRigidbody.velocity = new Vector2 (playerRigidbody.velocity.x, 0-MaxSpeedY);
		}
			
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!busy) {
			MovementX ();
			MovementY ();
		}

		if (playerRigidbody.velocity.x > 1) {
			this.transform.rotation = Quaternion.Euler (0, 0, 0);
		}
		if (playerRigidbody.velocity.x < -1) {
			this.transform.rotation = Quaternion.Euler (0, 180, 0);
		}

		currentSppedX = playerRigidbody.velocity.x;
	}
}

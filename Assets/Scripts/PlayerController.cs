using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float walkSpeed = 1; // player left right walk speed
	private bool isOnGround = true; // is player on the ground?


	Animator animator;
	Rigidbody2D rigidbody2D;

	//some flags to check when certain animations are playing
	bool isCrouchPlaying = false;
	bool isWalkPlaying = false;
	bool isHadookenPlaying = false;

	//animation states - the values in the animator conditions
	const int STATE_IDLE = 0;
	const int STATE_WALK = 1;
	const int STATE_CROUCH = 2;
	const int STATE_JUMP = 3;
	const int STATE_HADOOKEN = 4;

	string currentDirection = "left";
	int currentAnimationState = STATE_IDLE;

	// Use this for initialization
	void Start () {

		//define the animator attached to the player
		animator = this.GetComponent<Animator>();
		rigidbody2D = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// FixedUpdate is used insead of Update to better handle the physics based jump
	void FixedUpdate () {
		
		//Check for keyboard input
		if (Input.GetKeyDown (KeyCode.Space))
		{
			ChangeState (STATE_HADOOKEN);
		}
		else if (Input.GetKey ("up") && !isHadookenPlaying && !isCrouchPlaying)
		{
			if(isOnGround)
			{
				isOnGround = false;
				//simple jump code using unity physics
				rigidbody2D.AddForce(new Vector2(0, 300));
				ChangeState(STATE_JUMP);
			}
		}
		else if (Input.GetKey ("down"))
		{
			ChangeState(STATE_CROUCH);
		}
		else if (Input.GetKey ("right") && !isHadookenPlaying )
		{
			ChangeDirection ("right");
			transform.Translate(Vector3.left * walkSpeed * Time.deltaTime);

			if(isOnGround)
				ChangeState(STATE_WALK);

		}
		else if (Input.GetKey ("left") && !isHadookenPlaying)
		{
			ChangeDirection ("left");
			transform.Translate(Vector3.left * walkSpeed * Time.deltaTime);

			if(isOnGround)
				ChangeState(STATE_WALK);

		}
		else
		{
			if(isOnGround)
				ChangeState(STATE_IDLE);
		}

		//check if crouch animation is playing
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("ken_crouch"))
			isCrouchPlaying = true;
		else
			isCrouchPlaying = false;

		//check if hadooken animation is playing
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("ken_hadooken"))
			isHadookenPlaying = true;
		else
			isHadookenPlaying = false;

		//check if strafe animation is playing
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("ken_walk"))
			isWalkPlaying = true;
		else
			isWalkPlaying = false;
	}


	//--------------------------------------
	// Change the players animation state
	//--------------------------------------
	void ChangeState(int state){

		if (currentAnimationState == state)
			return;

		switch (state) {

		case STATE_WALK:
			animator.SetInteger ("state", STATE_WALK);
			break;

		case STATE_CROUCH:
			animator.SetInteger ("state", STATE_CROUCH);
			break;

		case STATE_JUMP:
			animator.SetInteger ("state", STATE_JUMP);
			break;

		case STATE_IDLE:
			animator.SetInteger ("state", STATE_IDLE);
			break;

		case STATE_HADOOKEN:
			animator.SetInteger ("state", STATE_HADOOKEN);
			break;

		}

		currentAnimationState = state;
	}


	//--------------------------------------
	// Flip player sprite for left/right walking
	//--------------------------------------
	void ChangeDirection(string direction)
	{

		if (currentDirection != direction)
		{
			if (direction == "right")
			{
				transform.Rotate (0, 180, 0);
				currentDirection = "right";
			}
			else if (direction == "left")
			{
				transform.Rotate (0, -180, 0);
				currentDirection = "left";
			}
		}

	}

	//--------------------------------------
	// Check if player has collided with the floor
	//--------------------------------------
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.name == "Floor")
		{
			isOnGround = true;
			ChangeState(STATE_IDLE);
		}

	}
}

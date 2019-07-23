using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour {

	private Rigidbody2D rb2D;
	public float speed;

	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D> ();

		Assert.IsNotNull (rb2D);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		rb2D.AddForce (movement * speed);
	
	}

	private void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.CompareTag ("PickUp")) {
			other.gameObject.SetActive (false);
		}
	}
}

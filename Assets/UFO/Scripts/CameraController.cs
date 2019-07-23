using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	// Use this for initialization
	void Start () {

		Assert.IsNotNull (player);

		offset = transform.position - player.transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void LateUpdate() {
		transform.position = offset + player.transform.position;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Camera : MonoBehaviour {

	public float sensitivity = 5f;
	public float xRot;
	public float yRot;
	public float currentXRot;
	public float currentYRot;
	public float xRotV;
	public float yRotV;
	public float lookSmoothDamp = 0.1f;

	public GameObject player;


	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.I)) {
			transform.RotateAround (player.transform.position, Vector3.up, 1);
		}

		if (Input.GetKey (KeyCode.P)) {
			transform.RotateAround (player.transform.position, Vector3.up, -1);
		}

		if (Input.GetMouseButton (1)) {
			Cursor.lockState = CursorLockMode.Locked;
			xRot -= Input.GetAxis ("Mouse Y") * sensitivity;
			yRot += Input.GetAxis ("Mouse X") * sensitivity;

			xRot = Mathf.Clamp (xRot, -90, 90);

			currentXRot = Mathf.SmoothDamp (currentXRot, xRot, ref xRotV, lookSmoothDamp);
			currentYRot = Mathf.SmoothDamp (currentYRot, yRot, ref yRotV, lookSmoothDamp);

			transform.rotation = Quaternion.Euler (currentXRot, currentYRot, 0);
		} 
		else
			Cursor.lockState = CursorLockMode.None;
	}
}

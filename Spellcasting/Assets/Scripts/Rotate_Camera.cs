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
		if (Input.GetMouseButtonDown (1)) {
			yRot += Input.GetAxis ("Mouse X") * sensitivity;

			//xRot = Mathf.Clamp (xRot, -90, 90);

			Debug.Log ("A: " + yRot);

			//currentXRot = Mathf.SmoothDamp (currentXRot, xRot, ref xRotV, lookSmoothDamp);
			currentYRot = Mathf.SmoothDamp (currentYRot, yRot, ref yRotV, lookSmoothDamp);

			Debug.Log ("B: " + currentYRot);

		}

		if (Input.GetMouseButton (1)) {
			Cursor.lockState = CursorLockMode.Locked;

			//xRot -= Input.GetAxis ("Mouse Y") * sensitivity;
			yRot += Input.GetAxis ("Mouse X") * sensitivity;

			//xRot = Mathf.Clamp (xRot, -90, 90);

			//currentXRot = Mathf.SmoothDamp (currentXRot, xRot, ref xRotV, lookSmoothDamp);
			currentYRot = Mathf.SmoothDamp (currentYRot, yRot, ref yRotV, lookSmoothDamp);

			transform.rotation = Quaternion.Euler (0, currentYRot, 0);
//			transform.rotation = Quaternion.Euler (currentXRot, currentYRot, 0);
		} 
		else
			Cursor.lockState = CursorLockMode.None;
	}
}

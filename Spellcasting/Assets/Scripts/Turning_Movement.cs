using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turning_Movement : MonoBehaviour {

	public float speed = 1;

	public float RotateSpeed = 1;

	public float jump_height = 8;

	bool is_falling = false;

	public bool moving = false;

	Rigidbody this_rb;

	public Cast_Manager caster;

	// Use this for initialization
	void Start () {
		this_rb = this.gameObject.GetComponent<Rigidbody>();
		caster = this.gameObject.GetComponent<Cast_Manager> ();
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.name == "Floor" || col.gameObject.tag == "Floor")
		{
			is_falling = false;
		}
	}

	void Jump()
	{
		this_rb.velocity = new Vector3(0, jump_height, 0);
		is_falling = true;
		if(caster.casting == true)
			caster.Interrupt ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
			transform.Rotate(-Vector3.up * RotateSpeed * Time.deltaTime);
		else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
			transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);

		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
		{
			transform.Translate(Vector3.forward * 0.1f * speed);
			moving = true;
			if(caster.casting == true)
				caster.Interrupt ();
		}
		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
		{
			transform.Translate(Vector3.forward * -0.1f * speed);
			moving = true;
			if(caster.casting == true)
				caster.Interrupt ();
		}

		if (Input.GetKey(KeyCode.Q)) {
			transform.Translate(Vector3.left * 0.1f * speed * 0.8f);
			moving = true;
			if(caster.casting == true)
				caster.Interrupt ();
		}
		if (Input.GetKey (KeyCode.E)) {
			transform.Translate (Vector3.right * 0.1f * speed * 0.8f);
			moving = true;
			if(caster.casting == true)
				caster.Interrupt ();
		} 

		if (!(Input.GetKey(KeyCode.UpArrow)) && !(Input.GetKey(KeyCode.DownArrow)) && !(Input.GetKey (KeyCode.W)) && !(Input.GetKey (KeyCode.S)) && !(Input.GetKey (KeyCode.Q)) && !(Input.GetKey (KeyCode.E))) {
			moving = false;
		}

		if (Input.GetKeyDown(KeyCode.Space) && is_falling == false)
			Jump();

	}


}

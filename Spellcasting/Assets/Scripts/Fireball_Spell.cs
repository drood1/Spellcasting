using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fireball_Spell: MonoBehaviour {
	public GameObject target;

	public GameObject fireball_obj;

	public float base_damage = 10;

	Rigidbody rb;

	public bool casting = false;

	public string input_key = "a";

	public float cast_time = 2.5f;
	public float CD = 3f;

	public bool OnCD = false;

	public float time_started = 0;
	public float time_completed = 0;

	public GameObject cast_bar_obj;
	public GameObject bar_bg_obj;

	public Image cast_bar;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		cast_bar_obj = GameObject.Find ("CastBar");
		bar_bg_obj = GameObject.Find ("CastBar_BG");
		cast_bar = GameObject.Find ("CastBar").GetComponent<Image>();
		cast_bar.fillAmount = 0;
	}

	void FinishCast()	{
		GameObject proj = Instantiate(fireball_obj, transform.position + (transform.forward), transform.rotation) as GameObject;
		proj.GetComponent<Fireball> ().Initialize (target, base_damage);

		//proj.GetComponent<Rigidbody>().AddForce(transform.forward * fireball_movespeed, ForceMode.Impulse);

		Debug.Log ("Cast complete!");
		casting = false;
		time_completed = Time.time;
		OnCD = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (input_key)) {
			target = GetComponent<Targeting> ().target;

			if (target == null) {
				Debug.Log ("HAVE NO TARGET DOOFUS");
			} 
			else if (rb.velocity.magnitude > 0) {
				Debug.Log ("CAN'T CAST WHILE MOVING DINGUS");
			} 
			else if (casting == true) {
				Debug.Log ("YOU'RE ALREADY CASTING A SPELL DONKUS");
			}
			else {
				Invoke ("FinishCast", cast_time);
				casting = true;
				time_started = Time.time;
				Debug.Log ("Cast started!");
			}
		}

		if (casting == true && rb.velocity.magnitude > 0) {
			Debug.Log ("SPELL CANCELLED DUE TO MOVEMENT");
			CancelInvoke ();
			casting = false;
		}


		if (casting) {
			bar_bg_obj.SetActive (true);
			cast_bar_obj.SetActive (true);
			cast_bar.fillAmount = ((Time.time - time_started) / cast_time);
		} 
		else{
			bar_bg_obj.SetActive (false);
			cast_bar_obj.SetActive (false);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fireball_Spell: MonoBehaviour {
	public GameObject target;

	public Player_Stats owner;

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

	Vector3 tar_dir;
	float tar_dir_f;
	float test;
	public float view_threshold_deg = 60f;

	public Image cast_bar;

	// Use this for initialization
	void Start () {
		owner = GameObject.Find ("Player").GetComponent<Player_Stats> ();
		rb = GetComponent<Rigidbody> ();
		cast_bar_obj = GameObject.Find ("CastBar");
		bar_bg_obj = GameObject.Find ("CastBar_BG");
		cast_bar = GameObject.Find ("CastBar").GetComponent<Image>();
		cast_bar.fillAmount = 0;
	}

	void FinishCast()	{
		tar_dir = target.transform.position - this.transform.position;
		tar_dir_f = Mathf.Atan2 (tar_dir.z, tar_dir.x) * Mathf.Rad2Deg;

		test = Mathf.Atan2 (this.transform.forward.z, this.transform.forward.x) * Mathf.Rad2Deg;

		if (tar_dir_f > test + view_threshold_deg || tar_dir_f < test - view_threshold_deg || tar_dir_f == test) {
			Debug.Log (test + " IS NOT IN FRONT OF YOU");
			OnCD = true;
			owner.casting = false;
			return;
		}

		GameObject proj = Instantiate(fireball_obj, transform.position + (transform.forward), transform.rotation) as GameObject;
		proj.GetComponent<Fireball> ().Initialize (target, base_damage);

		//proj.GetComponent<Rigidbody>().AddForce(transform.forward * fireball_movespeed, ForceMode.Impulse);

		Debug.Log ("Cast complete!");
		owner.casting = false;
		time_completed = Time.time;
		OnCD = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (input_key)) {
			test = Mathf.Atan2 (this.transform.forward.z, this.transform.forward.x) * Mathf.Rad2Deg;
			//Debug.Log("PLAYER'S 'FORWARD': " + test);

			//can't cast a spell if already casting one
			if (owner.casting == false) {
				target = GetComponent<Targeting> ().target;
				if (target != null) {
					//calculate the angle between the target and the caster (to be used to see if target is "in front" of caster)
					tar_dir = target.transform.position - this.transform.position;
					tar_dir_f = Mathf.Atan2 (tar_dir.z, tar_dir.x) * Mathf.Rad2Deg;
					//Debug.Log ("PLAYER-TARGET ANGLE: " + tar_dir_f);

					//check if caster is moving when trying to cast
					if (rb.velocity.magnitude > 0) {
						Debug.Log ("CAN'T CAST WHILE MOVING DINGUS");
					} 
					//check if target is "in front of" caster (within threshold)
					else if (tar_dir_f > test + view_threshold_deg || tar_dir_f < test - view_threshold_deg || tar_dir_f == test) {
						Debug.Log (test + " IS NOT IN FRONT OF YOU");
					}

					//all pre-checks successful, cast begins
					else {
						Invoke ("FinishCast", cast_time);
						owner.casting = true;
						time_started = Time.time;
						owner.time_cast_started = time_started;
						owner.cast_time = cast_time;
						Debug.Log ("Cast started!");
					}
				}
				else
					Debug.Log ("HAVE NO TARGET DOOFUS");
			}
			else 
				Debug.Log ("YOU'RE ALREADY CASTING A SPELL DONKUS");
		}

		//casting is interrupted by movement
		if (owner.casting == true && rb.velocity.magnitude > 0) {
			Debug.Log ("SPELL CANCELLED DUE TO MOVEMENT");
			CancelInvoke ();
			owner.casting = false;
		}

//		//cast bar appears/"fills" when casting
//		if (owner.casting == true) {
//			bar_bg_obj.SetActive (true);
//			cast_bar_obj.SetActive (true);
//			cast_bar.fillAmount = ((Time.time - time_started) / cast_time);
//		} 
//		//if spell isn't being cast, cast bar doesn't appear
//		else {
//			bar_bg_obj.SetActive (false);
//			cast_bar_obj.SetActive (false);
//		}


	}
}

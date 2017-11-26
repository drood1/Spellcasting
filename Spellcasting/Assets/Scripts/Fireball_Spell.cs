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

	public string input_key = "a";

	public float cast_time = 2.5f;
	public float CD = 3f;

	public bool OnCD = false;

	public float time_completed = 0;

	public GameObject cast_bar_obj;
	public GameObject bar_bg_obj;

	Vector3 tar_dir;
	float tar_dir_f;
	float forward_angle;
	public float view_threshold_deg = 60f;

	public Image cast_bar;
	public GameObject fireball_text;

	// Use this for initialization
	void Start () {
		owner = GameObject.Find ("Player").GetComponent<Player_Stats> ();
		rb = GetComponent<Rigidbody> ();
		cast_bar_obj = GameObject.Find ("CastBar");
		bar_bg_obj = GameObject.Find ("CastBar_BG");
		cast_bar = GameObject.Find ("CastBar").GetComponent<Image>();
		fireball_text = GameObject.Find ("Fireball_Text");
		fireball_text.SetActive (false);
		cast_bar.fillAmount = 0;
	}

	//what to do if the cast completes successfully
	void FinishCast()	{
		tar_dir = target.transform.position - this.transform.position;
		tar_dir_f = Mathf.Atan2 (tar_dir.z, tar_dir.x) * Mathf.Rad2Deg;

		forward_angle = Mathf.Atan2 (this.transform.forward.z, this.transform.forward.x) * Mathf.Rad2Deg;

		//first, cast makes sure that the target is still "in front" of the caster (don't want to shoot fireballs backwards)
		if (tar_dir_f > forward_angle + view_threshold_deg || tar_dir_f < forward_angle - view_threshold_deg || tar_dir_f == forward_angle) {
			Debug.Log (forward_angle + " IS NOT IN FRONT OF YOU");
			OnCD = true;
			owner.casting = false;
			fireball_text.SetActive (false);
			return;
		}

		//create Fireball projectile and call its initialize function to give it a target and a damage value
		GameObject proj = Instantiate(fireball_obj, transform.position + (transform.forward), transform.rotation) as GameObject;
		proj.GetComponent<Fireball> ().Initialize (target, base_damage);


		Debug.Log ("Cast complete!");
		owner.casting = false;
		time_completed = Time.time;
		OnCD = true;
		fireball_text.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		//spell's assigned button is pressed
		if (Input.GetKeyDown (input_key)) {
			forward_angle = Mathf.Atan2 (this.transform.forward.z, this.transform.forward.x) * Mathf.Rad2Deg;
			//Debug.Log("PLAYER'S 'FORWARD': " + forward_angle);

			//can't cast a spell if already casting one
			if (owner.casting == false) {
				target = GetComponent<Targeting> ().target;
				//player must have a target to be casting at
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
					else if (tar_dir_f > forward_angle + view_threshold_deg || tar_dir_f < forward_angle - view_threshold_deg || tar_dir_f == forward_angle) {
						Debug.Log (forward_angle + " IS NOT IN FRONT OF YOU");
					}

					//all pre-checks successful, cast begins
					else {
						Invoke ("FinishCast", cast_time);
						fireball_text.SetActive (true);
						owner.casting = true;
						owner.channeling = false;
						owner.time_cast_started = Time.time;
						owner.cast_time = cast_time;
						Debug.Log ("Cast started!");
					}
				}
				//spell cast fails if the caster has no target
				else
					Debug.Log ("HAVE NO TARGET DOOFUS");
			}
			//spell cast fails if the caster is already casting a spell
			else 
				Debug.Log ("YOU'RE ALREADY CASTING A SPELL DONKUS");
		}

		//casting is interrupted by movement
		if (owner.casting == true && rb.velocity.magnitude > 0) {
			Debug.Log ("SPELL CANCELLED DUE TO MOVEMENT");
			CancelInvoke ();
			owner.casting = false;
			fireball_text.SetActive (false);
		}

	}
}

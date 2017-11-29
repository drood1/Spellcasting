using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mind_Flay : MonoBehaviour {
	public GameObject target;

	public Cast_Manager owner;

	public float duration = 3f;
	public float time_between_ticks = 1;
	public int num_ticks = 3;
	public float damage_per_tick = 5;
	public int ticks_done = 0;

	public string input_key = "a";

	Rigidbody rb;

	Vector3 tar_dir;
	float tar_dir_f;
	float forward_angle;
	public float view_threshold_deg = 60f;

	public GameObject cast_bar_obj;
	public GameObject bar_bg_obj;

	public Image cast_bar;
	public Text cast_bar_text;

	public string spell_name = "Mind Flay";

	// Use this for initialization
	void Start () {
		owner = GameObject.Find ("Player").GetComponent<Cast_Manager> ();
		rb = GetComponent<Rigidbody> ();
		cast_bar_obj = GameObject.Find ("CastBar");
		bar_bg_obj = GameObject.Find ("CastBar_BG");
		cast_bar = GameObject.Find ("CastBar").GetComponent<Image>();
		cast_bar_text = GameObject.Find ("Spell_Text").GetComponent<Text>();
		cast_bar_text.text = "";
		cast_bar.fillAmount = 0;
	}

	void TickDamage()	{
		if (target != null)	{
			target.GetComponent<Stats> ().TakeDamage (damage_per_tick);
			ticks_done++;
			Debug.Log ("MIND FLAY DAMAGE DEALT! (" + ticks_done + ")");

			if (ticks_done < num_ticks) {
				Invoke ("TickDamage", time_between_ticks);
			} 
			else {
				ticks_done = 0;
				owner.casting = false;
				cast_bar_text.text = "";
			}
		}
		else {
			ticks_done = 0;
			owner.casting = false;
			cast_bar_text.text = "";
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (input_key)) {
			forward_angle = Mathf.Atan2 (this.transform.forward.z, this.transform.forward.x) * Mathf.Rad2Deg;
			//Debug.Log("PLAYER'S 'FORWARD': " + forward_angle);


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
					else if (tar_dir_f > forward_angle + view_threshold_deg || tar_dir_f < forward_angle - view_threshold_deg || tar_dir_f == forward_angle) {
						Debug.Log (forward_angle + " IS NOT IN FRONT OF YOU");
					}
					//*******************all pre-checks successful, cast begins
					else {
						owner.casting = true;
						owner.channeling = true;
						cast_bar_text.text = spell_name;
						owner.time_cast_started = Time.time;
						owner.cast_time = duration;
						//Debug.Log ("Cast started!");
						Invoke ("TickDamage", time_between_ticks);
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
			Debug.Log ("MIND FLAY CANCELLED DUE TO MOVEMENT");
			CancelInvoke ();
			ticks_done = 0;
			owner.casting = false;
			cast_bar_text.text = "";
		}
	}
}

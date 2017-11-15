using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mind_Flay : MonoBehaviour {
	public GameObject target;

	public Player_Stats owner;

	public float duration = 3f;
	public float num_ticks = 3;
	public float damage_per_tick = 5;

	public string input_key = "a";

	Rigidbody rb;

	Vector3 tar_dir;
	float tar_dir_f;
	float forward_angle;
	public float view_threshold_deg = 60f;

	public GameObject cast_bar_obj;
	public GameObject bar_bg_obj;
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

	void TickDamage(float amount)	{
		target.GetComponent<Stats>().TakeDamage(amount);
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
					//all pre-checks successful, cast begins
					else {
						owner.casting = true;
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
	}
}

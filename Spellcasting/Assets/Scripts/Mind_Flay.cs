using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mind_Flay : MonoBehaviour {
	public GameObject target;

	public float duration = 3f;
	public float num_ticks = 3;
	public float damage_per_tick = 5;


	public string input_key = "a";

	Rigidbody rb;

	public bool casting = false;

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

	void TickDamage(float amount)	{
		target.GetComponent<Stats>().TakeDamage(amount);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (input_key)) {
			//test = Mathf.Atan2 (this.transform.forward.z, this.transform.forward.x) * Mathf.Rad2Deg;
			//Debug.Log("PLAYER'S 'FORWARD': " + test);

			//can't cast a spell if already casting one
			if (casting == false) {
				target = GetComponent<Targeting> ().target;
				if (target != null) {

					//check if caster is moving when trying to cast
					if (rb.velocity.magnitude > 0) {
						Debug.Log ("CAN'T CAST WHILE MOVING DINGUS");
					}
					//all pre-checks successful, cast begins
					else {
						casting = true;
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
		if (casting == true && rb.velocity.magnitude > 0) {
			Debug.Log ("SPELL CANCELLED DUE TO MOVEMENT");
			CancelInvoke ();
			casting = false;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Spell
{
	string name;
	int id;
	bool channeled;
	float cast_time;
	float direct_damage;
	float duration;
	float damage_per_tick;
	float num_ticks;

	public Spell(string n, int i, bool c, float ct, float dd, float d, float dpt,  float nt)	{
		name = n; 
		id = i;
		channeled = c;
		cast_time = ct;

		//channeled, periodic spell (i.e. Mind Flay)
		if (channeled == true) {
			direct_damage = 0;
			duration = d;
			damage_per_tick = d;
			num_ticks = nt;
		} 	
		//cast-time, direct-damage spell (i.e. Fireball)
		else {
			direct_damage = dd;
			duration = 0;
			damage_per_tick = 0;
			num_ticks = 0;
		}
	}

}


public class Cast_Manager : MonoBehaviour {
	public bool casting = false;
	public bool channeling = false;
	public bool moving = false;

	public float time_cast_started = 0;

	public GameObject cast_bar_obj;
	public GameObject bar_bg_obj;

	public Image cast_bar;

	public float cast_time = 0;


	// Use this for initialization
	void Start () {
		cast_bar_obj = GameObject.Find ("CastBar");
		bar_bg_obj = GameObject.Find ("CastBar_BG");
		cast_bar = GameObject.Find ("CastBar").GetComponent<Image>();
		cast_bar.fillAmount = 0;
	}

	public void Interrupt()	{
		Debug.Log ("INTERRUPTING (need implementation)");
	}
	
	// Update is called once per frame
	void Update () {
		//CAST BAR FOR *****NON-CHANNELING*****
		if (channeling == false) {
			//cast bar appears/"fills" when casting
			if (casting == true) {
				bar_bg_obj.SetActive (true);
				cast_bar_obj.SetActive (true);
				cast_bar.fillAmount = ((Time.time - time_cast_started) / cast_time);
			} 

			//if spell isn't being cast, cast bar doesn't appear
			else {
				bar_bg_obj.SetActive (false);
				cast_bar_obj.SetActive (false);
				cast_bar.fillAmount = 0;
			}
		} 
		//CAST BAR FOR ****CHANNELING******
		else {
			if (casting == true) {
				bar_bg_obj.SetActive (true);
				cast_bar_obj.SetActive (true);
				cast_bar.fillAmount = ((cast_time - (Time.time - time_cast_started)) / cast_time);
			} 

			//if spell isn't being cast, cast bar doesn't appear
			else {
				bar_bg_obj.SetActive (false);
				cast_bar_obj.SetActive (false);
				cast_bar.fillAmount = 0;
			}
		}

		/*

		//NEED TO FIGURE OUT SOME WAY TO HAVE A "CURRENT_SPELL" VARIABLE
		//FOR THIS, I'D NEED TO BE ABLE TO PLUG IN *ANY* C# SCRIPT AS A PARAMETER

		//casting is interrupted by movement
		if (owner.casting == true && rb.velocity.magnitude > 0) {
			Debug.Log ("FIREBALL CANCELLED DUE TO MOVEMENT");
			CancelInvoke ();
			owner.casting = false;
			cast_bar_text.text = "";
		}

		*/
	}
}

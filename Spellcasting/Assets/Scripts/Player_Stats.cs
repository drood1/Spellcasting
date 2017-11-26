using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Stats : MonoBehaviour {
	public float max_hp = 100;
	public float current_hp;

	public float max_mana = 100;
	public float current_mana;

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
		current_hp = max_hp;
		current_mana = max_mana;

		cast_bar_obj = GameObject.Find ("CastBar");
		bar_bg_obj = GameObject.Find ("CastBar_BG");
		cast_bar = GameObject.Find ("CastBar").GetComponent<Image>();
		cast_bar.fillAmount = 0;
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


	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target_Display : MonoBehaviour {
	public GameObject target;
	public Stats target_stats;

	public Image target_frame;
	public Image HP_Bar;
	public Image Mana_Bar;
	public Image Portrait;

	public Sprite p;

	// Use this for initialization
	void Start () {
		HP_Bar = GameObject.Find ("Target_HP").GetComponent<Image> ();
		Mana_Bar = GameObject.Find ("Target_Mana").GetComponent<Image> ();
		target_frame = GameObject.Find ("Target_Frame").GetComponent<Image> ();
		Portrait = GameObject.Find ("Target_Portrait").GetComponent<Image> ();
	}

	public void UpdateTarget(GameObject t)	{
		if (t == null) {
			target_frame.enabled = false;
			HP_Bar.enabled = false;
			Mana_Bar.enabled = false;
			Portrait.enabled = false;
		}
		else {
			target = t;
			target_stats = target.GetComponent<Stats> ();
			target_frame.enabled = true;
			HP_Bar.enabled = true;
			Mana_Bar.enabled = true;
			Portrait.enabled = true;
			//Debug.Log("UPDATING DAT PORTRAIT");
			Portrait.sprite = target.GetComponent<Stats> ().target_portrait;
			//Debug.Log (target.GetComponent<Stats> ().target_portrait.name);
		}

	}

	public void UpdateBars()	{
		//Debug.Log ("CURRENT HP: " + target_stats.current_hp);
		//Debug.Log ("MAX HP: " + target_stats.max_hp);
		HP_Bar.fillAmount = target_stats.current_hp / target_stats.max_hp;
		//Mana_Bar.fillAmount = target_stats.current_mana / target_stats.max_mana;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

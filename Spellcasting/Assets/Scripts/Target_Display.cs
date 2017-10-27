using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target_Display : MonoBehaviour {
	public GameObject target;
	public Stats target_stats;

	public Image HP_Bar;
	public Image Mana_Bar;


	// Use this for initialization
	void Start () {
		HP_Bar = GameObject.Find ("Target_HP").GetComponent<Image> ();
		Mana_Bar = GameObject.Find ("Target_Mana").GetComponent<Image> ();
	}

	public void UpdateTarget(GameObject t)	{
		target = t;
		target_stats = target.GetComponent<Stats> ();
	}

	public void UpdateBars()	{
		Debug.Log ("CURRENT HP: " + target_stats.current_hp);
		Debug.Log ("MAX HP: " + target_stats.max_hp);
		HP_Bar.fillAmount = target_stats.current_hp / target_stats.max_hp;
		//Mana_Bar.fillAmount = target_stats.current_mana / target_stats.max_mana;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {
	public float max_hp = 100;
	public float current_hp;

	public Sprite target_portrait;

	public Target_Display t_display;

	// Use this for initialization
	void Start () {
		current_hp = max_hp;
		t_display = GameObject.Find ("Player").GetComponent<Target_Display> ();
	}

	public void TakeDamage(float d)	{
		current_hp -= d;
		t_display.UpdateBars ();

		if (current_hp <= 0) {
			Debug.Log (this.gameObject.name = " DIED!");
			t_display.UpdateTarget (null);
			Destroy (this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}

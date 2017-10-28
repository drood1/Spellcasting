using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Targeting : MonoBehaviour {
	public GameObject target;

	public GameObject targeting_indicator;

	public GameObject target_portrait;

	public Target_Display t_display;

	// Use this for initialization
	void Start () {
		target_portrait = GameObject.Find ("Target_Portrait");
		t_display = GameObject.Find ("Player").GetComponent<Target_Display> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.collider.gameObject.tag.Contains("Target")) {
					if(target !=null && target != hit.collider.gameObject)	{
						target.transform.GetChild (0).gameObject.GetComponent<Renderer> ().enabled = false;
					}
					target = hit.collider.gameObject;
					t_display.UpdateTarget (target);
					t_display.UpdateBars ();
					hit.collider.gameObject.transform.GetChild (0).gameObject.GetComponent<Renderer> ().enabled = true;
					Debug.Log ("TARGET SET TO " + hit.collider.gameObject.name);
					//target_portrait.
				}
			}


		}
	}
}

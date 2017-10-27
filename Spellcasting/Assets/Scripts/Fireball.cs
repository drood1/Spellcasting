using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {
	public GameObject target;
	public float damage;

	public float dist_to_target;

	public float move_speed = 1;
	public float turn_speed = 1;

	Vector3 target_dir;
	Vector3 new_dir;

	public void Initialize(GameObject t, float d)	{
		target = t;
		damage = d;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		target_dir = target.transform.position - transform.position;
		new_dir = Vector3.RotateTowards (transform.forward, target_dir, turn_speed, 0.0f);
		transform.rotation = Quaternion.LookRotation (new_dir);

		transform.position = Vector3.MoveTowards (transform.position, target.transform.position, move_speed * 0.1f);
		dist_to_target = Vector3.Distance (transform.position, target.transform.position);


		if (dist_to_target < 1) {
			//deal damage to the target
			target.GetComponent<Stats>().TakeDamage(damage);
			Debug.Log("FIREBALL HIT " + target.name);
			Destroy (this.gameObject);
		}
	}
}

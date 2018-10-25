using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetVelocity : MonoBehaviour {
	private Rigidbody rd;
	private float velocity;
	public Animator anim;
	
	// Use this for initialization
	void Start () {
		rd = GetComponent<Rigidbody>();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		velocity = rd.velocity.magnitude;
		
		if (velocity > 0.5f){
			anim.SetBool("Moving", true);
		} else anim.SetBool("Moving", false);
	}
}

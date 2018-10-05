using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundPosParticle : MonoBehaviour {

	public GameObject GroundPosParticle;

	public Vector3 GroundPosHitPoint;
	public GameObject RayOrigin;
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit GroundPosHitPoint;
		if (Physics.Raycast(RayOrigin.transform.position,(RayOrigin.transform.up * -1),out GroundPosHitPoint,100))
		{

			GroundPosParticle.transform.position= GroundPosHitPoint.point;
			//Debug.Log(GroundPosHitPoint.point);
		}
		
	}
}

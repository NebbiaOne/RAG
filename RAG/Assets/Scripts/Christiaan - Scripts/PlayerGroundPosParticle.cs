using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerGroundPosParticle : MonoBehaviour {
	Overlord_Main _Overlord;
	public GameObject GroundPosParticle;
	public Vector3 GroundPosHitPoint;
	public GameObject RayOrigin;
	void Start () {
		_Overlord = Overlord_Main._Overlord_main;
	}
	void Update () {
		if (_Overlord.playAble == true) {
			RaycastHit GroundPosHitPoint;
			if (Physics.Raycast (RayOrigin.transform.position, (RayOrigin.transform.up * -1), out GroundPosHitPoint, 100)) {
				GroundPosParticle.transform.position = GroundPosHitPoint.point;
				//Debug.Log(GroundPosHitPoint.point);
			}
		}
	}
}
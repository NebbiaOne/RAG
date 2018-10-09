﻿using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
public class Player_Movement : MonoBehaviour {
	Overlord_Main _Overlord;
	Player rwInput;
	Rigidbody rbPlayer;
	Quaternion inputRotation, movementDirection;
	bool dashWaited = false, dashRecharging = false, dashAble = true;
	float horizontalMovmement, verticalMovement, jumpCounter = 3f;
	[SerializeField]
	float movementSpeed = 150, jumpForce = 75, rotationSpeed = 50, maxSpeed = 5f, dashStrength = 125f, dashUseWaiter = 0.25f, dashRechargeWaiter = 1f;
	int dashCharges = 3;
	void Awake () {
		_Overlord = Overlord_Main._Overlord_main;
	}
	void Start () {
		if (gameObject.tag == "Player_01") {
			rwInput = ReInput.players.GetPlayer (0);
		}
		if (gameObject.tag == "Player_02") {
			rwInput = ReInput.players.GetPlayer (1);
		}
		if (gameObject.tag == "Player_03") {
			rwInput = ReInput.players.GetPlayer (2);
		}
		if (gameObject.tag == "Player_04") {
			rwInput = ReInput.players.GetPlayer (3);
		}

		rbPlayer = gameObject.transform.GetComponent<Rigidbody> ();
	}
	void Update () {
		//Debug.Log (dashCharges);
		//Debug.Log (new Vector2 (rbPlayer.velocity.x, rbPlayer.velocity.z).magnitude);

		if (dashCharges != 3 && dashRecharging == false) {
			dashRecharging = true;
			StartCoroutine (DashRecharge ());
		}
		if (dashWaited == true && dashAble == false) {
			if (rwInput.GetButton ("Dash")) {
				dashWaited = false;
				dashAble = true;
			}
		}
		if (rwInput.GetButton ("Dash") && dashCharges != 0 && dashAble == true) {
			dashAble = false;
			dashCharges -= 1;
			if (new Vector2 (rbPlayer.velocity.x, rbPlayer.velocity.z).magnitude > maxSpeed) {
				if (dashRecharging == false) {
					dashRecharging = true;
					StartCoroutine (DashRecharge ());
				}
			}
			StartCoroutine (Dash ());
		}
		if (Mathf.Round (rbPlayer.velocity.y * 1000) / 1000 == 0f) {
			jumpCounter = 3f;
		}
		if (rwInput.GetAxis ("CL_Horizontal") == 0 && rwInput.GetAxis ("CL_Vertical") == 0 && rwInput.GetAxis ("CM_Horizontal") == 0 && rwInput.GetAxis ("CM_Vertical") == 0) {
			transform.rotation = Quaternion.RotateTowards (transform.rotation, inputRotation, rotationSpeed);
		}
		if (rwInput.GetAxis ("CL_Horizontal") != 0 || rwInput.GetAxis ("CL_Vertical") != 0) {

			inputRotation = Quaternion.Euler (new Vector3 (0f, Mathf.Atan2 (rwInput.GetAxis ("CL_Horizontal"), rwInput.GetAxis ("CL_Vertical")) * 180 / Mathf.PI));
			transform.rotation = Quaternion.RotateTowards (transform.rotation, inputRotation, rotationSpeed);
		}
		if (rwInput.GetAxis ("CM_Vertical") != 0 || rwInput.GetAxis ("CM_Horizontal") != 0) {
			horizontalMovmement = rwInput.GetAxis ("CM_Horizontal");
			verticalMovement = rwInput.GetAxis ("CM_Vertical");
			if (rwInput.GetAxis ("CL_Horizontal") == 0 && rwInput.GetAxis ("CL_Vertical") == 0) {
				inputRotation = Quaternion.Euler (new Vector3 (0f, Mathf.Atan2 (rwInput.GetAxis ("CM_Horizontal"), rwInput.GetAxis ("CM_Vertical")) * 180 / Mathf.PI));
				transform.rotation = Quaternion.RotateTowards (transform.rotation, inputRotation, rotationSpeed);
			}
			if (new Vector2 (rbPlayer.velocity.x, rbPlayer.velocity.z).magnitude < maxSpeed) {
				rbPlayer.AddForce (new Vector3 (horizontalMovmement * movementSpeed, 0f, verticalMovement * movementSpeed));
			}
		}
		if (rwInput.GetButtonDown ("C_Jump") && jumpCounter != 0f) {
			rbPlayer.AddRelativeForce (transform.up * jumpForce, ForceMode.Impulse);
			CountJump ();
		}
	}
	void CountJump () {
		jumpCounter -= 1;
	}
	IEnumerator Dash () {
		if (rwInput.GetAxis ("CM_Vertical") == 0 && rwInput.GetAxis ("CM_Horizontal") == 0 && rwInput.GetAxis ("CL_Vertical") == 0 && rwInput.GetAxis ("CL_Horizontal") == 0) {
			rbPlayer.AddForce (Vector3.down * dashStrength, ForceMode.Impulse);
		} else {
			rbPlayer.AddForce (new Vector3 (horizontalMovmement * dashStrength, 0f, verticalMovement * dashStrength), ForceMode.Impulse);
		}
		yield return new WaitForSeconds (dashUseWaiter);
		dashWaited = true;
	}
	IEnumerator DashRecharge () {
		Debug.Log ("Recharging");
		yield return new WaitForSeconds (dashRechargeWaiter);
		dashCharges += 1;
		dashRecharging = false;
	}
}
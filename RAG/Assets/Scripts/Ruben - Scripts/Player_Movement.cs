using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.UI;
public class Player_Movement : MonoBehaviour {
	Overlord_Main _Overlord;
	Player_Main _PlayerMain;
	Player rwInput;
	Rigidbody rbPlayer;
	Image charge_01, charge_02, charge_03;
	Quaternion inputRotation, movementDirection;
	bool dashWaited = false, dashRecharging = false, dashAble = true;
	bool p_01 = false, p_02 = false, p_03 = false, p_04 = false;
	float horizontalMovmement, verticalMovement, jumpCounter = 1f;
	[SerializeField]
	GameObject hud;
	[SerializeField]
	float movementSpeed = 200, jumpForce = 150, rotationSpeed = 50, maxSpeed = 5f, dashStrength = 125f, dashUseWaiter = 0.25f, dashRechargeWaiter = 1f;
	[SerializeField]
	int dashCharges = 3;
	void Start () {
		_Overlord = Overlord_Main._Overlord_main;
		_PlayerMain = Player_Main._Player_Main;
		hud = _PlayerMain.hud;
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
		if (_Overlord.playAble == true) {
			if (charge_01 == null || charge_02 == null || charge_03 == null) {
				charge_01 = hud.transform.GetChild (0).gameObject.GetComponent<Image> ();
				charge_02 = hud.transform.GetChild (1).gameObject.GetComponent<Image> ();
				charge_03 = hud.transform.GetChild (2).gameObject.GetComponent<Image> ();
			}
			switch (dashCharges) {
				case 0:
					charge_01.enabled = false;
					charge_02.enabled = false;
					charge_03.enabled = false;
					break;
				case 1:
					charge_01.enabled = true;
					charge_02.enabled = false;
					charge_03.enabled = false;
					break;
				case 2:
					charge_01.enabled = true;
					charge_02.enabled = true;
					charge_03.enabled = false;
					break;
				case 3:
					charge_01.enabled = true;
					charge_02.enabled = true;
					charge_03.enabled = true;
					break;
			}
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
				jumpCounter = 1f;
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
		yield return new WaitForSeconds (dashRechargeWaiter);
		dashCharges += 1;
		dashRecharging = false;
	}
}
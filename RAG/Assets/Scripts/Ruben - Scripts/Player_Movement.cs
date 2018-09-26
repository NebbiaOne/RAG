using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
public class Player_Movement : MonoBehaviour {
	Player rwInput;
	Rigidbody rbPlayer;
	Quaternion inputRotation, movementDirection;
	bool dashAble = true;
	float horizontalMovmement, verticalMovement, jumpCounter = 3f;
	[SerializeField]
	float movementSpeed, jumpForce, rotationSpeed, maxSpeed = 5f, dashStrength = 125f, dashWaiter = 0.25f;
	void Start () {
		rwInput = ReInput.players.GetPlayer (0);
		rbPlayer = gameObject.transform.GetComponent<Rigidbody> ();
	}
	void Update () {
		Debug.Log (Mathf.Atan2 (rwInput.GetAxis ("CM_Horizontal"), rwInput.GetAxis ("CM_Vertical")) * 180 / Mathf.PI);
		if (rwInput.GetButton ("Dash") && dashAble == true) {
			dashAble = false;
			StartCoroutine (Dash ());
		}
		if (Mathf.Round (rbPlayer.velocity.y * 100) / 100 == 0f) {
			jumpCounter = 3f;
		}
		if (rwInput.GetButtonDown ("Action")) {
			Debug.Log ("Action!");
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
		rbPlayer.AddForce (new Vector3 (horizontalMovmement * dashStrength, 0f, verticalMovement * dashStrength), ForceMode.Impulse);
		yield return new WaitForSeconds (dashWaiter);
		dashAble = true;
	}
}
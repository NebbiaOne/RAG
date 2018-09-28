using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
public class Player_Combat : MonoBehaviour {
	Player rwInput;
	bool attacking = false, attackAble = true, blocking = false, blockAble = true;
	[SerializeField]
	float attackSpeed = 0.5f, blockTime = 2f, blockCooldown = 1f;
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
	}
	void Update () {
		if (rwInput.GetButtonDown ("Action")) {
			Debug.Log ("Action!");
		}
		if (rwInput.GetAxis ("Attack") > 0f && attackAble == true && blocking == false) {
			if (attacking != true) {
				attacking = true;
			}
			attackAble = false;
			gameObject.transform.GetChild (0).gameObject.SetActive (true);
			StartCoroutine (AttackWaiter ());
		} else if (rwInput.GetAxis ("Attack") <= 0f || attackAble == false) {
			if (attacking != false) {
				attacking = false;
			}
			gameObject.transform.GetChild (0).gameObject.SetActive (false);

		}
		if (rwInput.GetAxis ("Block") > 0f && blockAble == true && attacking == false) {
			if (blocking != true) {
				blocking = true;
			}
			gameObject.transform.GetChild (1).gameObject.SetActive (true);
			StartCoroutine (BlockWaiter ());
		} else if (rwInput.GetAxis ("Block") <= 0f || blockAble == false) {
			if (blocking != false) {
				blocking = false;
			}
			gameObject.transform.GetChild (1).gameObject.SetActive (false);
		}
	}
	IEnumerator AttackWaiter () {
		yield return new WaitForSeconds (attackSpeed);
		attackAble = true;
	}
	IEnumerator BlockWaiter () {
		yield return new WaitForSeconds (blockTime);
		blockAble = false;
		StartCoroutine (BlockCooldown ());
	}
	IEnumerator BlockCooldown () {
		yield return new WaitForSeconds (blockCooldown);
		blockAble = true;
	}
}
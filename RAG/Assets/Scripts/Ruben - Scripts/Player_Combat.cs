using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
public class Player_Combat : MonoBehaviour {
	Player_Main _Main;
	Player rwInput;
	RaycastHit hit;
	bool attacking = false, attackWaited = false, attackAble = true, blockAble = true;
	int layerMask = 1 << 12;
	[SerializeField]
	float attackSpeed = 0.5f, blockTime = 2f, blockCooldown = 1f;
	public float playerHealth = 100;
	void Start () {
		_Main = transform.GetComponent<Player_Main> ();
		layerMask = ~layerMask;
		Debug.Log(layerMask);
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
		if (attackWaited == true && attackAble == false) {
			if (rwInput.GetAxis ("Attack") == 0f) {
				attackWaited = false;
				attackAble = true;
			}
		}
		if (rwInput.GetAxis ("Attack") > 0f && attackAble == true && _Main.blocking == false) {
			Debug.DrawRay (transform.position, transform.position + (this.transform.position - _Main.target.transform.position) * -25f, Color.green, Mathf.Infinity);
			if (Physics.Raycast(transform.position, transform.position + (this.transform.position - _Main.target.transform.position) * -25f, out hit, 5f, layerMask))
			{
				if (hit.collider.tag == "Shield")
				{
					Debug.Log("Shield");
				} else if (hit.collider.tag != "Shield") {
					Debug.Log("Np Shield");
					_Main.DamagePlayer();
				}	
			}
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
			if (_Main.blocking != true) {
				_Main.blocking = true;
			}
			gameObject.transform.GetChild (1).gameObject.SetActive (true);
			StartCoroutine (BlockWaiter ());
		} else if (rwInput.GetAxis ("Block") <= 0f || blockAble == false) {
			if (_Main.blocking != false) {
				_Main.blocking = false;
			}
			gameObject.transform.GetChild (1).gameObject.SetActive (false);
		}
	}
	IEnumerator AttackWaiter () {
		yield return new WaitForSeconds (attackSpeed);
		attackWaited = true;
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
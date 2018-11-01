using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
public class Selection_Stands : MonoBehaviour {
	/* - Remember to tag stands with the player tags
	 */
	Overlord_Main _Overlord;
	Overlord_Ghost _Ghost;
	Player rwInput;
	GameObject stand, indicator_01, indicator_02, indicator_Ready, player;
	Color col_pink = new Vector4 (1f, 0f, 0.95f, 1f);
	Color col_white = new Vector4 (1f, 1f, 1f, 1f);
	bool readyUpAble = false, UnReadyAble = false, ready = false;
	void Start () {
		_Overlord = Overlord_Main._Overlord_main;
		_Ghost = Overlord_Ghost._Overlord_Ghost;
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
		stand = this.gameObject;
		indicator_01 = stand.transform.GetChild (0).gameObject;
		indicator_02 = stand.transform.GetChild (1).GetChild (0).gameObject;
		indicator_Ready = stand.transform.GetChild (1).gameObject;
		player = stand.transform.GetChild (3).gameObject;
		player.SetActive (false);
	}
	void Update () {
		if (_Overlord.paused == false) {
			if (indicator_Ready.transform.GetComponent<Renderer> ().material.color != col_pink && ready == true) {
				indicator_Ready.transform.GetComponent<Renderer> ().material.color = col_pink;
			} else if (indicator_Ready.transform.GetComponent<Renderer> ().material.color != col_white && ready == false) {
				indicator_Ready.transform.GetComponent<Renderer> ().material.color = col_white;
			}
			if (rwInput.GetButton ("C_Jump") && ready == false) {
				if (readyUpAble == false && indicator_01.activeSelf != false) {
					_Overlord.playersJoined += 1;
					player.SetActive (true);
					indicator_01.SetActive (false);
					Invoke ("Readying", 0.25f);
					if (gameObject.tag == "Player_01") {
						_Ghost.player_01 = true;
					}
					if (gameObject.tag == "Player_02") {
						_Ghost.player_02 = true;
					}
					if (gameObject.tag == "Player_03") {
						_Ghost.player_03 = true;
					}
					if (gameObject.tag == "Player_04") {
						_Ghost.player_04 = true;
					}
				}
				if (readyUpAble == true) {
					ready = true;
					_Overlord.playersReady += 1;
					Invoke ("Readied", 0.25f);
				}
			}
			if (rwInput.GetButton ("C_Jump") && ready == true) {
				if (readyUpAble == false) {
					ready = false;
					_Overlord.playersReady -= 1;
					Invoke ("Readying", 0.25f);
				}
			}
			if (rwInput.GetButton ("Cancel") && readyUpAble == true || rwInput.GetButton ("Cancel") && ready == true) {
				_Overlord.playersJoined -= 1;
				if (ready == true) {
					ready = false;
					_Overlord.playersReady -= 1;
				}
				readyUpAble = false;
				player.SetActive (false);
				indicator_01.SetActive (true);
				indicator_02.SetActive (false);
			}
		}
	}
	void Readying () {
		readyUpAble = true;
		if (indicator_02.activeSelf == false) {
			indicator_02.SetActive (true);
		}
	}
	void Readied () {
		readyUpAble = false;
	}
}
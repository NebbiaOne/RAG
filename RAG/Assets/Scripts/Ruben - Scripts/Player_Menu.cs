using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
public class Player_Menu : MonoBehaviour {
	Overlord_Main _Overlord;
	Player rwInput;
	[SerializeField]
	float pauseTimer = 0.25f;
	void Awake () {
		_Overlord = Overlord_Main._Overlord_main;
	}
	void Start () {
		_Overlord = Overlord_Main._Overlord_main;

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
		if (rwInput.GetButton ("Pause") && _Overlord.pausable == true) {
			_Overlord.pausable = false;
			if (_Overlord.paused == true) {
				_Overlord.paused = false;
			} else {
				_Overlord.paused = true;
			}
			StartCoroutine (PauseWaiter ());
		}
	}
	IEnumerator PauseWaiter () {
		yield return new WaitForSeconds (pauseTimer);
		if (_Overlord.pausable == false) {
			_Overlord.pausable = true;
		} else {
			_Overlord.pausable = false;
		}
	}
}
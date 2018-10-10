using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
public class Stand_CharacterSelection : MonoBehaviour {
	Overlord_Main _Overlord;
	Player rwInput;
	GameObject stand, indicator_01, indicator_02;
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
		stand = this.gameObject;
		indicator_01 = stand.transform.GetChild(0).gameObject;
		indicator_02 = stand.transform.GetChild(1).GetChild(0).gameObject;
	}
	void Update () {
		if (rwInput.GetButton("C_Jump"))
		{
			if (indicator_01.activeSelf != false)
			{
				indicator_01.SetActive(false);
			}			
		}
	}
}
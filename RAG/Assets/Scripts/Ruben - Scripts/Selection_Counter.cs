using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Selection_Counter : MonoBehaviour {
	Overlord_Main _Overlord;
	Loader_Main _Loader;
	Text counterText;
	bool countDownStarted = false, allConnected = false;
	void Start () {
		_Overlord = Overlord_Main._Overlord_main;
		_Loader = Loader_Main._Loader_Main;
		counterText = transform.GetComponent<Text> ();
		counterText.enabled = false;
	}
	void Update () {
		if (_Overlord.playersReady == _Overlord.playersJoined && _Overlord.playersJoined != 0 && countDownStarted == false) {
			countDownStarted = true;
			allConnected = true;
			counterText.enabled = true;
			StartCoroutine (CountDown ());
		}
		if (_Overlord.playersReady < _Overlord.playersJoined || _Overlord.playersJoined == 0 ) {
			countDownStarted = false;
			allConnected = false;
			counterText.text = "3";
			counterText.enabled = false;
		}
	}
	IEnumerator CountDown () {
		if (allConnected == false) {
			yield break;
		}
		counterText.text = "3";
		yield return new WaitForSeconds (1);
		if (allConnected == false) {
			yield break;
		}
		counterText.text = "2";
		yield return new WaitForSeconds (1);
		if (allConnected == false) {
			yield break;
		}
		counterText.text = "1";
		yield return new WaitForSeconds (1);
		counterText.text = "0";
		if (allConnected == true) {
			_Loader.Load_Arena ();
			_Loader.Initiate_Load();	
		} else {
			yield break;
		}
	}
}
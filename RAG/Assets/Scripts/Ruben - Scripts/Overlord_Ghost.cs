using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Overlord_Ghost : MonoBehaviour {
	public static Overlord_Ghost _Overlord_Ghost;
	Overlord_Main _Overlord;
	Loader_Main _Loader;
	bool levelChecked = false;
	public bool player_01 = false, player_02 = false, player_03 = false, player_04 = false;
	void Awake () {
		_Overlord_Ghost = this;
	}
	void Start () {
		_Overlord = Overlord_Main._Overlord_main;
		_Loader = _Overlord.gameObject.transform.GetComponent<Loader_Main> ();
		DontDestroyOnLoad (this.gameObject);
	}
	void Update () {
		if (_Loader.nextScene == "Arena_01" && levelChecked == false) {
			levelChecked = true;
		}
	}
}
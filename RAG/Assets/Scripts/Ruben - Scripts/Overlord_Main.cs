using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Overlord_Main : MonoBehaviour {
	public static Overlord_Main _Overlord_main;
	GameObject overlord;
	bool pauseChecked = true, inGame = false;
	public int playerID = 0;
	[SerializeField]
	GameObject menu;
	[HideInInspector]
	public bool pausable = true, paused = false;
	void Awake () {
		overlord = this.gameObject;
		_Overlord_main = this;
	}
	void Start () {
		if (SceneManager.GetActiveScene ().name != "Loading" && SceneManager.GetActiveScene ().name != "MainMenu" && inGame != true) {
			inGame = true;
		} else if ((SceneManager.GetActiveScene ().name == "Loading" || SceneManager.GetActiveScene ().name == "MainMenu") && inGame != false) {
			inGame = false;
		}
	}
	void Update () {
		if (SceneManager.GetActiveScene ().name != "Loading" && SceneManager.GetActiveScene ().name != "MainMenu" && inGame != true) {
			inGame = true;
		} else if ((SceneManager.GetActiveScene ().name == "Loading" || SceneManager.GetActiveScene ().name == "MainMenu") && inGame != false) {
			inGame = false;
		}
		if (inGame == true && menu == null) {
			menu = GameObject.FindWithTag ("Menu");
		}
		if (inGame == true && paused == true) {
			if (menu.activeSelf != true) {
				menu.SetActive (true);
			}
		}
		if (inGame == true && paused == false) {
			if (menu.activeSelf != false) {
				menu.SetActive (false);
			}
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (paused == false && pauseChecked == true && inGame == true) {
				Debug.Log ("Paused");
				paused = true;
				pauseChecked = false;
				menu.SetActive (true);
				Invoke ("PauseChecker", 0.25f);
			}
			if (paused == true && pauseChecked == true && inGame == true) {
				Debug.Log ("Not Paused");
				paused = false;
				pauseChecked = false;
				menu.SetActive (false);
				Invoke ("PauseChecker", 0.25f);
			}
		}
	}
	void PauseChecker () {
		pauseChecked = true;
	}
}
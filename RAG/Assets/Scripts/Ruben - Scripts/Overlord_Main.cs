﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Overlord_Main : MonoBehaviour {
	public static Overlord_Main _Overlord_main;
	GameObject overlord;
	bool pauseChecked = true, inGame = false, choiceActive = false;
	public GameObject menuMain;
	public GameObject menuChoice;
	[HideInInspector]
	public int playersJoined = 0, playersReady = 0;
	public int playerID = 0;
	public bool playAble = false, pausable = true, paused = false;
	public bool player_01 = false, player_02 = false, player_03 = false, player_04 = false;
	void Awake () {
		overlord = this.gameObject;
		_Overlord_main = this;
	}
	void Start () {
		if (SceneManager.GetActiveScene ().name != "Loading" && SceneManager.GetActiveScene ().name != "MainMenuMain" && inGame != true) {
			inGame = true;
		} else if ((SceneManager.GetActiveScene ().name == "Loading" || SceneManager.GetActiveScene ().name == "MainMenuMain") && inGame != false) {
			inGame = false;
		}
		if (menuMain == null && inGame == false) {
			menuMain = GameObject.Find ("Menu_Game");
		}
		if (menuChoice == null && inGame == false) {
			menuChoice = GameObject.Find ("Menu_Game_Choice");
		}
		if (menuChoice.activeSelf != false)
		{
			menuChoice.SetActive(false);
		}
	}
	void Update () {
		if (SceneManager.GetActiveScene ().name != "Loading" && SceneManager.GetActiveScene ().name != "MainMenuMain" && inGame != true) {
			inGame = true;
		} else if ((SceneManager.GetActiveScene ().name == "Loading" || SceneManager.GetActiveScene ().name == "MainMenuMain") && inGame != false) {
			inGame = false;
		}
		if (SceneManager.GetActiveScene ().name == "Arena_01" && playAble != true) {
			playAble = true;
		} else if (SceneManager.GetActiveScene ().name != "Arena_01" && playAble != false) {
			playAble = false;

		}
		if (inGame == true && menuMain == null) {
			menuMain = GameObject.FindWithTag ("MenuMain");
		}
		if (inGame == true && paused == true) {
			if (menuMain.activeSelf != true && choiceActive == false) {
				menuMain.SetActive (true);
			}
		}
		if (inGame == true && paused == false) {
			if (menuMain.activeSelf != false) {
				menuMain.SetActive (false);
			}
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (paused == false && pauseChecked == true && inGame == true) {
				Debug.Log ("Paused");
				paused = true;
				pauseChecked = false;
				menuMain.SetActive (true);
				Invoke ("PauseChecker", 0.25f);
			}
			if (paused == true && pauseChecked == true && inGame == true) {
				Debug.Log ("Not Paused");
				paused = false;
				pauseChecked = false;
				menuMain.SetActive (false);
				Invoke ("PauseChecker", 0.25f);
			}
		}
	}
	void PauseChecker () {
		pauseChecked = true;
	}
	public void MenuChoiceVisible () {
		choiceActive = true;
		menuChoice.SetActive(true);
		menuMain.SetActive(false);
	}
	public void MenuMainVisible () {
		choiceActive = false;
		menuChoice.SetActive (false);
		menuMain.SetActive (true);
	}
}
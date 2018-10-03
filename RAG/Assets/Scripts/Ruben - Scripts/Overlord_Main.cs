﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Overlord_Main : MonoBehaviour {
	public static Overlord_Main _Overlord_main;
	bool pauseChecked = true, inGame = false;
	public int playerID = 0;
	[SerializeField]
	GameObject menu;
	[HideInInspector]
	public bool pausable = true, paused = false;
	void Awake () {
		if (this.gameObject != null) {
			DontDestroyOnLoad (this.gameObject);
			_Overlord_main = this;
		} else {
			Destroy (this.gameObject);
		}
	}
	void Start () {
		if (SceneManager.GetActiveScene ().name != "Loading" && SceneManager.GetActiveScene ().name != "MainMenu") {
			inGame = true;
		} else {
			inGame = false;
		}
	}
	void Update () {
		if (paused == true && inGame == false)
		{
			if (menu.activeSelf != true)
			{
				menu.SetActive(true);
			}
		}
		if (paused == false && inGame == false)
		{
			if (menu.activeSelf != false)
			{
				menu.SetActive(false);
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
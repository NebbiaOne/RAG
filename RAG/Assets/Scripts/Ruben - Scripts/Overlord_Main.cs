using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Overlord_Main : MonoBehaviour {
	public static Overlord_Main _Overlord_main;
	bool pauseChecked = true, isArena = false;
	public GameObject pauseMenu;
	[HideInInspector]
	public bool paused = false;
	void Awake () {
		if (this.gameObject != null) {
			DontDestroyOnLoad (this.gameObject);
			_Overlord_main = this;
		} else {
			Destroy (this.gameObject);
		}
	}
	void Start () {
		if (SceneManager.GetActiveScene ().name == "testArena") {
			isArena = true;
		} else {
			isArena = false;
		}
	}
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (paused == false && pauseChecked == true && isArena == true) {
				Debug.Log ("Paused");
				paused = true;
				pauseChecked = false;
				pauseMenu.SetActive (true);
				Invoke ("PauseChecker", 0.25f);
			}
			if (paused == true && pauseChecked == true && isArena == true) {
				Debug.Log ("Not Paused");
				paused = false;
				pauseChecked = false;
				pauseMenu.SetActive (false);
				Invoke ("PauseChecker", 0.25f);
			}
		}
	}
	void PauseChecker () {
		pauseChecked = true;
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Overlord_Main : MonoBehaviour {
	// Remove the play room checkers
	public static Overlord_Main _Overlord_main;
	Coloriser_Main _Color;
	GameObject overlord;
	EventSystem eventSystem;
	Text textPlayerVic;
	[SerializeField] //REMOVE
	bool pauseChecked = true, inGame = false, choice = false, victory = false;
	[SerializeField]
	GameObject menuGame, menuChoice, menuVictory;
	[HideInInspector]
	public int playersJoined = 0, playersReady = 0, playersAlive = 0;
	public int playerID = 0;
	public bool playAble = false, pausable = true, paused = false;
	public bool player_01 = false, player_02 = false, player_03 = false, player_04 = false;
	public bool inArena = false;
	[SerializeField] //REMOVE
	public bool player01Alive, player02Alive, player03Alive, player04Alive;
	void Awake () {
		overlord = this.gameObject;
		_Overlord_main = this;
	}
	void Start () {
		_Color = Coloriser_Main._Coloriser_Main;
		playersAlive = playersJoined;
		if (SceneManager.GetActiveScene ().name == "Arena_01" || SceneManager.GetActiveScene().name == "RubensPlayRoom") {
			inArena = true;
			textPlayerVic = menuVictory.transform.GetChild (0).GetChild (0).gameObject.GetComponent<Text> ();
		} else {
			inArena = false;
		}
		if (SceneManager.GetActiveScene ().name != "Loading" && SceneManager.GetActiveScene ().name != "MainMenuGame" && inGame != true) {
			inGame = true;
		} else if ((SceneManager.GetActiveScene ().name == "Loading" || SceneManager.GetActiveScene ().name == "MainMenuGame") && inGame != false) {
			inGame = false;
			if (SceneManager.GetActiveScene ().name == "Main_Menu") {
				GameObject.Find ("Canvas_MainMenu").transform.GetChild (1).GetComponent<Button> ().Select ();
				GameObject.Find ("Canvas_MainMenu").transform.GetChild (1).GetComponent<Button> ().OnSelect (null);
			}
		}
		if (eventSystem == null) {
			eventSystem = EventSystem.current;
		}
	}
	void Update () {
		// REMOVE
		if (Input.GetKeyDown (KeyCode.Keypad9)) {
			playersAlive = 1;
			Victory ();
		}
		// REMOVE

		if (playersAlive == 1) {
			MenuVictoryActive ();
			if (player01Alive == true) {
				textPlayerVic.text = "- " + "Player 1" + " -";
				textPlayerVic.color = _Color.col_Player01;
			}
			if (player02Alive == true) {
				textPlayerVic.text = "- " + "Player 2" + " -";
				textPlayerVic.color = _Color.col_Player02;
			}
			if (player03Alive == true) {
				textPlayerVic.text = "- " + "Player 3" + " -";
				textPlayerVic.color = _Color.col_Player03;
			}
			if (player04Alive == true) {
				textPlayerVic.text = "- " + "Player 4" + " -";
				textPlayerVic.color = _Color.col_Player04;
			}
		}
		if (SceneManager.GetActiveScene ().name != "Loading" && SceneManager.GetActiveScene ().name != "MainMenu" && inGame != true) {
			inGame = true;
		} else if ((SceneManager.GetActiveScene ().name == "Loading" || SceneManager.GetActiveScene ().name == "MainMenu") && inGame != false) {
			inGame = false;
		}
		if (SceneManager.GetActiveScene ().name == "Arena_01" || SceneManager.GetActiveScene ().name == "RubensPlayRoom" && playAble != true) {
			playAble = true;
		} else if (SceneManager.GetActiveScene ().name != "Arena_01" && SceneManager.GetActiveScene ().name != "RubensPlayRoom" && playAble != false) {
			playAble = false;

		}
		if (inGame == true && menuGame == null) {
			menuGame = GameObject.FindWithTag ("Menu_Game");
		}
		if (inGame == true && paused == true && choice == false) {
			if (menuGame.activeSelf != true) {
				MenuGameActive ();
			}
		}
		if (inGame == true && paused == false && victory == false) {
			if (menuGame.activeSelf != false) {
				menuGame.SetActive (false);
			}
			if (menuChoice.activeSelf != false) {
				choice = false;
				menuChoice.SetActive (false);
			}
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (paused == false && pauseChecked == true && inGame == true) {
				Debug.Log ("Paused");
				paused = true;
				pauseChecked = false;
				menuGame.SetActive (true);
				Invoke ("PauseChecker", 0.25f);
			}
			if (paused == true && pauseChecked == true && inGame == true) {
				Debug.Log ("Not Paused");
				paused = false;
				pauseChecked = false;
				menuGame.SetActive (false);
				Invoke ("PauseChecker", 0.25f);
			}
		}
	}
	void Victory () {
		MenuVictoryActive ();
	}
	void PauseChecker () {
		pauseChecked = true;
	}
	public void MenuVictoryActive () {
		if (inArena == true) {
			choice = false;
			victory = true;
			EventSystem.current.GetComponent<EventSystem> ().firstSelectedGameObject = menuVictory.transform.GetChild (0).GetChild (2).GetChild (0).gameObject;
			EventSystem.current.GetComponent<EventSystem> ().SetSelectedGameObject (menuVictory.transform.GetChild (0).GetChild (2).GetChild (0).gameObject);
			menuVictory.transform.GetChild (0).GetChild (2).GetChild (0).GetComponent<Button> ().Select ();
			menuVictory.transform.GetChild (0).GetChild (2).GetChild (0).GetComponent<Button> ().OnSelect (null);
			if (menuVictory.activeSelf == false) {
				menuVictory.SetActive (true);
			}
		}
	}
	public void MenuGameActive () {
		choice = false;
		EventSystem.current.GetComponent<EventSystem> ().firstSelectedGameObject = menuGame.transform.GetChild (1).gameObject;
		EventSystem.current.GetComponent<EventSystem> ().SetSelectedGameObject (menuGame.transform.GetChild (1).gameObject);
		menuGame.transform.GetChild (1).gameObject.GetComponent<Button> ().Select ();
		menuGame.transform.GetChild (1).gameObject.GetComponent<Button> ().OnSelect (null);
		if (menuGame.activeSelf == false) {
			menuGame.SetActive (true);
		}
		if (menuChoice.activeSelf == true) {
			menuChoice.SetActive (false);
		}
	}
	public void MenuChoiceActive () {
		choice = true;
		EventSystem.current.GetComponent<EventSystem> ().firstSelectedGameObject = menuChoice.transform.GetChild (1).gameObject;
		EventSystem.current.GetComponent<EventSystem> ().SetSelectedGameObject (menuChoice.transform.GetChild (1).gameObject);
		menuChoice.transform.GetChild (1).gameObject.GetComponent<Button> ().Select ();
		menuChoice.transform.GetChild (1).gameObject.GetComponent<Button> ().OnSelect (null);
		if (menuGame.activeSelf == true) {
			menuGame.SetActive (false);
		}
		if (menuChoice.activeSelf == false) {
			menuChoice.SetActive (true);
		}
	}
	public void MenuChoiceInactive () {
		menuChoice.SetActive (false);
		if (victory == true) {
			MenuVictoryActive ();
		}
	}
}
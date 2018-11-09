using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Overlord_Main : MonoBehaviour {
	public static Overlord_Main _Overlord_main;
	GameObject overlord;
	EventSystem eventSystem;
	Text textPlayerVictory;
	bool pauseChecked = true, inGame = false, choice = false, victory = false;
	[SerializeField]
	GameObject menuGame, menuChoice, menuVictory;
	[HideInInspector]
	public int playersJoined = 0, playersReady = 0, playersAlive = 2;
	public int playerID = 0;
	public bool playAble = false, pausable = true, paused = false;
	public bool player_01 = false, player_02 = false, player_03 = false, player_04 = false;
	public bool player01Alive = false, player02Alive = false, player03Alive = false, player04alive = false;
	void Awake () {
		overlord = this.gameObject;
		_Overlord_main = this;
	}
	void Start () {
		victory = false;
		playersAlive = playersJoined;
		textPlayerVictory = menuVictory.transform.GetChild (0).GetChild (0).GetComponent<Text> ();
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
		if (Input.GetKeyDown (KeyCode.Keypad8)) {
			playersAlive += 1;
		}
		if (Input.GetKeyDown (KeyCode.Keypad7)) {
			playersAlive -= 1;
		}

		if (SceneManager.GetActiveScene ().name != "Loading" && SceneManager.GetActiveScene ().name != "MainMenu" && inGame != true) {
			inGame = true;
		} else if ((SceneManager.GetActiveScene ().name == "Loading" || SceneManager.GetActiveScene ().name == "MainMenu") && inGame != false) {
			inGame = false;
		}
		if (SceneManager.GetActiveScene ().name == "Arena_01" && playAble != true || SceneManager.GetActiveScene ().name == "RubensPlayRoom" && playAble != true) {
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
				menuChoice.SetActive (false);
				choice = false;
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
		if (playersAlive <= 1 && victory == false) {
			MenuVictoryActive ();
		}
	}
	void PauseChecker () {
		pauseChecked = true;
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
		Debug.Log ("Choice");
		choice = true;
		if (menuChoice.activeSelf == false) {
			menuChoice.SetActive (true);
		}
		EventSystem.current.GetComponent<EventSystem> ().firstSelectedGameObject = menuChoice.transform.GetChild (1).gameObject;
		EventSystem.current.GetComponent<EventSystem> ().SetSelectedGameObject (menuChoice.transform.GetChild (1).gameObject);
		menuChoice.transform.GetChild (1).gameObject.GetComponent<Button> ().Select ();
		menuChoice.transform.GetChild (1).gameObject.GetComponent<Button> ().OnSelect (null);

	}
	public void MenuChoiceInactive () {
		choice = false;
		if (menuChoice.activeSelf == true) {
			menuChoice.SetActive (false);
		}
		if (victory == true)
		{
			MenuVictoryActive();
		}
	}
	public void MenuVictoryActive () {
		victory = true;
		choice = false;
		if (menuVictory.activeSelf == false) {
			menuVictory.SetActive (true);
		}
		EventSystem.current.GetComponent<EventSystem> ().firstSelectedGameObject = menuVictory.transform.GetChild (0).GetChild (2).GetChild (0).gameObject;
		EventSystem.current.GetComponent<EventSystem> ().SetSelectedGameObject (menuVictory.transform.GetChild (0).GetChild (2).GetChild (0).gameObject);
		menuVictory.transform.GetChild (0).GetChild (2).GetChild (0).gameObject.GetComponent<Button> ().Select ();
		menuVictory.transform.GetChild (0).GetChild (2).GetChild (0).gameObject.GetComponent<Button> ().OnSelect (null);
		if (player01Alive == true) {
			textPlayerVictory.text = "Player 1";
		}
		if (player02Alive == true) {
			textPlayerVictory.text = "Player 2";
		}
		if (player03Alive == true) {
			textPlayerVictory.text = "Player 3";
		}
		if (player04alive == true) {
			textPlayerVictory.text = "Player 4";
		}
		//paused = true;
	}
}
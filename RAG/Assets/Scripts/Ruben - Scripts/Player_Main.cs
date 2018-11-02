using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player_Main : MonoBehaviour {
	public static Player_Main _Player_Main;
	Overlord_Main _Overlord;
	Overlord_Ghost _Ghost;
	Coloriser_Main _Color;
	MultipleTargetCamera _Camera;
	TextMesh healthText;
	RaycastHit hit;
	Rigidbody rbPlayer;
	Image healthBar, heartBeat;
	Image life_01, life_02, life_03;
	[SerializeField]
	GameObject preciseSensor;
	public Player_Main _MainEnemy;
	public GameObject target, hud;
	public int playerLives = 3;
	public float playerHealth = 100f;
	public bool isTargeted = false, blocking = false;
	public bool sensor_01 = false, sensor_02 = false;
	public ParticleSystem BloodParticle;
	void Awake () {
		_Player_Main = this;
	}
	void Start () {
		_Overlord = Overlord_Main._Overlord_main;
		_Ghost = Overlord_Ghost._Overlord_Ghost;
		_Camera = MultipleTargetCamera._MultipleTargetCamera;
		_Color = Coloriser_Main._Coloriser_Main;
		rbPlayer = transform.GetComponent<Rigidbody> ();
		healthText = gameObject.transform.GetChild (2).gameObject.GetComponent<TextMesh> ();
		healthBar = hud.transform.GetChild (0).gameObject.GetComponent<Image> ();
		heartBeat = hud.transform.GetChild (1).gameObject.GetComponent<Image> ();
		if (gameObject.tag == "Player_01") {
			if (_Ghost.player_01 == false) {
				this.gameObject.SetActive (false);
				if (hud != null) {
					hud.SetActive (false);
				}
			}
			if (hud == null) {
				hud = GameObject.Find ("HUD_Player_01");
			}
		}
		if (gameObject.tag == "Player_02") {
			if (_Ghost.player_02 == false) {
				this.gameObject.SetActive (false);
				if (hud != null) {
					hud.SetActive (false);
				}
			}
			if (hud == null) {
				hud = GameObject.Find ("HUD_Player_02");
			}
		}
		if (gameObject.tag == "Player_03") {
			if (_Ghost.player_03 == false) {
				this.gameObject.SetActive (false);
				if (hud != null) {
					hud.SetActive (false);
				}
			}
			if (hud == null) {
				hud = GameObject.Find ("HUD_Player_03");
			}
		}
		if (gameObject.tag == "Player_04") {
			if (_Ghost.player_04 == false) {
				this.gameObject.SetActive (false);
				if (hud != null) {
					hud.SetActive (false);
				}
			}
			if (hud == null) {
				hud = GameObject.Find ("HUD_Player_04");
			}
		}

	}
	void Update () {
		if (Input.GetKeyDown (KeyCode.Keypad9)) {
			playerHealth -= 5f;
		}
		if (_Overlord.playAble == true) {
			if (life_01 == null || life_02 == null || life_03 == null) {
				life_01 = hud.transform.GetChild (2).gameObject.GetComponent<Image> ();
				life_02 = hud.transform.GetChild (3).gameObject.GetComponent<Image> ();
				life_03 = hud.transform.GetChild (4).gameObject.GetComponent<Image> ();
			}
			switch (playerLives) {
				case 0:
					life_01.enabled = false;
					life_02.enabled = false;
					life_03.enabled = false;
					break;
				case 1:
					life_01.enabled = true;
					life_02.enabled = false;
					life_03.enabled = false;
					break;
				case 2:
					life_01.enabled = true;
					life_02.enabled = true;
					life_03.enabled = false;
					break;
				case 3:
					life_01.enabled = true;
					life_02.enabled = true;
					life_03.enabled = true;
					break;

			}
			if (rbPlayer.isKinematic != false) {
				rbPlayer.isKinematic = false;
			}
			if (healthText.text != playerHealth.ToString ()) {
				healthText.text = playerHealth.ToString ();
			}
			if (playerHealth <= 0) {
				playerHealth = 0f;
			}
			if (healthBar.fillAmount != playerHealth / 100) {
				healthBar.fillAmount = playerHealth / 100;
				if (playerHealth >= 75 && healthBar.color != _Color.col_Green) {
					healthBar.color = _Color.col_Green;
				}
				if (playerHealth <= 75 && playerHealth >= 25 && healthBar.color != _Color.col_Yellow) {
					healthBar.color = _Color.col_Yellow;
				}
				if (playerHealth <= 25 && healthBar.color != _Color.col_Red) {
					healthBar.color = _Color.col_Red;
				}
				if (playerHealth <= 0 && heartBeat.IsActive () == false) {
					heartBeat.gameObject.SetActive (true);
				} else if (playerHealth >= 0 && heartBeat.IsActive () == true) {
					heartBeat.gameObject.SetActive (false);
				}
			}
			if (playerLives == 0) {
				gameObject.SetActive (false);
				hud.SetActive (false);
			}
			if (target != null && _MainEnemy != target.transform.GetComponent<Player_Main> ()) {
				_MainEnemy = target.transform.GetComponent<Player_Main> ();
				_MainEnemy.isTargeted = true;
			}
		} else if (_Overlord.playAble == false && rbPlayer.isKinematic != true) {
			rbPlayer.isKinematic = true;
		}
	}
	public void Retarget () {
		_MainEnemy = target.transform.GetComponent<Player_Main> ();
	}
	public void DamagePlayer () {
		_MainEnemy.playerHealth -= 5f;
		//BloodParticle.Emit(50);
		_MainEnemy.BloodParticle.Emit (50);
	}
}
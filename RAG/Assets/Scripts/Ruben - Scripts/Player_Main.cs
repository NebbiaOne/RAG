using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player_Main : MonoBehaviour {
	MultipleTargetCamera _Camera;
	Player_Main _MainEnemy;
	TextMesh healthText;
	RaycastHit hit;
	public static Player_Main _Player_Main;
	public bool blocking = false;
	[SerializeField]
	GameObject preciseSensor;
	public float playerHealth = 100f;
	public bool isTargeted = false;
	[HideInInspector]
	public bool sensor_01 = false, sensor_02 = false;
	public GameObject target;
	void Awake () {
		_Player_Main = this;
		_Camera = MultipleTargetCamera._MultipleTargetCamera;
	}
	void Start () {
		healthText = gameObject.transform.GetChild (2).gameObject.GetComponent<TextMesh> ();
	}
	void Update () {
		if (healthText.text != playerHealth.ToString ()) {
			healthText.text = playerHealth.ToString ();
		}
		if (playerHealth <= 0f) {
			gameObject.SetActive (false);
		}
		if (target != null && _MainEnemy != target.transform.GetComponent<Player_Main>())
		{
			_MainEnemy = target.transform.GetComponent<Player_Main>();
			_MainEnemy.isTargeted = true;
		}
	}
	public void Retarget () {
		_MainEnemy = target.transform.GetComponent<Player_Main> ();
	}
	public void DamagePlayer () {
		_MainEnemy.playerHealth -= 5f;
	}
}
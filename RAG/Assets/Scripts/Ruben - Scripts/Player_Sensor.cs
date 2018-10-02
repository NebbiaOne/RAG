using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player_Sensor : MonoBehaviour {
	Player_Main _Main;
	void Start () {
		_Main = gameObject.transform.parent.GetComponent<Player_Main>();
	}
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.layer == 9 && this.gameObject.tag == "Sensor_01") {
			//other.gameObject.transform.GetComponent<Renderer> ().material.color = Color.yellow;
			_Main.sensor_01 = true;
			if (_Main.target == null) {
				_Main.target = other.gameObject;
				_Main.Retarget ();
			}
		}
		if (other.gameObject.layer == 9 && this.gameObject.tag == "Sensor_02") {
			//other.gameObject.transform.GetComponent<Renderer> ().material.color = Color.red;
			_Main.sensor_02 = true;
			if (_Main.target != other.gameObject) {
				_Main.target = other.gameObject;
			}
		}
	}
	void OnTriggerStay (Collider other) {
		if (other.gameObject.layer == 9 && this.gameObject.tag == "Sensor_01" && _Main.sensor_02 == false) {
			if (_Main.target != other.gameObject) {
				_Main.target = other.gameObject;
			}
		}
	}
	void OnTriggerExit (Collider other) {
		if (other.gameObject.layer == 9 && this.gameObject.tag == "Sensor_01") {
			//other.gameObject.transform.GetComponent<Renderer> ().material.color = Color.white;
			_Main.sensor_01 = false;
			if (_Main.target == other.gameObject) {
				_Main.target = null;
			}

		}
		if (other.gameObject.layer == 9 && this.gameObject.tag == "Sensor_02") {
			//other.gameObject.transform.GetComponent<Renderer> ().material.color = Color.yellow;
			_Main.sensor_02 = false;
			if (_Main.target == other.gameObject) {
				_Main.target = null;
			}
		}
	}
}
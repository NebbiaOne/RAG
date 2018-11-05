using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Coloriser_Main : MonoBehaviour {
	public static Coloriser_Main _Coloriser_Main;
	[SerializeField]
	public Color col_Player01, col_Player02, col_Player03, col_Player04;
	void Awake() {
		_Coloriser_Main = this;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Coloriser_Main : MonoBehaviour {
	public static Coloriser_Main _Coloriser_Main;
	public Color col_Green, col_Yellow, col_Red;
	void Awake () {
		_Coloriser_Main = this;
	}
}

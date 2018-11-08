using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Overlord_Colorizer : MonoBehaviour {
	public static Overlord_Colorizer _Overlord_Colorizer;
	public Color col_Green, col_Yellow, col_Red;
	void Awake () {
		Overlord_Colorizer._Overlord_Colorizer = this;
	}
}

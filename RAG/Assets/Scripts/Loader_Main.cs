using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Loader_Main : MonoBehaviour {
	public static Loader_Main _Loader_Main;
	Overlord_Main _Overlord;
	AsyncOperation asyncLoad;
	string nextScene;
	bool enumerating = false;
	void Awake () {
		_Loader_Main = this;
	}
	void Start () {
		_Overlord = Overlord_Main._Overlord_main;
		nextScene = "MainMenu";
	}
	void Update () {
		if (SceneManager.GetActiveScene ().name == "Loading" && enumerating == false && nextScene != null) {
			StartCoroutine (LoadLevel());
			enumerating = true;
		}
	}
	IEnumerator LoadLevel () {
		asyncLoad = SceneManager.LoadSceneAsync(nextScene);
		while (!asyncLoad.isDone) {
			if (asyncLoad.allowSceneActivation != false) {
				asyncLoad.allowSceneActivation = false;
			}
			if (asyncLoad.progress >= 0.9f) {
				yield return new WaitForSeconds (1f);
				asyncLoad.allowSceneActivation = true;
				enumerating = false;
				nextScene = null;
			}
			yield return null;
		}
	}
	public void Load_MainMenu () {
		nextScene = "MainMenu";
		SceneManager.LoadScene ("Loading");
	}
	public void Load_TempArena () {
		nextScene = "testArena";
		SceneManager.LoadScene ("Loading");
	}
	public void Load_Quit () {
		Application.Quit ();
	}
}
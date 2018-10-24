using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Loader_Main : MonoBehaviour {
	public static Loader_Main _Loader_Main;
	Overlord_Main _Overlord;
	AsyncOperation asyncLoad;
	bool enumerating = false;
	public string nextScene;
	void Awake () {
		_Loader_Main = this;
	}
	void Start () {
		_Overlord = Overlord_Main._Overlord_main;
	}
	void Update () {
		if (SceneManager.GetActiveScene ().name == "Loading" && enumerating == false && nextScene != null) {
			StartCoroutine (LoadLevel ());
			enumerating = true;
		}
	}
	IEnumerator LoadLevel () {
		if (nextScene != null && nextScene != "") {
			asyncLoad = SceneManager.LoadSceneAsync (nextScene);
			while (!asyncLoad.isDone) {
				if (asyncLoad.allowSceneActivation != false) {
					asyncLoad.allowSceneActivation = false;
				}
				if (asyncLoad.progress >= 0.9f) {
					yield return new WaitForSeconds (1f);
					enumerating = false;
					nextScene = null;
					Destroy (this.gameObject);
					asyncLoad.allowSceneActivation = true;
				}
				yield return null;
			}
		}
	}
	public void Initiate_Load () {
		DontDestroyOnLoad (this.gameObject);
		SceneManager.LoadScene ("Loading");
	}
	public void Load_MainMenu () {
		DontDestroyOnLoad (this.gameObject);
		nextScene = "MainMenu";
	}
	public void Load_CharacterSelection () {
		DontDestroyOnLoad (this.gameObject);
		nextScene = "CharacterSelection";
	}
	public void Load_Arena () {
		DontDestroyOnLoad (this.gameObject);
		nextScene = "Arena_01";
	}
	public void Load_Quit () {
		Application.Quit ();
	}
}
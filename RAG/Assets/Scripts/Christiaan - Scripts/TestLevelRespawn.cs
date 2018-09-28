using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevelRespawn : MonoBehaviour {

	public GameObject CurrentPlayer;
	public GameObject SpawnPoint;
	public GameObject WaterCollider;


 	private void OnCollisionEnter(Collision collision) 
	 {
		if (collision.gameObject.tag == "Player_01" || collision.gameObject.tag == "Player_02" || collision.gameObject.tag == "Player_03" || collision.gameObject.tag == "Player_04")
		{
			CurrentPlayer = collision.gameObject;
			//CurrentPlayer.transform.position = WaterCollider.transform.position;
			CurrentPlayer.transform.position = new Vector3(0.0f, 20.0f,0.0f);
		}	 


	 }
	 

}

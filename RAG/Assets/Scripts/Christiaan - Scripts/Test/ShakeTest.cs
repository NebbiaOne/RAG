using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeTest : MonoBehaviour {

    float speed =5.0f; //how fast it shakes
    float amount = 4.0f; //how much it shakes

	public GameObject ThisObject;
    void Start () {
		
	}
	
	void Update ()
	{
		
     ThisObject.transform.position =  new Vector3(ThisObject.transform.position.x,Mathf.Sin(Time.time * speed) * amount,ThisObject.transform.position.z) ;
	 
	 
	  	   
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlatformHeight : MonoBehaviour {

private int Position = 0;
	// Use this for initialization
	void Start () 
	{
		StartCoroutine(SetPosition());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator SetPosition()
	{
        yield return new WaitForSeconds(0.5f);
        Position = Random.Range(0, 3);
       //Debug.Log(Position);
        transform.Translate(0, Position, 0, Space.Self);
		//MoveNow();
        
	}

	// void MoveNow()
	// {
	// 	transform.Translate(0,Position*Time.deltaTime,0,Space.Self);	
	// }
}

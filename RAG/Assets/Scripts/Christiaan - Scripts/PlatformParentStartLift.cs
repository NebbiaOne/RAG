using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParentStartLift : MonoBehaviour {

	public GameObject PlatformL1;
	public GameObject PlatformL2;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       // PlatformL1.transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 2.85f, 0), Time.deltaTime * 5);
        //PlatformL2.transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), Time.deltaTime * 5);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), Time.deltaTime * 5);
	
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDropper : MonoBehaviour {

    public List<GameObject> platforms;
	
	private GameObject CurrentPlatform;
	private GameObject TempPlatform;
    void Start()
    {
		StartCoroutine(StartDrop());
       
    }
	IEnumerator StartDrop()
	{
        yield return new WaitForSeconds(15);
        if (platforms != null)
        {
            List<GameObject> spawnList = new List<GameObject>(platforms);

            while (spawnList.Count > 0)
            {
                int randomIndex = Random.Range(0, spawnList.Count);

                if (spawnList[randomIndex] != null)
                {
                    
                    CurrentPlatform = (spawnList[randomIndex]);
                    Debug.Log("Created: " + CurrentPlatform.name + " " + CurrentPlatform.GetInstanceID());
					TempPlatform=CurrentPlatform;
                     //CurrentPlatform.transform.position += Vector3.up * Time.deltaTime;

                    // CurrentPlatform.transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, -10, 0), Time.deltaTime * 5);
                     //CurrentPlatform.transform.Translate(CurrentPlatform.transform.position.Vector3.up * -Time.deltaTime, Space.World);
                   //CurrentPlatform.transform.position = Vector3.MoveTowards(CurrentPlatform.transform.position,new Vector3(CurrentPlatform.transform.position.x,-50,CurrentPlatform.transform.position.z),1);
				  	StartCoroutine(Drop());
                }

                spawnList.RemoveAt(randomIndex);
                yield return new WaitForSeconds(5);
            }
        }
	}
	IEnumerator Drop()
	{
        //CurrentPlatform.transform.Translate(CurrentPlatform.transform.up * -Time.deltaTime, Space.World);
        TempPlatform.transform.position = new Vector3(0,-20,0);
        yield return null;
	}
	
}


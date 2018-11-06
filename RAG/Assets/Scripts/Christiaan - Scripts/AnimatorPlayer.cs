using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class AnimatorPlayer : MonoBehaviour {

    Player rwInput;
	Animator PlayerAnimator;
    [SerializeField] GameObject AnimatorObject;
	

	// Use this for initialization
	void Start () {
        if (gameObject.tag == "Player_01")
        {
            rwInput = ReInput.players.GetPlayer(0);
        }
        if (gameObject.tag == "Player_02")
        {
            rwInput = ReInput.players.GetPlayer(1);
        }
        if (gameObject.tag == "Player_03")
        {
            rwInput = ReInput.players.GetPlayer(2);
        }
        if (gameObject.tag == "Player_04")
        {
            rwInput = ReInput.players.GetPlayer(3);
        }
        PlayerAnimator = AnimatorObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (rwInput.GetButtonDown("Attack"))
        {
            StartCoroutine(AttackAnimate());
           // Debug.Log("ANIMATINE");
        }
	}

	IEnumerator AttackAnimate()
		{
        Debug.Log("ANIMATINE");
        PlayerAnimator.SetLayerWeight(1,1);
        PlayerAnimator.SetTrigger("AttackTrigger");
        yield return new WaitForSeconds(0.25f);
        PlayerAnimator.SetLayerWeight(1, 0);
		Debug.Log("ANIMATINE");
		}
}

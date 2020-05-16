using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chest : MonoBehaviour {
	
	private ChestState state
	{
		get 
		{ 
			return (ChestState)animator.GetInteger("state");
		}
		set { animator.SetInteger("state",(int) value);}
	}
	new private Rigidbody2D rigidbody;
	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}
	private void Update () 

	{

	}


	void OnTriggerEnter2D(Collider2D Finish)
	{ 
		if (Finish.tag == "Player")
			state = ChestState.finish;	
		else state = ChestState.Chest1;

	}

}

public enum ChestState
{
	Chest1,
	finish
}


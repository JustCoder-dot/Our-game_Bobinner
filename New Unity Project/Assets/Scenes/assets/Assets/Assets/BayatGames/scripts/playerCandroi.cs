using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerCandroi : MonoBehaviour {
	Animator anim;
	Rigidbody2D rb;
	public GameObject btnLeft;
	public GameObject btnRight;
	float PosBtnLeft;
	float PosBtnRight;
	float run;
	void Start(){
		PosBtnLeft = btnLeft.transform.position.y;
		PosBtnRight = btnRight.transform.position.y;
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		}
	void Update () {
		if (PosBtnLeft != btnLeft.transform.position.y){
			Debug.Log("niggas");
			//run = -5f;
			} 
		else if (PosBtnRight != btnRight.transform.position.y){
			Debug.Log("wiggas");
			//run = 5f;
		    } 
		else 
		{
			Debug.Log("none");
			//run = 0f;
		}
		//rb.velocity = new Vector2 (run, rb.velocity.y);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		SceneManager.LoadScene("2");
	}

}
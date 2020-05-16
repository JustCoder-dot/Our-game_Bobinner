using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Saw : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D Player)
	{
		if (Player.tag == "Respawn") {
			SceneManager.LoadScene("level2");
		}
	}
}

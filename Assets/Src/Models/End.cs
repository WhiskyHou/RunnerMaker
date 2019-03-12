using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour {

	void Start() {
		
    }

    void Update() {
        
    }

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Player") {
			Debug.Log("===== Event =====\nplayer arrive end");
		}
	}
}

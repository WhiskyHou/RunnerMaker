using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

    void Start() {
		
    }

    void Update() {

    }

	void OnTriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.tag == "Player") {
			Destroy(gameObject);
		}
	}
}

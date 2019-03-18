using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour {

	public float dropTime;

	private bool isDroping = false;

	private float dropDelta = 0f;

    void Start() {
        
    }

    void Update() {
        if (isDroping) {
			dropDelta += Time.deltaTime;
			if (dropDelta > dropTime) {
				Destroy(gameObject);
			}
		}
    }

	void OnCollisionEnter2D(Collision2D collision) {
		if(collision.gameObject.tag == "Player") {
			isDroping = true;
		}
	}
}

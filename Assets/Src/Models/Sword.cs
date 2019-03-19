using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

	public float rotateSpeed;

    void Start() {
        
    }

    void Update() {
		float delta = Time.deltaTime;
		transform.Rotate(new Vector3(0f, 0f, delta * rotateSpeed));
    }

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player")
			Debug.Log("=== Sword trigger enter ===");
	}
}

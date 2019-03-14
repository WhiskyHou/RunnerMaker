using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour {

	public int force;

    void Start() {
        
    }

    void Update() {
        
    }

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player") {
			Rigidbody2D rigidbody = collision.GetComponent<Rigidbody2D>();
			rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);
			rigidbody.AddForce(new Vector2(0f, force));
		}
	}
}

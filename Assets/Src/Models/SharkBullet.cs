using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBullet : MonoBehaviour {

	public bool isLeft;

	public float moveSpeed;

    void Start() {
		if (!isLeft) {
			gameObject.transform.Find("cannon_bullet").GetComponent<SpriteRenderer>().flipX = true;
		}
    }

	void Update() {
		float delta = Time.deltaTime;
		transform.Translate((isLeft ? Vector3.left : Vector3.right) * moveSpeed * delta);
    }

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.tag == "Player") {
			// player die
		}
		//Destroy(gameObject);
	}
}

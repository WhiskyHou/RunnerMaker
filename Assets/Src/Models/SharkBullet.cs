using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBullet : MonoBehaviour {

	public bool isLeft;

	public float moveSpeed;

    void Start() {
		
	}

	void Update() {
		float delta = Time.deltaTime;
		transform.Translate((isLeft ? Vector3.left : Vector3.right) * moveSpeed * delta);
    }

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player") {
			// ... player die
			Remove();
		} else if (collision.gameObject.tag != "Cannon" || collision.gameObject.tag != "SharkBullet") {
			Remove();
		}
	}

	public void Init(Transform transform, bool isLeft) {
		gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		this.isLeft = isLeft;
		if (!isLeft) {
			gameObject.transform.localScale = new Vector3(-1, 1, 1);
		} else {
			gameObject.transform.localScale = new Vector3(1, 1, 1);
		}
	}

	public void Remove() {
		transform.parent.gameObject.GetComponent<Cannon>().pool.Add(gameObject);
	}
}

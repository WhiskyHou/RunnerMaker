using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCameraController : MonoBehaviour {

	public float moveSpeed;

    void Start() {
        
    }

    void Update() {
		if (Input.GetKey(KeyCode.A)) {
			Move(Vector3.left);
		} else if (Input.GetKey(KeyCode.D)) {
			Move(Vector3.right);
		}

		if (Input.GetKey(KeyCode.W)) {
			Move(Vector3.up);
		} else if (Input.GetKey(KeyCode.S)) {
			Move(Vector3.down);
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			ResetPos();
		}
    }

	private void Move(Vector3 dir) {
		transform.Translate(dir * Time.deltaTime * moveSpeed);
	}

	private void ResetPos() {
		transform.position = new Vector3(0f, 0f, -10f);
	}
}

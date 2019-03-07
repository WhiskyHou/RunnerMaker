using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;

	public float jumpForce;

	public Rigidbody2D rigidbody;

	public DragonBones.UnityArmatureComponent ua;

    void Start() {
        
    }

    void Update() {
		float deltaTime = Time.deltaTime;
        if (Input.GetKey(KeyCode.A)) {
			//transform.Translate(Vector3.left * moveSpeed * deltaTime);
			//rigidbody.MovePosition(transform.position + Vector3.left * moveSpeed * deltaTime);
			rigidbody.velocity = new Vector2(-moveSpeed, rigidbody.velocity.y);
		} else if (Input.GetKey(KeyCode.D)) {
			//transform.Translate(Vector3.right * moveSpeed * deltaTime);
			//rigidbody.MovePosition(transform.position + Vector3.right * moveSpeed * deltaTime);
			rigidbody.velocity = new Vector2(moveSpeed, rigidbody.velocity.y);
		}


		if (Input.GetKeyDown(KeyCode.Space)) {
			rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);
			rigidbody.AddForce(new Vector2(0f, jumpForce));
		}

		if (Input.GetKeyDown(KeyCode.A)) {
			ua.armature.flipX = false;
			ua.animation.Play("boy-move");
		} else if (Input.GetKeyDown(KeyCode.D)) {
			ua.armature.flipX = true;
			ua.animation.Play("boy-move");
		}

		if (Input.GetKeyUp(KeyCode.A)) {
			ua.animation.Stop("boy-move");
			rigidbody.velocity.Set(0f, rigidbody.velocity.y);
		} else if (Input.GetKeyUp(KeyCode.D)) {
			ua.animation.Stop("boy-move");
			rigidbody.velocity.Set(0f, rigidbody.velocity.y);
		}
	}
}

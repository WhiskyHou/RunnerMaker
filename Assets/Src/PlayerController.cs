using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

enum MovingState {
	stand,
	left,
	right,
}

public class PlayerController : MonoBehaviour {

	public float moveSpeed;

	public float jumpForce;

	public Rigidbody2D rigidbody;

	public UnityArmatureComponent ua;

	private MovingState movingState = MovingState.stand;

	private bool isAirring = false;

	private bool goJump = false;

    void Start() {
        
    }

	void FixedUpdate() {
		if (movingState == MovingState.left) {
			rigidbody.velocity = new Vector2(-moveSpeed, rigidbody.velocity.y);
		} else if (movingState == MovingState.right) {
			rigidbody.velocity = new Vector2(moveSpeed, rigidbody.velocity.y);
		}

		if (!isAirring && goJump) {
			rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);
			rigidbody.AddForce(new Vector2(0f, jumpForce));
			isAirring = true;
			goJump = false;
		}
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.A)) {
			movingState = MovingState.left;
			ua.armature.flipX = false;
			ua.animation.Play("boy-move");
		} else if (Input.GetKeyDown(KeyCode.D)) {
			movingState = MovingState.right;
			ua.armature.flipX = true;
			ua.animation.Play("boy-move");
		}

		if (Input.GetKeyDown(KeyCode.Space) && !isAirring) {
			goJump = true;
		}

		if (Input.GetKeyUp(KeyCode.A)) {
			if(movingState == MovingState.left) {
				movingState = MovingState.stand;
				ua.animation.Stop("boy-move");
				ua.animation.Reset();
			}
		} else if (Input.GetKeyUp(KeyCode.D)) {
			if(movingState == MovingState.right) {
				movingState = MovingState.stand;
				ua.animation.Stop("boy-move");
				ua.animation.Reset();
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		Vector3 temp = transform.position - collision.gameObject.transform.position;
		bool isOnTarget = temp.y > Math.Abs(temp.x);
		if(collision.gameObject.tag == "Stone" && isOnTarget) {
			isAirring = false;
		}
	}
}

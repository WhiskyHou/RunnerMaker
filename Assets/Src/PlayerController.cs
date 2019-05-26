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

	public float horizontalBufferNum;

	public AudioSource audioSource;

	private MovingState movingState = MovingState.stand;

	private bool isAirring = false;

	private bool goJump = false;

    void Start() {
		GameCameraController gameCamera = GameObject.Find("GameCamera").GetComponent<GameCameraController>();
		gameCamera.SetPlayer(this.gameObject);
    }

	void FixedUpdate() {
		if (movingState == MovingState.left) {
			rigidbody.velocity = new Vector2(-moveSpeed, rigidbody.velocity.y);
		} else if (movingState == MovingState.right) {
			rigidbody.velocity = new Vector2(moveSpeed, rigidbody.velocity.y);
		} else if (movingState == MovingState.stand) {
			rigidbody.velocity = new Vector2(rigidbody.velocity.x * horizontalBufferNum, rigidbody.velocity.y);
		}

		if (!isAirring && goJump) {
			audioSource.Play();
			rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);
			rigidbody.AddForce(new Vector2(0f, jumpForce));
			isAirring = true;
			goJump = false;
		}
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
			movingState = MovingState.left;
			ua.armature.flipX = false;
			ua.animation.Play("boy-move");
		} else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
			movingState = MovingState.right;
			ua.armature.flipX = true;
			ua.animation.Play("boy-move");
		}

		if (Input.GetKeyDown(KeyCode.Space) && !isAirring) {
			goJump = true;
		}

		if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) {
			if(movingState == MovingState.left) {
				movingState = MovingState.stand;
				ua.animation.Stop("boy-move");
				ua.animation.Reset();
			}
		} else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) {
			if(movingState == MovingState.right) {
				movingState = MovingState.stand;
				ua.animation.Stop("boy-move");
				ua.animation.Reset();
			}
		}

		RaycastHit2D hit;
		Vector3 down = new Vector3(0f, -0.6f);
		Debug.DrawLine(transform.position, transform.position + down);
		hit = Physics2D.Raycast(transform.position + down, Vector2.up, 0.1f);
		if (hit.collider == null || hit.collider.name == "Player") {
			isAirring = true;
		} else {
			isAirring = false;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraController : MonoBehaviour {

	public GameObject player;

    void Start() {
        if (!player) {
			transform.position = new Vector3(0f, 0f, -10f);
		}
    }

    void Update() {
        if (player) {
			transform.position = player.transform.position + Vector3.back * 10;
			// transform.Translate(new Vector3(0f, 0f, -10f));
		}
    }

	public void SetPlayer(GameObject player) {
		this.player = player;
		transform.position = player.transform.position + Vector3.up * 2;
		transform.Translate(new Vector3(0f, 0f, -10f));
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour {

	public GameObject stone;

	public const float logInterval = 1f;

	private float logDelta = 0f;

    void Start() {
        
    }

    void Update() {
		logDelta += Time.deltaTime;

		Vector3 mousePosOnScene = Input.mousePosition;
		Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(mousePosOnScene);

		if (logDelta > logInterval) {
			Debug.Log(mousePosInWorld);
			logDelta = 0f;
		}
		
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			GameObject go = Instantiate(stone);
			go.transform.position = new Vector3((int)Math.Round(mousePosInWorld.x), (int)Math.Round(mousePosInWorld.y), 0f);
		}
    }
}

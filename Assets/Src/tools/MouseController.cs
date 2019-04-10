using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseController : MonoBehaviour {

	public EditorManager manager;

	void Start () {

	}

	void Update () {

		Vector3 mousePosOnScene = Input.mousePosition;
		Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint (mousePosOnScene);

		// TODO 这里只是临时处理了一下 点击在button上的操作不会被捕获
		if (Input.GetKeyDown (KeyCode.Mouse0) && Screen.height - 120 > Input.mousePosition.y) {
			manager.ClickMouse ((int) Math.Round (mousePosInWorld.x), (int) Math.Round (mousePosInWorld.y), true);
		} else if (Input.GetKeyDown (KeyCode.Mouse1)) {
			manager.ClickMouse ((int) Math.Round (mousePosInWorld.x), (int) Math.Round (mousePosInWorld.y), false);
		}

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			manager.ClickNumber (1);
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			manager.ClickNumber (2);
		} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			manager.ClickNumber (3);
		} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
			manager.ClickNumber (4);
		} else if (Input.GetKeyDown (KeyCode.Alpha5)) {
			manager.ClickNumber (5);
		} else if (Input.GetKeyDown (KeyCode.Alpha6)) {
			manager.ClickNumber (6);
		} else if (Input.GetKeyDown (KeyCode.Alpha7)) {
			manager.ClickNumber (7);
		} else if (Input.GetKeyDown (KeyCode.Alpha8)) {
			manager.ClickNumber (8);
		} else if (Input.GetKeyDown (KeyCode.Alpha9)) {
			manager.ClickNumber (9);
		} else if (Input.GetKeyDown (KeyCode.Alpha0)) {
			manager.ClickNumber (0);
		}
	}
}
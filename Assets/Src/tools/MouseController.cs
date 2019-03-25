using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

	public EditorManager manager;

    void Start() {

    }

    void Update() {

		Vector3 mousePosOnScene = Input.mousePosition;
		Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(mousePosOnScene);
		
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			manager.ClickMouse((int)Math.Round(mousePosInWorld.x), (int)Math.Round(mousePosInWorld.y), true);
		} else if (Input.GetKeyDown(KeyCode.Mouse1)) {
			manager.ClickMouse((int)Math.Round(mousePosInWorld.x), (int)Math.Round(mousePosInWorld.y), false);
		}

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			manager.ClickNumber(1);
		} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
			manager.ClickNumber(2);
		} else if (Input.GetKeyDown(KeyCode.Alpha3)) {
			manager.ClickNumber(3);
		} else if (Input.GetKeyDown(KeyCode.Alpha4)) {
			manager.ClickNumber(4);
		} else if (Input.GetKeyDown(KeyCode.Alpha5)) {
			manager.ClickNumber(5);
		} else if (Input.GetKeyDown(KeyCode.Alpha6)) {
			manager.ClickNumber(6);
		} else if (Input.GetKeyDown(KeyCode.Alpha7)) {
			manager.ClickNumber(7);
		} else if (Input.GetKeyDown(KeyCode.Alpha8)) {
			manager.ClickNumber(8);
		} else if (Input.GetKeyDown(KeyCode.Alpha9)) {
			manager.ClickNumber(9);
		} else if (Input.GetKeyDown(KeyCode.Alpha0)) {
			manager.ClickNumber(0);
		}

		if (Input.GetKeyDown(KeyCode.RightShift)) {
			manager.ClickFuncKey("Save");
		}
    }
}

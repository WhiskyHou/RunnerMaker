using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorModelButton : MonoBehaviour {

	public GameObject manager;

    void Start() {
        
    }

    void Update() {
        
    }

	public void OnClickButton(int index) {
		Debug.Log("=== click button === " + index);

		EditorManager editor = manager.GetComponent<EditorManager>();
		editor.ClickNumber(index);
	}
}

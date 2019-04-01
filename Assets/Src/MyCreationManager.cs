using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCreationManager : MonoBehaviour {

	public GameObject newMapWindow;

    void Start() {
        
    }

    void Update() {
        
    }

	public void OnClickButtonGroup(string button) {
		switch (button) {
			case "create":
				newMapWindow.SetActive(true);
				break;
			case "remove":
				newMapWindow.SetActive(false);
				break;
			case "upload":
				break;
			case "edit":
				break;
			default:
				break;
		}
	}
}

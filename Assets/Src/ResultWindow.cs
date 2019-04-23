using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultWindow : MonoBehaviour {

	public Text title;

	public Text content;

    void Start() {
        
    }

    void Update() {
        
    }

	public void Show(bool isWin, string text) {
		gameObject.SetActive(true);
		title.text = isWin ? "通 关 成 功" : "通 关 失 败";
		content.text = text;
	}
}

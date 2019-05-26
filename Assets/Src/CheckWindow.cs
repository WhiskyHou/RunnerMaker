using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckWindow : MonoBehaviour {

	public Text title;

	public Text content;

    void Start() {
        
    }

    void Update() {
        
    }

	public void Show(bool isWin) {
		gameObject.SetActive(true);
		title.text = isWin ? "通 关 成 功" : "通 关 失 败";
		content.text = isWin ? "地图现在可以上传" : "地图没有通过还不能上传";
	}
}

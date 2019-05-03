using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyUpMap : MonoBehaviour {

	public Text mapNameText;

	public Text goodText;

	public Text diffText;

	public Text passText;

	public Text trysText;

    void Start() {
        
    }

    void Update() {
        
    }

	public void Init(MyMapInfo info) {
		mapNameText.text = info.nickname;
		goodText.text = info.goodCount.ToString();
		diffText.text = info.diffCount.ToString();
		passText.text = info.passCount.ToString();
		trysText.text = info.trysCount.ToString();
	}
}

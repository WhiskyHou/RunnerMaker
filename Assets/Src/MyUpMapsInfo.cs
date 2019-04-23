using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyUpMapsInfo : MonoBehaviour {

	public Text goodText;

	public Text diffText;

	public Text passText;

	public Text trysText;

    void Start() {
        
    }

    void Update() {
    	
    }

	public void SetTexts(int good, int diff, int pass, int trys) {
		goodText.text = "点赞人数：" + good.ToString();
		diffText.text = "服了人数：" + diff.ToString();
		passText.text = "通关人数：" + pass.ToString();
		trysText.text = "游玩人数：" + trys.ToString();
	}
}

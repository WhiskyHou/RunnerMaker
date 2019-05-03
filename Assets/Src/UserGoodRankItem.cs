using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserGoodRankItem : MonoBehaviour {

	public Text rankText;

	public Text userNameText;

	public Text goodNumText;

    void Start() {
        
    }

    void Update() {
        
    }

	public void Init(GoodRankInfo info) {
		rankText.text = info.rank.ToString();
		userNameText.text = info.userNme;
		goodNumText.text = info.goodCount.ToString();
	}
}

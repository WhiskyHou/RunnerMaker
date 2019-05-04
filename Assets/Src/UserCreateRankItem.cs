using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserCreateRankItem : MonoBehaviour {

	public Text rankText;

	public Text userNameText;

	public Text goodNumText;

	void Start() {
        
    }

    void Update() {
        
    }

	public void Init(CreateRankInfo info) {
		rankText.text = info.rank.ToString();
		userNameText.text = info.nickname;
		goodNumText.text = info.createCount.ToString();
	}
}

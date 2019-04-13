using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	private GameManager manager;

	private float countDown;

	private float duringTime;

	private bool gameing = true;

    void Start() {

    }

    void Update() {
		if(duringTime >= countDown && gameing) {
			manager.GameOver();
			gameing = false;
			return;
		}
		duringTime += Time.deltaTime;
		manager.countDownText.text = (countDown - (int)Math.Floor(duringTime)).ToString() + " s";
	}

	public void Init(GameManager manager, float countDown) {
		this.manager = manager;
		this.countDown = countDown;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Text countDownText;

	private Map map;

	private float startTime;

	private int status = 0; 	// 0: ing 1: over 2: pass

    void Start() {
		map = GetComponent<MapBuilder>().GetMap();
    }

    void Update() {
		if(startTime >= map.countDown && status == 0) {
			status = 1;
			GameOver();
		}

		float delta = Time.deltaTime;

		startTime += delta;
		countDownText.text = (map.countDown - (int)Math.Floor(startTime)).ToString() + " s";
    }

	public void GameOver() {
		Debug.Log("=== Game Over ===");
		Time.timeScale = 0;
	}

	public void GamePass() {
		Debug.Log("=== Game Pass ===");

	}

	public void OnClickBack() {
		Time.timeScale = 1;
		SceneManager.LoadScene("MapListScene");
	}
}

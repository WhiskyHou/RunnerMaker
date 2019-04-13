using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Text countDownText;

	private Map map;

	private int status = 0; 	// 0: ing 1: over 2: pass

    void Start() {
		map = GetComponent<MapBuilder>().GetMap();
    }

    void Update() {

    }

	public void GameOver() {
		Debug.Log("=== Game Over ===");
		Time.timeScale = 0;
	}

	public void GamePass() {
		Debug.Log("=== Game Pass ===");
		Time.timeScale = 0;
	}

	public void OnClickBack() {
		Time.timeScale = 1;
		SceneManager.LoadScene("MapListScene");
	}
}

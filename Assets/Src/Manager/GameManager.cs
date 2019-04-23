using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Text countDownText;

	public ResultWindow resultWindow;

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
		resultWindow.Show(false, "你可真是太菜了");
	}

	public void GamePass() {
		Debug.Log("=== Game Pass ===");
		Time.timeScale = 0;

		Timer timer = GameObject.Find("Timer").GetComponent<Timer>();
		resultWindow.Show(true, "通关用时： " + timer.GetDurringTime().ToString() + "S");
	}

	public void OnClickBack() {
		Time.timeScale = 1;
		SceneManager.LoadScene("MapListScene");
	}

	public void GoBackToMapListScene() {
		Time.timeScale = 1;
		SceneManager.LoadScene("MapListScene");
	}

	public void RePlay() {
		Time.timeScale = 1;
		SceneManager.LoadScene("SampleScene");
	}
}

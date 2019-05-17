using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Text countDownText;

	public ResultWindow resultWindow;

	public GameObject WinTabFuncButtons;

	public NetHelper netHelper;

	private Map map;

    void Start() {
		map = GetComponent<MapBuilder>().GetMap();
    }

    void Update() {

    }

	public void GameOver() {
		Debug.Log("=== Game Over ===");
		Time.timeScale = 0;
		resultWindow.Show(false, "你可真是太菜了");

		WinTabFuncButtons.SetActive(false);

		string trys = "{\"uid\":" + LoginStatus.Instance.GetUser().uid + ",\"mid\":" + map.mid + ",\"type\":\"trys\"}";
		netHelper.Post("/upgradeMapInfo", trys);
	}

	public void GamePass() {
		Debug.Log("=== Game Pass ===");
		Time.timeScale = 0;

		Timer timer = GameObject.Find("Timer").GetComponent<Timer>();
		resultWindow.Show(true, "通关用时： " + timer.GetDurringTime().ToString() + "S");

		WinTabFuncButtons.SetActive(true);

		string data = "{\"uid\":" + LoginStatus.Instance.GetUser().uid + ",\"mid\":" + map.mid + ",\"time\":" + timer.GetDurringTime() + ",\"type\":\"pass\"}";
		netHelper.Post("/upgradeMapInfo", data);

		string trys = "{\"uid\":" + LoginStatus.Instance.GetUser().uid + ",\"mid\":" + map.mid + ",\"type\":\"trys\"}";
		netHelper.Post("/upgradeMapInfo", trys);
	}

	public void OnClickBack() {
		Time.timeScale = 1;
		SceneManager.LoadScene("MapListScene");
	}

	public void OnClickFuncButtons(int type) {
		if (type == 0) {
			string data = "{\"uid\":" + LoginStatus.Instance.GetUser().uid + ",\"mid\":" + map.mid + ",\"type\":\"good\"}";
			netHelper.Post("/upgradeMapInfo", data);
		} else if (type == 1) {
			string data = "{\"uid\":" + LoginStatus.Instance.GetUser().uid + ",\"mid\":" + map.mid + ",\"type\":\"diff\"}";
			netHelper.Post("/upgradeMapInfo", data);
		}
	}

	public void GoBackToMapListScene() {
		Time.timeScale = 1;
		SceneManager.LoadScene("MapListScene");
	}

	public void RePlay() {
		Time.timeScale = 1;
		SceneManager.LoadScene("GameScene");
	}
}

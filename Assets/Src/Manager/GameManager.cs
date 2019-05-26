using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Text countDownText;

	public ResultWindow resultWindow;

	public CheckWindow checkWindow;

	public GameObject WinTabFuncButtons;

	private NetHelper netHelper = NetHelper.Instance;

	private FileHelper fileHelper = FileHelper.Instance;

	private bool isEnd = false;

	private Map map;

	private string model = "";

    void Start() {
		map = GetComponent<MapBuilder>().GetMap();
		model = EnterGame.Instance.isCheckingMap ? "checking" : "playing";
    }

    void Update() {

    }

	public void GameOver() {
		if (isEnd) return;

		isEnd = true;
		Debug.Log("=== Game Over ===");
		Time.timeScale = 0;

		if (model == "playing") {
			resultWindow.Show(false, "你可真是太菜了");

			WinTabFuncButtons.SetActive(false);

			string trys = "{\"uid\":" + LoginStatus.Instance.GetUser().uid + ",\"mid\":" + map.mid + ",\"type\":\"trys\"}";
			netHelper.Post("/upgradeMapInfo", trys);

		} else if  (model == "checking") {
			checkWindow.Show(false);

			string oldname = "Data/" + LoginStatus.Instance.GetUser().username + "/" + EnterGame.Instance.checkMapFilename;
			string newname = "Data/" + LoginStatus.Instance.GetUser().username + "/" + EnterGame.Instance.checkMapFilename.Split('.')[0] + ".json.no";
			fileHelper.RenameFile(oldname, newname);
		}
	}

	public void GamePass() {
		if (isEnd) return;

		isEnd = true;
		Debug.Log("=== Game Pass ===");
		Time.timeScale = 0;

		if (model == "playing") {
			Timer timer = GameObject.Find("Timer").GetComponent<Timer>();
			resultWindow.Show(true, "通关用时： " + timer.GetDurringTime().ToString() + "S");

			WinTabFuncButtons.SetActive(true);

			string data = "{\"uid\":" + LoginStatus.Instance.GetUser().uid + ",\"mid\":" + map.mid + ",\"time\":" + timer.GetDurringTime() + ",\"type\":\"pass\"}";
			netHelper.Post("/upgradeMapInfo", data);

			string trys = "{\"uid\":" + LoginStatus.Instance.GetUser().uid + ",\"mid\":" + map.mid + ",\"type\":\"trys\"}";
			netHelper.Post("/upgradeMapInfo", trys);

		} else if(model == "checking") {
			checkWindow.Show(true);

			string oldname = "Data/" + LoginStatus.Instance.GetUser().username + "/" + EnterGame.Instance.checkMapFilename;
			string newname = "Data/" + LoginStatus.Instance.GetUser().username + "/" + EnterGame.Instance.checkMapFilename.Split('.')[0] + ".json.ok";
			fileHelper.RenameFile(oldname, newname);
		}
		
	}

	public void OnClickBack() {
		Time.timeScale = 1;
		if (model == "playing") {
			SceneManager.LoadScene("MapListScene");
		} else if (model == "checking") {
			SceneManager.LoadScene("MyCreationScene");
		}
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

	public void GoBackToMyCreationScene() {
		Time.timeScale = 1;
		SceneManager.LoadScene("MyCreationScene");
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

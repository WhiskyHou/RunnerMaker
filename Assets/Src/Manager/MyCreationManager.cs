using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MyCreationManager : MonoBehaviour {

	public GameObject mapListGroup;
	public GameObject mapUpGroup;
	public GameObject buttonGroup;
	public GameObject newMapWindow;

	public InputField nameText;
	public InputField widthText;
	public InputField heightText;

	public FileHelper fileHelper;
	public NetHelper netHelper;

	private MyMap currentMap = null;

	void Start() {
		CreateMapList();
	}

	void Update() {

	}

	public void OnClickBack() {
		SceneManager.LoadScene("HomeScene");
	}

	public void OnClickButtonGroup(string button) {
		switch (button) {
			case "create":
				newMapWindow.SetActive(true);
				break;
			case "remove":
				if (currentMap) {
					fileHelper.RemoveFile("Data/" + LoginStatus.Instance.GetUser().username + "/" + currentMap.fileName);
				}
				break;
			case "upload":
				if (currentMap) {
					UploadMap();
				}
				break;
			case "edit":
				if (currentMap) {
					OpenMap.Instance.LoadMapFile(currentMap.fileName);
					SceneManager.LoadScene("EditorScene");
				}
				break;
			default:
				break;
		}
	}

	public void OnClickOK() {
		Debug.Log("name: " + nameText.text + " width: " + widthText.text + " height: " + heightText.text);

		string mapname = "";
		int width = 0;
		int height = 0;

		try {
			mapname = nameText.text;
			width = int.Parse(widthText.text);
			height = int.Parse(heightText.text);
		} catch (Exception e) {
			width = height = 0;
		}

		if (mapname.Equals("") || width == 0 || height == 0) {
			GameObject toast = Instantiate(Resources.Load("prefab/toast") as GameObject);
			toast.transform.SetParent(GameObject.Find("UI").transform, false);
			toast.GetComponent<Toast>().Set("不符合规范", 2000);
		} else {
			Map map = new Map {
				mid = -1,
				uid = LoginStatus.Instance.GetUser().uid,
				createrName = LoginStatus.Instance.GetUser().nickname,
				nickName = nameText.text,
				width = int.Parse(widthText.text),
				height = int.Parse(heightText.text),
				countDown = 0
			};

			string path = "Data/" + LoginStatus.Instance.GetUser().username + "/" + nameText.text + ".json";
			// TODO: 这里判断需要重写
			if (File.Exists(path)) {
				GameObject toast = Instantiate(Resources.Load("prefab/toast") as GameObject);
				toast.transform.SetParent(GameObject.Find("UI").transform, false);
				toast.GetComponent<Toast>().Set("已有同名地图关卡", 2000);
			} else {
				fileHelper.WriteToFile(path, JsonUtility.ToJson(map));
			}
		}

		newMapWindow.SetActive(false);
	}

	public void OnClickChooseMap(MyMap map) {
		currentMap = map;
		Debug.Log("=== currentmap: " + currentMap.fileName);
	}

	public void CreateMapList() {
		// TODO 后面要做成翻页的
		string path = "Data/" + LoginStatus.Instance.GetUser().username + "/";
		DirectoryInfo dirInfo = new DirectoryInfo(path);
		List<FileInfo> infoList = new List<FileInfo>(dirInfo.GetFiles());
		infoList.Sort(new Comp());
		int index = 0;
		infoList.ForEach((FileInfo obj) => {
			GameObject mymap = Instantiate(Resources.Load("prefab/MyMap") as GameObject);
			mymap.transform.SetParent(mapListGroup.transform, true);
			mymap.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, -60f * index, 0f);

			mymap.GetComponent<MyMap>().Init(obj.Name, this);

			index++;
		});
	}

	public void UploadMap() {
		string data = fileHelper.ReadFile("Data/" + LoginStatus.Instance.GetUser().username + "/" + currentMap.fileName);
		netHelper.Post("/uploadMap", data);
	}
}

class Comp : Comparer<FileInfo> {
	public override int Compare(FileInfo x, FileInfo y) {
		if (x.CreationTime.CompareTo(y.CreationTime) < 0) {
			return 1;
		}
		return -1;
	}
}


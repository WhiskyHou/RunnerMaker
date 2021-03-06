﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MyCreationManager : MonoBehaviour {

	public GameObject mapList;
	public GameObject mapListGroup;
	public int mapListGroupLength;
	public Text mapListPageText;

	public GameObject mapUp;
	public GameObject mapUpGroup;
	public int mapUpGroupLength;
	public Text mapUpPageText;

	public GameObject buttonGroup;
	public GameObject newMapWindow;

	public GameObject infoGroup;

	public InputField nameText;
	public InputField widthText;
	public InputField heightText;

	private FileHelper fileHelper = FileHelper.Instance;
	private NetHelper netHelper = NetHelper.Instance;

	private bool isMapListState = true;

	private int mapListPageCount = 0;
	private int currentListPageindex = 0;

	private int mapUpPageCount = 0;
	private int currentUpPageindex = 0;

	private List<FileInfo> mapListInfo;
	private List<MyMapInfo> mapUpInfo;

	private MyMap currentMap = null;

	void Start() {
		ReadFolderFile();

		GetRemoteMaps();
	}

	void Update() {

	}


	/**
	 * 事件响应
	 */ 
	public void OnClickBack() {
		SceneManager.LoadScene("HomeScene");
	}

	public void OnClickChangeState() {
		isMapListState = !isMapListState;

		mapList.SetActive(isMapListState);
		mapUp.SetActive(!isMapListState);
		buttonGroup.SetActive(isMapListState);
		infoGroup.SetActive(!isMapListState);
	}

	public void OnClickButtonGroup(string button) {
		switch (button) {
			case "create":
				newMapWindow.SetActive(true);
				break;
			case "remove":
				if (currentMap) {
					RemoveMap();
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

			string path = "Data/" + LoginStatus.Instance.GetUser().username + "/" + nameText.text + ".json.no";
			string pathok = "Data/" + LoginStatus.Instance.GetUser().username + "/" + nameText.text + ".json.ok";
			// TODO: 这里判断需要重写 
			// 5.26: 忽略文件后缀类型进行重复性判断
			if (File.Exists(path) || File.Exists(pathok)) {
				GameObject toast = Instantiate(Resources.Load("prefab/toast") as GameObject);
				toast.transform.SetParent(GameObject.Find("Canvas").transform, false);
				toast.GetComponent<Toast>().Set("已有同名地图关卡", 2000);
			} else {
				fileHelper.WriteToFile(path, JsonUtility.ToJson(map));
			}
		}

		newMapWindow.SetActive(false);

		currentListPageindex = 0;
		ReadFolderFile();
		BuildMapList();
	}

	public void OnClickChooseMap(MyMap map) {
		if (isMapListState) {
			currentMap = map;
			Debug.Log("=== currentmap: " + currentMap.fileName);
		} else {
			// infoGroup.GetComponent<MyUpMapsInfo>().SetTexts(map.info.goodCount, map.info.diffCount, map.info.passCount, map.info.trysCount); ;
		}
	}

	public void OnClickPage(int type) {
		if (isMapListState) {
			if (type == 0) {
				currentListPageindex = currentListPageindex - 1 < 0 ? 0 : currentListPageindex - 1;
			} else if (type == 1) {
				currentListPageindex = currentListPageindex + 1 > mapListPageCount - 1 ? mapListPageCount - 1 : currentListPageindex + 1;
			}
			BuildMapList();
		} else {
			if (type == 0) {
				currentUpPageindex = currentUpPageindex - 1 < 0 ? 0 : currentUpPageindex - 1;
			} else if (type == 1) {
				currentUpPageindex = currentUpPageindex + 1 > mapUpPageCount - 1 ? mapUpPageCount - 1 : currentUpPageindex + 1;
			}
			BuildMapUp();
		}
	}


	/**
	 * 两个列表的数据获取和构建
	 */
	public void ReadFolderFile() {
		// 读取文件夹下文件列表
		string path = "Data/" + LoginStatus.Instance.GetUser().username + "/";
		DirectoryInfo dirInfo = new DirectoryInfo(path);
		mapListInfo = new List<FileInfo>(dirInfo.GetFiles());
		mapListInfo.Sort(new Comp());

		// 计算页数
		mapListPageCount = (int) Math.Ceiling((float) mapListInfo.Count / mapListGroupLength);

		BuildMapList();
	}

	public void BuildMapList() {
		// 清空 list 节点下的所有子节点
		Transform[] children = mapListGroup.transform.GetComponentsInChildren<Transform>();
		foreach(Transform child in children) {
			if (!child.Equals(mapListGroup.transform)) {
				Destroy(child.gameObject);
			}
		}

		// 给 list 节点添加对应的子项
		int index = 0;
		for (int i = currentListPageindex * mapListGroupLength; i < Math.Min(mapListInfo.Count, (currentListPageindex + 1) * mapListGroupLength); i++) {
			GameObject mymap = Instantiate(Resources.Load("prefab/MyMap") as GameObject);
			mymap.transform.SetParent(mapListGroup.transform, true);
			mymap.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, -60f * index, 0f);
			mymap.GetComponent<MyMap>().Init(mapListInfo[i].Name, this);

			index++;
		}

		// 更新页码
		mapListPageText.text = (currentListPageindex + 1).ToString() + " / " + mapListPageCount.ToString();
	}

	public void GetRemoteMaps() {
		// 请求获取数据
		netHelper.Post("/getRemoteMapsInfo", JsonUtility.ToJson(LoginStatus.Instance.GetUser()), (string res)=> {
			GetRemoteMapsResult result = JsonUtility.FromJson<GetRemoteMapsResult>(res);
			mapUpInfo = result.maps;
			// 计算页数
			mapUpPageCount = (int) Math.Ceiling((float) result.maps.Count / mapUpGroupLength);

			BuildMapUp();
		});
	}

	public void BuildMapUp() {
		// 清空 up 节点下的所有子节点
		Transform[] children = mapUpGroup.transform.GetComponentsInChildren<Transform>();
		foreach (Transform child in children) {
			if (!child.Equals(mapUpGroup.transform)) {
				Destroy(child.gameObject);
			}
		}

		// 给 up 节点添加对应的子项
		int index = 0;
		for (int i = currentUpPageindex * mapUpGroupLength; i < Math.Min(mapUpInfo.Count, (currentUpPageindex + 1) * mapUpGroupLength); i++) {
			GameObject mymap = Instantiate(Resources.Load("prefab/MyUpMap") as GameObject);
			mymap.transform.SetParent(mapUpGroup.transform, true);
			mymap.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, -60f * index, 0f);
			mymap.GetComponent<MyUpMap>().Init(mapUpInfo[i]);

			index++;
		}

		// 更新页码
		mapUpPageText.text = (currentUpPageindex + 1).ToString() + " / " + mapUpPageCount.ToString();
	}


	/**
	 * 逻辑处理
	 */
	public void UploadMap() {
		if (currentMap.fileName.Split('.')[2] == "ok") {
			string data = fileHelper.ReadFile("Data/" + LoginStatus.Instance.GetUser().username + "/" + currentMap.fileName);
			netHelper.Post("/uploadMap", data);

			RemoveMap();
		} else {
			GameObject toast = Instantiate(Resources.Load("prefab/toast") as GameObject);
			toast.transform.SetParent(GameObject.Find("Canvas").transform, false);
			toast.GetComponent<Toast>().Set("未通关验证的地图不能上传", 2000);
		}
	}

	public void RemoveMap() {
		fileHelper.RemoveFile("Data/" + LoginStatus.Instance.GetUser().username + "/" + currentMap.fileName);
		currentListPageindex = 0;
		ReadFolderFile();
		BuildMapList();
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


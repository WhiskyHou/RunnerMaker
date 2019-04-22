using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MyCreationManager : MonoBehaviour {

	public GameObject mapListGroup;
	public int mapListGroupLength;
	public Text mapListPageText;
	public GameObject mapUpGroup;
	public GameObject buttonGroup;
	public GameObject newMapWindow;

	public InputField nameText;
	public InputField widthText;
	public InputField heightText;

	public FileHelper fileHelper;
	public NetHelper netHelper;

	private int mapListPageCount = 0;
	private int currentPageindex = 0;

	private List<FileInfo> mapListInfo;

	private MyMap currentMap = null;

	void Start() {
		ReadFolderFile();
		BuildMapList();
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

	public void OnClickPage(int type) {
		if (type == 0) {
			currentPageindex = currentPageindex - 1 < 0 ? 0 : currentPageindex - 1;
		} else if (type == 1) {
			currentPageindex = currentPageindex + 1 > mapListPageCount - 1 ? mapListPageCount - 1 : currentPageindex + 1;
		}

		BuildMapList();
	}

	public void ReadFolderFile() {
		// 读取文件夹下文件列表
		string path = "Data/" + LoginStatus.Instance.GetUser().username + "/";
		DirectoryInfo dirInfo = new DirectoryInfo(path);
		mapListInfo = new List<FileInfo>(dirInfo.GetFiles());
		mapListInfo.Sort(new Comp());

		// 计算页数
		mapListPageCount = (int) Math.Ceiling((float) mapListInfo.Count / mapListGroupLength);
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
		for (int i = currentPageindex * mapListGroupLength; i < Math.Min(mapListInfo.Count, (currentPageindex + 1) * mapListGroupLength); i++) {
			GameObject mymap = Instantiate(Resources.Load("prefab/MyMap") as GameObject);
			mymap.transform.SetParent(mapListGroup.transform, true);
			mymap.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, -60f * index, 0f);
			mymap.GetComponent<MyMap>().Init(mapListInfo[i].Name, this);

			index++;
		}

		// 更新页码
		mapListPageText.text = (currentPageindex + 1).ToString() + " / " + mapListPageCount.ToString();
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


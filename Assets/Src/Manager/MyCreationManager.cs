using System.IO;
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

	public FileHelper fileHelper;
	public NetHelper netHelper;

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
		BuildMapList();

		GetRemoteMaps();
		BuildMapUp();
	}

	void Update() {

	}

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

		currentListPageindex = 0;
		ReadFolderFile();
		BuildMapList();
	}

	public void OnClickChooseMap(MyMap map) {
		if (isMapListState) {
			currentMap = map;
			Debug.Log("=== currentmap: " + currentMap.fileName);
		} else {
			infoGroup.GetComponent<MyUpMapsInfo>().SetTexts(map.info.goodCount, map.info.diffCount, map.info.passCount, map.info.trysCount); ;
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
		for (int i = currentListPageindex * mapListGroupLength; i < Math.Min(mapListInfo.Count, (currentListPageindex + 1) * mapListGroupLength); i++) {
			GameObject mymap = Instantiate(Resources.Load("prefab/MyMap") as GameObject);
			mymap.transform.SetParent(mapListGroup.transform, true);
			mymap.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, -60f * index, 0f);
			mymap.GetComponent<MyMap>().Init(mapListInfo[i].Name.Split('.')[0], this);

			index++;
		}

		// 更新页码
		mapListPageText.text = (currentListPageindex + 1).ToString() + " / " + mapListPageCount.ToString();
	}

	public void UploadMap() {
		string data = fileHelper.ReadFile("Data/" + LoginStatus.Instance.GetUser().username + "/" + currentMap.fileName);
		netHelper.Post("/uploadMap", data);

		RemoveMap();
	}

	public void RemoveMap() {
		fileHelper.RemoveFile("Data/" + LoginStatus.Instance.GetUser().username + "/" + currentMap.fileName);
		currentListPageindex = 0;
		ReadFolderFile();
		BuildMapList();
	}

	public void GetRemoteMaps() {
		// 请求获取数据
		string str = netHelper.Post("/getRemoteMapsInfo", JsonUtility.ToJson(LoginStatus.Instance.GetUser()));
		GetRemoteMapsResult result = JsonUtility.FromJson<GetRemoteMapsResult>(str);
		mapUpInfo = result.maps;

		// 计算页数
		mapUpPageCount = (int) Math.Ceiling((float) result.maps.Count / mapUpGroupLength);
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
			GameObject mymap = Instantiate(Resources.Load("prefab/MyMap") as GameObject);
			mymap.transform.SetParent(mapUpGroup.transform, true);
			mymap.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, -60f * index, 0f);
			mymap.GetComponent<MyMap>().Init(mapUpInfo[i].nickname, this, mapUpInfo[i]);

			index++;
		}

		// 更新页码
		mapUpPageText.text = (currentUpPageindex + 1).ToString() + " / " + mapUpPageCount.ToString();
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


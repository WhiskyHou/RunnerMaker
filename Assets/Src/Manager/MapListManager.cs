using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapListManager : MonoBehaviour {

	public GameObject mapListGroup;
	public int mapListGroupLength;
	public Text mapListPageText;

	private int mapListPageCount = 0;
	private int currentListPageindex = 0;

	private List<MapInfo> mapListInfo;

	private NetHelper netHelper = NetHelper.Instance;

	void Start() {
		GetMapsInfo();
		BuildMapList();
	}

	void Update() {

	}


	/**
	 * 事件响应
	 */
	public void OnClickBack() {
		SceneManager.LoadScene("HomeScene");
	}

	public void OnClickSort(int type) {
		SortMapInfo(type);
		BuildMapList();
	}

	public void OnClickPage(int type) {
		if (type == 0) {
			currentListPageindex = currentListPageindex - 1 < 0 ? 0 : currentListPageindex - 1;
		} else if(type == 1) {
			currentListPageindex = currentListPageindex + 1 > mapListPageCount - 1 ? mapListPageCount - 1 : currentListPageindex + 1;
		}
		BuildMapList();
	}


	/**
	 * 列表的数据获取和构建
	 */
	public void GetMapsInfo() {
		netHelper.Post("/getMaps", "{}", (string res) => {
			GetMapsResult result = JsonUtility.FromJson<GetMapsResult>(res);

			if(result.error == 0) {
				mapListInfo = result.maps;
				mapListPageCount = (int)Math.Ceiling((float)result.maps.Count / mapListGroupLength);
			} else {
				Debug.Log("=== get maps info fail ===");
			}
		});

	}

	public void BuildMapList() {
		//int index = 0;
		//mapsInfo.ForEach((obj) => {
		//	GameObject map = Instantiate(Resources.Load("prefab/GameMap") as GameObject);
		//	map.transform.SetParent(GameObject.Find("MapList").transform, false);
		//	map.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, -80f * index, 0f);
		//	map.GetComponent<GameMap>().Init(obj, this);
		//	index++;
		//});

		// 清空 list 节点下的所有子节点
		Transform[] children = mapListGroup.transform.GetComponentsInChildren<Transform>();
		foreach (Transform child in children) {
			if (!child.Equals(mapListGroup.transform)) {
				Destroy(child.gameObject);
			}
		}

		// 给 list 节点添加对应的子项
		int index = 0;
		for (int i = currentListPageindex * mapListGroupLength; i < Math.Min(mapListInfo.Count, (currentListPageindex + 1) * mapListGroupLength); i++) {
			GameObject mymap = Instantiate(Resources.Load("prefab/GameMap") as GameObject);
			mymap.transform.SetParent(mapListGroup.transform, true);
			mymap.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, -80f * index, 0f);
			mymap.GetComponent<GameMap>().Init(mapListInfo[i], this);

			index++;
		}

		// 更新页码
		mapListPageText.text = (currentListPageindex + 1).ToString() + " / " + mapListPageCount.ToString();
	}


	/**
	 * 逻辑处理
	 */
	public void EnterMap(int mid) {
		string postData = "{\"mid\":\"" + mid + "\"}";
		netHelper.Post("/getMapById", postData, (string res) => {
			Map result = JsonUtility.FromJson<Map>(res);
			EnterGame.Instance.SetMap(result);
			EnterGame.Instance.isCheckingMap = false;
			SceneManager.LoadScene("GameScene");
		});

	}

	public void SortMapInfo(int type) {
		if (type == 0) {
			mapListInfo.Sort(new CompLatest());
		} else if(type == 1) {
			mapListInfo.Sort(new CompBest());
		} else if(type == 2) {
			mapListInfo.Sort(new CompHardest());
		}
	}
}

class CompLatest : Comparer<MapInfo> {
	public override int Compare(MapInfo x, MapInfo y) {
		if (x.mid < y.mid) {
			return 1;
		} if(x.mid > y.mid) {
			return - 1;
		}
		return 0;
	}
}

class CompBest : Comparer<MapInfo> {
	public override int Compare(MapInfo x, MapInfo y) {
		if (x.goodCount < y.goodCount) {
			return 1;
		} if (x.goodCount > y.goodCount) {
			return -1;
		}
		return 0;
	}
}

class CompHardest : Comparer<MapInfo> {
	public override int Compare(MapInfo x, MapInfo y) {

		float xPre = x.passCount == 0 ? ((float)x.trysCount / 1) : ((float)x.trysCount / x.passCount);
		float yPre = y.passCount == 0 ? ((float)y.trysCount / 1) : ((float)y.trysCount / y.passCount);

		if (xPre < yPre) {
			return 1;
		} if (xPre > yPre) {
			return -1;
		}
		return 0;
	}
}

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

	public NetHelper netHelper;

	private int mapListPageCount = 0;
	private int currentListPageindex = 0;

	private List<MapInfo> mapListInfo;

	void Start() {
		GetMapsInfo();
		BuildMapList();
	}

	void Update() {

	}

	public void OnClickBack() {
		SceneManager.LoadScene("HomeScene");
	}

	public void OnClickPage(int type) {
		if (type == 0) {
			currentListPageindex = currentListPageindex - 1 < 0 ? 0 : currentListPageindex - 1;
		} else if(type == 1) {
			currentListPageindex = currentListPageindex + 1 > mapListPageCount - 1 ? mapListPageCount - 1 : currentListPageindex + 1;
		}
		BuildMapList();
	}

	public void GetMapsInfo() {
		GetMapsResult result = JsonUtility.FromJson<GetMapsResult>(netHelper.Post("/getMaps", "{}"));

		if(result.error == 0) {
			mapListInfo = result.maps;
			mapListPageCount = (int)Math.Ceiling((float)result.maps.Count / mapListGroupLength);
		} else {
			Debug.Log("=== get maps info fail ===");
		}
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

	public void EnterMap(int mid) {
		string postData = "{\"mid\":\"" + mid + "\"}";
		string data = netHelper.Post("/getMapById", postData);

		Map result = JsonUtility.FromJson<Map>(data);
		EnterGame.Instance.SetMap(result);
		SceneManager.LoadScene("SampleScene");
	}
}

class CompLatest : Comparer<GameMap> {
	public override int Compare(GameMap x, GameMap y) {
		throw new System.NotImplementedException();
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour {

	public GameObject player;
	public GameObject end;
	public GameObject stone;
	public GameObject ice;
	public GameObject killer;
	public GameObject spring;
	public GameObject sword;
	public GameObject cannon;

	public FileHelper fileHelper;

	private Map map = new Map();

	private GameObject currentObject = null;

	private List<GameObject> list = new List<GameObject>();

    void Start() {
		map.id = -1;
		map.nickName = "out";
		map.countDown = 120;
		map.width = 100;
		map.height = 100;
		map.startPos = new Position();
		map.endPos = new Position();
		map.nodeInfo = new List<NodeInfo>();
    }

    void Update() {

		Vector3 mousePosOnScene = Input.mousePosition;
		Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(mousePosOnScene);
		
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			if (!currentObject) {
				RemoveObj((int)Math.Round(mousePosInWorld.x), (int)Math.Round(mousePosInWorld.y));
			} else {
				CreateObj((int)Math.Round(mousePosInWorld.x), (int)Math.Round(mousePosInWorld.y));
			}
		}

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			currentObject = stone;
		} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
			currentObject = ice;
		} else if (Input.GetKeyDown(KeyCode.Alpha3)) {
			currentObject = spring;
		} else if (Input.GetKeyDown(KeyCode.Alpha4)) {
			currentObject = cannon;
		} else if (Input.GetKeyDown(KeyCode.Alpha5)) {
			currentObject = sword;
		} else if (Input.GetKeyDown(KeyCode.Alpha6)) {
			currentObject = killer;
		} else if (Input.GetKeyDown(KeyCode.Alpha7)) {
			currentObject = player;
		} else if (Input.GetKeyDown(KeyCode.Alpha8)) {
			currentObject = end;
		} else if (Input.GetKeyDown(KeyCode.Alpha9)) {
			currentObject = null;
		}

		if (Input.GetKeyDown(KeyCode.RightShift)) {
			SaveToFile();
		}
    }

	private void CreateObj(int x, int y) {
		GameObject go = Instantiate(currentObject);
		go.transform.position = new Vector3(x, y, go.transform.position.z);
		go.name = currentObject.name;

		list.Add(go);
	}

	private void RemoveObj(int x, int y) {
		for(int i = 0; i < list.Count; ++i) {
			Vector3 pos = list[i].transform.position;
			if ((int)pos.x == x && (int)pos.y == y) {
				Destroy(list[i]);
				list.Remove(list[i]);
				return;
			}
		}
	}

	private void SaveToFile() {
		list.ForEach((item) => {
			Vector3 pos = item.transform.position;
			if(item.name == "Player") {
				map.startPos.x = (int)pos.x;
				map.startPos.y = (int)pos.y;
			} else if(item.name == "End") {
				map.endPos.x = (int)pos.x;
				map.endPos.y = (int)pos.y;
			} else {
				NodeInfo node = new NodeInfo();
				node.pos = new Position();
				node.pos.x = (int)pos.x;
				node.pos.y = (int)pos.y;
				node.prefabType = item.name;
				map.nodeInfo.Add(node);
			}
		});

		map.Log();

		fileHelper.WriteToFile(JsonUtility.ToJson(map));
	}
}

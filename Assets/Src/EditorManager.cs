using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour {

	public GameObject[] prefabs = new GameObject[9];

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
        
    }

	public void ClickMouse(int x, int y, bool isLeft) {
		if (isLeft) {
			//(currentObject == null) ? RemoveObj(x, y) : CreateObj(x,y);
			if(currentObject == null) {
				RemoveObj(x, y);
			} else {
				CreateObj(x, y);
			}
		} else {

		}
	}

	public void ClickNumber(int number) {
		if(number < prefabs.Length) {
			currentObject = prefabs[number];
		}
	}

	public void ClickFuncKey(string key) {
		switch (key) {
			case "Save":
				SaveToFile();
				break;
			default:
				break;
		}
	}

	private void CreateObj(int x, int y) {
		GameObject go = Instantiate(currentObject);
		go.transform.position = new Vector3(x, y, go.transform.position.z);
		go.name = currentObject.name;

		list.Add(go);
	}

	private void RemoveObj(int x, int y) {
		for (int i = 0; i < list.Count; ++i) {
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
			if (item.name == "Player") {
				map.startPos.x = (int)pos.x;
				map.startPos.y = (int)pos.y;
			} else if (item.name == "End") {
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

		//fileHelper.WriteToFile(JsonUtility.ToJson(map));
		//fileHelper.data = JsonUtility.ToJson(map);
		fileHelper.WriteToFileAsync(@"Assets/Config/out.json", JsonUtility.ToJson(map));
	}
}

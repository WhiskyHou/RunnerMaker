using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EditorManager : MonoBehaviour {

	public GameObject[] prefabs = new GameObject[10];
	public Dictionary<string, GameObject> prefabsD = new Dictionary<string, GameObject>();

	public InputField countDown;

	public GameObject line;

	private FileHelper fileHelper = FileHelper.Instance;

	private Map map = new Map();

	private GameObject currentObject = null;

	private List<GameObject> list = new List<GameObject>();

	void Start() {
		prefabsD.Add("Stone", prefabs[1]);
		prefabsD.Add("Ice", prefabs[2]);
		prefabsD.Add("Brick", prefabs[3]);
		prefabsD.Add("Spring", prefabs[4]);
		prefabsD.Add("Sword", prefabs[5]);
		prefabsD.Add("Cannon", prefabs[6]);
		prefabsD.Add("Killer", prefabs[7]);
		prefabsD.Add("Player", prefabs[8]);
		prefabsD.Add("End", prefabs[9]);


		map = JsonUtility.FromJson<Map>(OpenMap.Instance.mapData);
		BuildMap();
	}

	void Update() {

	}

	public void OnClickButton(int index) {
		ClickNumber(index);
	}

	public void ClickMouse(int x, int y, bool isLeft) {
		if (isLeft) {
			//(currentObject == null) ? RemoveObj(x, y) : CreateObj(x,y);
			if (currentObject == null) {
				RemoveObj(x, y);
			} else {
				CreateObj(x, y);
			}
		} else {
			
		}
	}

	public void ClickNumber(int number) {
		if (number < prefabs.Length) {
			currentObject = prefabs[number];
		}
	}

	public void ClickFuncKey(string key) {
		switch (key) {
			case "Save":
				UpgradeMap();
				SaveToFile();
				ChangeMapStateToNo();
				SceneManager.LoadScene("MyCreationScene");
				break;
			case "Play":
				UpgradeMap();
				SaveToFile();
				PlayToCheck();
				SceneManager.LoadScene("GameScene");
				break;
			default:
				break;
		}
	}

	private void BuildMap() {
		// 1. 根据尺寸进行区块描边
		for (int i = 0; i <= map.width; ++i) {
			GameObject line = Instantiate(this.line);
			LineRenderer render = line.GetComponent<LineRenderer>();
			render.SetPosition(0, new Vector3(i + 0.5f, 0.5f, 0.1f));
			render.SetPosition(1, new Vector3(i + 0.5f, map.height + 0.5f, 0.1f));
		}
		for(int i = 0; i <= map.height; ++i) {
			GameObject line = Instantiate(this.line);
			LineRenderer render = line.GetComponent<LineRenderer>();
			render.SetPosition(0, new Vector3(0.5f, i + 0.5f, 0.1f));
			render.SetPosition(1, new Vector3(map.width + 0.5f, i + 0.5f, 0.1f));
		}

		// 2. 创建起点 和 终点 和 倒计时
		currentObject = prefabs[8];
		CreateObj(map.startPos.x, map.startPos.y);
		currentObject = prefabs[9];
		CreateObj(map.endPos.x, map.endPos.y);
		countDown.text = map.countDown.ToString();

		// 3. 创建模块
		currentObject = prefabs[1];
		map.nodeInfo.ForEach((item) => {
			currentObject = prefabsD[item.prefabType];
			CreateObj(item.pos.x, item.pos.y);
		});

		// 4. 重置当前选定预制体为空
		currentObject = null;
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

	private void UpgradeMap() {
		map.countDown = int.Parse(countDown.text);
		map.nodeInfo.Clear();
		list.ForEach((item) => {
			Vector3 pos = item.transform.position;
			if (item.name == "Player") {
				map.startPos.x = (int)pos.x;
				map.startPos.y = (int)pos.y;
			} else if (item.name == "End") {
				map.endPos.x = (int)pos.x;
				map.endPos.y = (int)pos.y;
			} else {
				NodeInfo node = new NodeInfo {
					pos = new Position()
				};
				node.pos.x = (int)pos.x;
				node.pos.y = (int)pos.y;
				node.prefabType = item.name;
				map.nodeInfo.Add(node);
			}
		});
		map.Log();
	}

	private void SaveToFile() {
		Debug.Log("=== Save List's length: " + list.Count);

		fileHelper.WriteToFile(OpenMap.Instance.filePath, JsonUtility.ToJson(map));
	}

	private void ChangeMapStateToNo() {
		fileHelper.RenameFile(OpenMap.Instance.filePath, OpenMap.Instance.filePath.Split('.')[0] + ".json.no");
	}

	private void PlayToCheck() {
		EnterGame.Instance.isCheckingMap = true;
		EnterGame.Instance.checkMapFilename = OpenMap.Instance.fileName;
		EnterGame.Instance.SetMap(map);
	}
}
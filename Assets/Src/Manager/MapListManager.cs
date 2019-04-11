using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MapListManager : MonoBehaviour {

	public NetHelper netHelper;

	void Start() {
		CreateMapList();
	}

	void Update() {

	}

	public void OnClickBack() {
		SceneManager.LoadScene("HomeScene");
	}

	public void CreateMapList() {
		GetMapsResult result = JsonUtility.FromJson<GetMapsResult>(netHelper.Post("/getMaps", "{}"));
		if(result.error == 0) {
			int index = 0;
			List<MapInfo> maps = new List<MapInfo>(result.maps);
			maps.ForEach((obj) => {
				GameObject map = Instantiate(Resources.Load("prefab/GameMap") as GameObject);
				map.transform.SetParent(GameObject.Find("Canvas").transform, false);
				map.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, -80f * index, 0f);
				map.GetComponent<GameMap>().Init(obj.mid, obj.nickname, this);
				index++;
			});
		} else {
			Debug.Log("=== get maps info fail ===");
		}
	}

	public void EnterMap(GameMap map) {
		string postData = "{\"mid\":\"" + map.mid + "\"}";
		string data = netHelper.Post("/getMapById", postData);

		Map result = JsonUtility.FromJson<Map>(data);
		EnterGame.Instance.SetMap(result);
		SceneManager.LoadScene("SampleScene");
	}
}

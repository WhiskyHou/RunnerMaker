using System;
using System.IO;
using System.Text;
using System.Net;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour {

	private string res = "";

	private Map data;

	void Awake() {
		LoadFromCommunicate();
		//LoadFromLocal();		// 本地读取
		//LoadFromNetSync();		// 网络同步读取
		//LoadFromNetAsync();		// 网络异步读取
	}

	void Start() {
		
    }

    void Update() {
        
    }

	public Map GetMap() {
		return data;
	}

	public void LoadFromCommunicate() {
		res = JsonUtility.ToJson(EnterGame.Instance.map);
		ParseJson();
	}

	public void LoadFromLocal() {
		FileStream fs = File.OpenRead("Assets/Config/maps.json");
		byte[] bytes = new byte[1024];
		UTF8Encoding encoding = new UTF8Encoding(true);
		res = "";
		while (fs.Read(bytes, 0, bytes.Length) > 0) {
			res += encoding.GetString(bytes);

			//bytes = new byte[1024];
			for (int i = 0; i < bytes.Length; ++i) {
				bytes[i] = 0;
			}
		}
		ParseJson();
	}

	public void LoadFromNetSync() {
		byte[] d;
		UTF8Encoding temp = new UTF8Encoding(true);
		using (WebClient wc = new WebClient()) {
			d = wc.DownloadData(new Uri("http://172.18.6.97:8080/maps.json"));
			res = temp.GetString(d);
			ParseJson();
		}
	}

	public void LoadFromNetAsync() {
		UTF8Encoding temp = new UTF8Encoding(true);
		using (WebClient wc = new WebClient()) {
			wc.Credentials = CredentialCache.DefaultCredentials;
			wc.DownloadDataCompleted += new DownloadDataCompletedEventHandler(
				(object sender, DownloadDataCompletedEventArgs e) => { res = temp.GetString(e.Result); ParseJson(); }
			);
			wc.DownloadDataAsync(new Uri("http://172.18.6.97:8080/maps.json"));
		}
	}

	public void ParseJson() {
		Debug.Log(res);
		data = JsonUtility.FromJson<Map>(res);
		//data.maps[0].Log();
		BuildMap();
		BuildBound();
	}

	public void BuildMap() {

		// 创建计时器
		GameObject timer = Resources.Load("prefab/Timer") as GameObject;
		timer = Instantiate(timer);
		timer.GetComponent<Timer>().Init(GetComponent<GameManager>(), data.countDown);
		timer.name = "Timer";

		// 创建人物起点
		GameObject player = Resources.Load("prefab/player") as GameObject;
		player = Instantiate(player);
		player.transform.Translate(new Vector3(data.startPos.x, data.startPos.y, 0));
		player.name = player.tag = "Player";

		// 创建终点
		GameObject end = Resources.Load("prefab/end") as GameObject;
		end = Instantiate(end);
		end.transform.Translate(new Vector3(data.endPos.x, data.endPos.y, 0));
		end.name = end.tag = "End";
		end.GetComponent<End>().Init(GetComponent<GameManager>());

		// 创建各个模块
		List<NodeInfo> nodes = data.nodeInfo;
		nodes.ForEach((item) => {
			GameObject node = Resources.Load("prefab/" + item.prefabType) as GameObject;
			node = Instantiate(node);
			node.transform.Translate(new Vector3(item.pos.x, item.pos.y, 0));
			node.name = node.tag = item.prefabType;
		});
	}

	public void BuildBound() {
		int boundSize = 8;
		GameObject killerPrefab = Resources.Load("prefab/killer") as GameObject;
		float width = data.width;
		float height = data.height;

		//// 横向边界
		//for (int i = 1 - boundSize; i <= width + boundSize; i++) {
		//	GameObject killer1 = Instantiate(killerPrefab);
		//	killer1.transform.position = new Vector3(i, 1 - boundSize, 0);
		//	GameObject killer2 = Instantiate(killerPrefab);
		//	killer2.transform.position = new Vector3(i, height + boundSize, 0);
		//	killer1.GetComponent<Killer>().Init(GetComponent<GameManager>());
		//	killer2.GetComponent<Killer>().Init(GetComponent<GameManager>());
		//}
		//// 纵向边界
		//for (int i = 2 - boundSize; i < height + boundSize; i++) {
		//	GameObject killer1 = Instantiate(killerPrefab);
		//	killer1.transform.position = new Vector3(1 - boundSize, i, 0);
		//	GameObject killer2 = Instantiate(killerPrefab);
		//	killer2.transform.position = new Vector3(width + boundSize, i, 0);
		//	killer1.GetComponent<Killer>().Init(this.GetComponent<GameManager>());
		//	killer2.GetComponent<Killer>().Init(this.GetComponent<GameManager>());
		//}

		for (int i = 1-boundSize; i <= width + boundSize; i++) {
			GameObject killer = Instantiate(killerPrefab);
			killer.transform.position = new Vector3(i, 1 - boundSize, 0);
			killer.GetComponent<Killer>().Init(GetComponent<GameManager>());
		}
	}
}

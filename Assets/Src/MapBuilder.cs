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
		Build();
	}

	public void Build() {
		Map currentMap = data;

		GameObject player = Resources.Load("prefab/player") as GameObject;
		player = Instantiate(player);
		player.transform.Translate(new Vector3(currentMap.startPos.x, currentMap.startPos.y, 0));
		player.name = player.tag = "Player";

		GameObject end = Resources.Load("prefab/end") as GameObject;
		end = Instantiate(end);
		end.transform.Translate(new Vector3(currentMap.endPos.x, currentMap.endPos.y, 0));
		end.name = end.tag = "End";

		List<NodeInfo> nodes = currentMap.nodeInfo;
		nodes.ForEach((item) => {
			GameObject node = Resources.Load("prefab/" + item.prefabType) as GameObject;
			node = Instantiate(node);
			node.transform.Translate(new Vector3(item.pos.x, item.pos.y, 0));
			node.name = node.tag = item.prefabType;
		});
	}
}

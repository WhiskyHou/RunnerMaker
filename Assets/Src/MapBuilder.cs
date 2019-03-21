using System;
using System.IO;
using System.Text;
using System.Net;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour {

	public string jsonPath;

	private string res = "";

	private Maps data;

	void Awake() {
		LoadFromLocal();		// 本地读取
		//LoadFromNetSync();		// 网络同步读取
		//LoadFromNetAsync();		// 网络异步读取
	}

	void Start() {
		
    }

    void Update() {
        
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
		data = JsonUtility.FromJson<Maps>(res);
		data.maps[0].Log();
		Build();
	}

	public void Build() {
		Map currentMap = data.maps[0];

		GameObject player = Resources.Load("prefab/player") as GameObject;
		player = Instantiate(player);
		player.transform.Translate(new Vector3(currentMap.startPos.x, currentMap.startPos.y, 0));
		player.name = player.tag = "Player";

		List<NodeInfo> nodes = currentMap.nodeInfo;
		nodes.ForEach((item) => {
			GameObject node = Resources.Load("prefab/" + item.prefabType) as GameObject;
			node = Instantiate(node);
			node.transform.Translate(new Vector3(item.pos.x, item.pos.y, 0));
			node.name = node.tag = item.prefabType;
		});
	}
}

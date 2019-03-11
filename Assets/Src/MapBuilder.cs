using System;
using System.IO;
using System.Text;
using System.Net;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour {

	public string jsonPath;

	private string res;

	private Maps data;

	void Awake() {
		//LoadFromLocal();		// 本地读取
		//LoadFromNetSync();		// 网络同步读取
		LoadFromNetAsync();		// 网络异步读取
	}

	void Start() {
		
    }

    void Update() {
        
    }

	public void LoadFromLocal() {
		FileStream fs = File.OpenRead("Assets/Config/maps.json");
		byte[] d = new byte[2000];
		UTF8Encoding temp = new UTF8Encoding(true);
		int times = 0;
		res = "";
		while (fs.Read(d, 0, d.Length) > 0) {
			res += temp.GetString(d);
			times++;
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
	}
}

[Serializable]
class Maps {
	public List<Map> maps;

	public void Log() {
		Debug.Log("===== Maps =====\n" + "mapsLength:" + maps.Count);
	}
}

[Serializable]
class Map {
	public int id;
	public string nickName;
	public int countDown;
	public int width;
	public int height;
	public Position startPos;
	public Position endPos;
	public List<NodeInfo> nodeInfo;

	public void Log() {
		Debug.Log("===== Map =====\n"+ "id:"+id + " nickName:" + nickName + " countDown:" + countDown + " width:" + width + " height:" + height + " nodeInfoLength:" + nodeInfo.Count);
	}
}

[Serializable]
class Position {
	public int x;
	public int y;
}

[Serializable]
class NodeInfo {
	public Position pos;
	public string prefabType;
}

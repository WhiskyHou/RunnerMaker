using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMap {
	// 单例模式
	private static OpenMap instance = null;
	public static OpenMap Instance {
		get {
			if (instance == null) {
				instance = new OpenMap();
			}
			return instance;
		}
	}

	//public int id;
	//public string nickName;
	//public int countDown;
	//public int width;
	//public int height;
	//public string path;
	//public Position startPos = new Position();
	//public Position endPos = new Position();
	//public List<NodeInfo> nodeInfo = new List<NodeInfo>();

	public Map map = new Map();

	public string mapData;

	public OpenMap() {
		//map.id = -1;
		//map.nickName = "openmap";
		//map.countDown = 120;
		//map.width = 100;
		//map.height = 100;
		//map.startPos = new Position();
		//map.endPos = new Position();
		//map.nodeInfo = new List<NodeInfo>();

		//mapData = JsonUtility.ToJson(map);

		LoadMapFile("out");
	}

	public void LoadMapFile(string filename) {
		mapData = File.ReadAllText(@"Assets/Config/" + filename + ".json");
	}

}
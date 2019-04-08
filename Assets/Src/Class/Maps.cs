using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Maps {
	public List<Map> maps;

	public void Log() {
		Debug.Log("===== Maps =====\n" + "mapsLength:" + maps.Count);
	}
}

[Serializable]
public class Map {
	public int id;
	public string nickName;
	public string createrName;

	public int goodCount;
	public int diffCount;
	public int passCount;
	public int trysCount;

	public int countDown;
	public int width;
	public int height;

	public Position startPos;
	public Position endPos;
	public List<NodeInfo> nodeInfo;

	public void Log() {
		Debug.Log("===== Map =====\n" + "id:" + id + " nickName:" + nickName + " creater:" + createrName + " countDown:" + countDown + " width:" + width + " height:" + height + " nodeInfoLength:" + nodeInfo.Count);
	}
}

[Serializable]
public class Position {
	public int x;
	public int y;
}

[Serializable]
public class NodeInfo {
	public Position pos;
	public string prefabType;
}
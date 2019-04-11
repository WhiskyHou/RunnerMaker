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
	public int mid;
	public int uid;
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
		Debug.Log("===== Map =====\n" + "id:" + mid + " nickName:" + nickName + " creater:" + createrName + " countDown:" + countDown + " width:" + width + " height:" + height + " nodeInfoLength:" + nodeInfo.Count);
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
	//public List<Property> properties;
}

[Serializable]
public class Property {
	public string name;
	public int value;
}


// 获取所有地图的基本信息
[Serializable]
public class GetMapsResult {
	public int error;
	public List<MapInfo> maps;
}
[Serializable]
public class MapInfo {
	public int mid;
	public int uid;
	public string nickname;
}
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class MapBuilder : MonoBehaviour {

	public string jsonPath;

	private string json = @"{""maps"": [
        {
            ""id"": 0,
            ""nickName"": ""测试地图1"",
            ""countDown"": 120,
            ""width"": 16,
            ""height"": 9,
            ""startPos"": {
                ""x"": 0,
                ""y"": 1
            },
            ""endPos"": {
                ""x"": 8,
                ""y"": 1
            },
            ""nodeInfo"": [
                {
                    ""pos"": {
                        ""x"": 0,
                        ""y"": 0

					},
                    ""prefabType"": ""wall""
                },
                {
                    ""pos"": {
                        ""x"": 1,
                        ""y"": 0
                    },
                    ""prefabType"": ""wall""
                },
                {
                    ""pos"": {
                        ""x"": 2,
                        ""y"": 0
                    },
                    ""prefabType"": ""wall""
                },
                {
                    ""pos"": {
                        ""x"": 3,
                        ""y"": 0
                    },
                    ""prefabType"": ""wall""
                },
                {
                    ""pos"": {
                        ""x"": 4,
                        ""y"": 0
                    },
                    ""prefabType"": ""wall""
                },
                {
                    ""pos"": {
                        ""x"": 5,
                        ""y"": 0
                    },
                    ""prefabType"": ""wall""
                },
                {
                    ""pos"": {
                        ""x"": 6,
                        ""y"": 0
                    },
                    ""prefabType"": ""wall""
                }
            ]
        }
    ]
}";
	private string res;

	private Maps data;

	void Awake() {
		//TextAsset text = Resources.Load("Config/maps") as TextAsset;		// path这里前面默认拼上 Resources/
		//Debug.Log(text);
		FileStream fs = File.OpenRead("Assets/Config/maps.json");
		byte[] d = new byte[2048];
		UTF8Encoding temp = new UTF8Encoding(true);
		int times = 0;
		res = "";
		while (fs.Read(d, 0, d.Length) > 0) {
			Debug.Log(temp.GetString(d));
			res += temp.GetString(d);
			times++;
		}
		Debug.Log(times);
		Debug.Log(res);
	}

	void Start() {
		data = JsonUtility.FromJson<Maps>(res);
		data.maps[0].Log();
    }

    void Update() {
        
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

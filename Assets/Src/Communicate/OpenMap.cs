using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMap {
	// 单例模式
	public static OpenMap instance = null;
	public static OpenMap Instance {
		get {
			if (instance == null) {
				instance = new OpenMap();
			}
			return instance;
		}
	}

	public int width;
	public int height;
	public string path;
	public int countDown;
}

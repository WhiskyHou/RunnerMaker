using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterGame {

	private static EnterGame instance = null;
	public static EnterGame Instance {
		get {
			if (instance == null) {
				instance = new EnterGame();
			}
			return instance;
		}
	}

	public Map map;

	public void SetMap(Map map) {
		this.map = map;
	}
}

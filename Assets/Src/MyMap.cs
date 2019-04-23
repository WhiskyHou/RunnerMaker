using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyMap : MonoBehaviour {

	public Text mapname;

	public string fileName;

	public MyMapInfo info;

	private MyCreationManager manager;

	public void OnClick() {
		if (manager) {
			manager.OnClickChooseMap(this);
		}
	}

	public void Init(string name, MyCreationManager manager, MyMapInfo info = null) {
		mapname.text = name;
		fileName = name + ".json";
		this.info = info;
		this.manager = manager;
	}
}

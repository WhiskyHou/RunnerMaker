using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyMap : MonoBehaviour {

	public Text mapname;

	private MyCreationManager manager;

	public void OnClick() {
		if (manager) {
			manager.OnClickChooseMap(this);
		}
	}

	public void Init(string name, MyCreationManager manager) {
		mapname.text = name;
		this.manager = manager;
	}
}

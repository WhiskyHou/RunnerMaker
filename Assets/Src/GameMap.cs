using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMap : MonoBehaviour {

	public Text mapname;

	public int mid;

	public MapListManager manager;

    void Start() {

    }

    void Update() {

    }

	public void OnClick() {
		if (manager) {
			manager.EnterMap(this);
		}
	}

	public void Init(int mid, string name, MapListManager manager) {
		this.mid = mid;
		mapname.text = name;
		this.manager = manager;
	}
}

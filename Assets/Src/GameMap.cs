using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMap : MonoBehaviour {

	public Text mapname;

	public Text goodText;

	public Text diffText;

	public Text passText;

	public Text trysText;

	private int mid;

	private MapListManager manager;

    void Start() {

    }

    void Update() {

    }

	public void OnClick() {
		if (manager) {
			manager.EnterMap(mid);
		}
	}

	public void Init(MapInfo info, MapListManager manager) {
		mid = info.mid;
		mapname.text = info.nickname;
		goodText.text = info.goodCount.ToString();
		diffText.text = info.diffCount.ToString();
		passText.text = info.passCount.ToString();
		trysText.text = info.trysCount.ToString();

		this.manager = manager;
	}
}

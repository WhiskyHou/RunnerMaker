using System.Collections.Generic;
using UnityEngine;

public class ObjPool {

	private Queue<GameObject> list;

	private GameObject prefab;

	public ObjPool(GameObject prefab) {
		list = new Queue<GameObject>();
		this.prefab = prefab;
	}

	public void Add(GameObject go) {
		go.SetActive(false);
		list.Enqueue(go);
	}

	public GameObject Get() {
		if (list.Count == 0) {
			return Object.Instantiate(prefab);
		} else {
			return list.Dequeue();
		}
	}
}

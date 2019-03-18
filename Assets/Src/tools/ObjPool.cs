using System.Collections.Generic;
using UnityEngine;

public class ObjPool {

	private Queue<GameObject> list;

	public ObjPool() {
		list = new Queue<GameObject>();
	}

	public void Add(GameObject go) {
		go.SetActive(false);
		list.Enqueue(go);
	}

	public GameObject Get() {
		if (list.Count == 0) {
			return null;
		} else {
			return list.Dequeue();
		}
	}
}

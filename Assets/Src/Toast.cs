using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Toast : MonoBehaviour {

	public Text text;

	public int timeout = 1000;

	void Start() {
		
	}

	void Update() {

	}

	public void Set(string text, int timeout) {
		this.text.text = text;
		this.timeout = timeout;

		Task task = Remove();
	}

	private async Task Remove() {
		await Task.Delay(timeout);
		Destroy(gameObject);
	}
}

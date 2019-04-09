using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RankManager : MonoBehaviour {
	void Start() {

	}

	void Update() {

	}

	public void OnClickBack() {
		SceneManager.LoadScene("HomeScene");
	}
}

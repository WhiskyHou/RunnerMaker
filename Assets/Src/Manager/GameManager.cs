using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Text countDownText;

	private Map map;

	private float startTime;

    void Start() {
		map = GetComponent<MapBuilder>().GetMap();
    }

    void Update() {
		float delta = Time.deltaTime;

		startTime += delta;
		countDownText.text = (map.countDown - (int)Math.Floor(startTime)).ToString() + " s";
    }
}

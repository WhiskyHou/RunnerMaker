using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public int __MAX_FPS;

	private FpsLog fpsLog = new FpsLog();

    void Start() {
		Application.targetFrameRate = __MAX_FPS;    
    }

    void Update() {
		float deltaTime = Time.deltaTime;

		// fpsLog.Update(deltaTime);
    }
}

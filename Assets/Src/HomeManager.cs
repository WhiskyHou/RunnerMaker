using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour {
    void Start() {
		LoadGameScene();
    }

    void Update() {
        
    }

	private async Task LoadGameScene() {
		await Task.Delay(3000);
		//SceneManager.LoadScene("SampleScene");
		Application.LoadLevelAdditive("SampleScene");
		//Scene current = SceneManager.GetSceneByName("HomeScene");
		//GameObject[] objs = current.GetRootGameObjects();
		//for (int i = 0; i < objs.Length; ++i) {
		//	objs[i].SetActive(false);
		//}
		Debug.Log("===== Load to SmpleScene =====");
	}
}

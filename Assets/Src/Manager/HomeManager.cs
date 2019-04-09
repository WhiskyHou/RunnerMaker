using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour {
	void Start() {
		//Task task = LoadGameScene();
	}

	void Update() {

	}

	public void OnClickExit() {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

	public void OnClickLogout() {
		LoginStatus.Instance.Logout();
		SceneManager.LoadScene("SignInScene");
	}

	public void OnClickMyCreation() {
		SceneManager.LoadScene("MyCreationScene");
	}

	public void OnClickMapList() {
		SceneManager.LoadScene("MapListScene");
	}

	public void OnClickRank() {
		SceneManager.LoadScene("RankScene");
	}


	// 测试使用
	private async Task LoadGameScene() {
		await Task.Delay(3000);
		SceneManager.LoadScene("SampleScene");

		//Application.LoadLevelAdditive("SampleScene");

		Scene current = SceneManager.GetSceneByName("SampleScene");
		//GameObject[] objs = current.GetRootGameObjects();
		//for (int i = 0; i < objs.Length; ++i) {
		//	objs[i].SetActive(false);
		//}
		//SceneManager.SetActiveScene(current);
		Debug.Log("===== Load to SmpleScene =====");
	}


}

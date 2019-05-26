using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RankManager : MonoBehaviour {

	public GameObject goodRankRoot;

	public int goodRankRootLength;

	public GameObject createRankRoot;

	public int createRankRootLength;

	private NetHelper netHelper = NetHelper.Instance;

	private GoodRankInfo myGoodRank;

	private CreateRankInfo myCreateRank;

	private List<GoodRankInfo> goodRankList;

	private List<CreateRankInfo> createRankList;

	void Start() {
		GetRemoteGoodRank();
		GetRemoteCreateRank();
	}

	void Update() {

	}

	public void OnClickBack() {
		SceneManager.LoadScene("HomeScene");
	}

	public void GetRemoteGoodRank() {
		netHelper.Post("/getGoodRank", JsonUtility.ToJson(LoginStatus.Instance.GetUser()), (string res) => {
			GetGoodRankResult result = JsonUtility.FromJson<GetGoodRankResult>(res);
			if(result.error == 0) {
				goodRankList = result.list;
				myGoodRank = result.me;
			}
			Debug.Log("==== good rank list");

			BuildGoodRank();
		});
	}

	public void GetRemoteCreateRank() {
		netHelper.Post("/getCreateRank", JsonUtility.ToJson(LoginStatus.Instance.GetUser()), (string res) => {
			GetCreateRankResult result = JsonUtility.FromJson<GetCreateRankResult>(res);
			if (result.error == 0) {
				createRankList = result.list;
				myCreateRank = result.me;
			}
			Debug.Log("==== create rank list");
			Debug.Log(res);

			BuildCreateRank();
		});
	}

	public void BuildGoodRank() {
		for(int i = 0; i < Math.Min(goodRankRootLength, goodRankList.Count); i++) {
			GameObject goodRankItem = Instantiate(Resources.Load("prefab/GoodRank") as GameObject);
			goodRankItem.transform.SetParent(goodRankRoot.transform, true);
			goodRankItem.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, i * -50, 0f);
			goodRankItem.GetComponent<UserGoodRankItem>().Init(goodRankList[i]);
		}

		GameObject goodRankMe = Instantiate(Resources.Load("prefab/GoodRank") as GameObject);
		goodRankMe.transform.SetParent(goodRankRoot.transform, true);
		goodRankMe.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0f);
		goodRankMe.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0f);
		goodRankMe.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, 0f, 0f);
		goodRankMe.GetComponent<UserGoodRankItem>().Init(myGoodRank);
	}

	public void BuildCreateRank() {
		for (int i = 0; i < Math.Min(createRankRootLength, createRankList.Count); i++) {
			GameObject createRankItem = Instantiate(Resources.Load("prefab/CreateRank") as GameObject);
			createRankItem.transform.SetParent(createRankRoot.transform, true);
			createRankItem.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, i * -50, 0f);
			createRankItem.GetComponent<UserCreateRankItem>().Init(createRankList[i]);
		}

		GameObject createRankMe = Instantiate(Resources.Load("prefab/CreateRank") as GameObject);
		createRankMe.transform.SetParent(createRankRoot.transform, true);
		createRankMe.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0f);
		createRankMe.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0f);
		createRankMe.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, 0f, 0f);
		createRankMe.GetComponent<UserCreateRankItem>().Init(myCreateRank);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RankManager : MonoBehaviour {

	public GameObject goodRankRoot;

	public GameObject createRankRoot;

	public NetHelper netHelper;

	private GoodRankInfo myGoodRank;

	private CreateRankInfo myCreateRank;

	private List<GoodRankInfo> goodRankList;

	private List<CreateRankInfo> createRankList;

	void Start() {

	}

	void Update() {

	}

	public void OnClickBack() {
		SceneManager.LoadScene("HomeScene");
	}

	public void GetRemoteGoodRank() {
		string res = netHelper.Post("/getGoodRank", JsonUtility.ToJson(LoginStatus.Instance.GetUser()));
		GetGoodRankResult result = JsonUtility.FromJson<GetGoodRankResult>(res);
		if(result.error == 0) {
			goodRankList = result.list;
			myGoodRank = result.me;
		}
	}

	public void GetRemoteCreateRank() {
		string res = netHelper.Post("/getCreateRank", "{}");
		GetCreateRankResult result = JsonUtility.FromJson<GetCreateRankResult>(res);
		if (result.error == 0) {
			createRankList = result.list;
			myCreateRank = result.me;
		}
	}

	public void BuildGoodRank() {

	}

	public void BuildCreateRank() {

	}
}

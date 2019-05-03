using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GetGoodRankResult {
	public int error;
	public GoodRankInfo me;
	public List<GoodRankInfo> list;
}
[Serializable]
public class GoodRankInfo {
	public int rank;
	public string userNme;
	public int goodCount;
}


[Serializable]
public class GetCreateRankResult {
	public int error;
	public CreateRankInfo me;
	public List<CreateRankInfo> list;
}
[Serializable]
public class CreateRankInfo {
	public int rank;
	public string userName;
	public int createCount;
}
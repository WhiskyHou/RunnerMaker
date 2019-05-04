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
	public int uid;
	public int rank;
	public string nickname;
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
	public int uid;
	public int rank;
	public string nickname;
	public int createCount;
}
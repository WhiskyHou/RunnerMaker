using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User {
	public int uid;
	public string username;
	public string password;
	public string nickname;
}

[Serializable]
public class SignInResult {
	public int error;
	public User data;
}

[Serializable]
public class SignUpResult {
	public int error;
}

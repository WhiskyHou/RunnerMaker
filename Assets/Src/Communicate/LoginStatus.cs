using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginStatus {
	private static LoginStatus instance = null;
	public static LoginStatus Instance {
		get {
			if (instance == null) {
				instance = new LoginStatus();
			}
			return instance;
		}
	}

	private User user = new User();

	public LoginStatus() {
		user.uid = -1;
		user.username = user.password = "";
		user.nickname = "null";
	}

	public void SetUser(User u) {
		user.uid = u.uid;
		user.username = u.username;
		user.password = u.password;
		user.nickname = u.nickname;
	}
}

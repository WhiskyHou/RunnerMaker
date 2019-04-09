using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SignInManager : MonoBehaviour {

	public InputField username_in;
	public InputField password_in;
	public InputField retypePassword_in;
	public InputField nickname_in;

	public GameObject retypePasswordWindow;

	public NetHelper net;

	private string username = "";
	private string password = "";
	private string retypePassword = "";
	private string nickname = "";

	void Start() {
		ResetInput();
	}

	void Update() {

	}

	public void OnClickSignInButton() {
		username = username_in.text;
		password = password_in.text;

		string postData = "{\"username\":\"" + username + "\",\"password\":\"" + password + "\"}";

		string res = net.Post("/signin", postData);
		SignInResult result = JsonUtility.FromJson<SignInResult>(res);

		if (result.error == 0) {
			Debug.Log("=== Sign in ===\n登录成功");

			LoginStatus.Instance.Login(result.data);
			if (!Directory.Exists("Data/" + result.data.username)) {
				Directory.CreateDirectory("Data/" + result.data.username);
			}
			SceneManager.LoadScene("HomeScene");

		} else if (result.error == 1) {
			Debug.Log("=== Sign in ===\n密码错误");
			CreateToast("密码错误");

		} else if (result.error == 2) {
			Debug.Log("=== Sign in ===\n用户名错误");
			CreateToast("用户名不存在");

		}

		ResetInput();
	}

	public void OnClickSignUpButton() {
		username = username_in.text;
		password = password_in.text;

		if (!username.Equals("") && !password.Equals("")) {
			retypePasswordWindow.SetActive(true);
		}
	}

	public void OnClickSignUpOkButton() {
		retypePassword = retypePassword_in.text;
		nickname = nickname_in.text;

		retypePasswordWindow.SetActive(false);

		if (!password.Equals(retypePassword)) {
			Debug.Log("=== Sign Up ===\n两次密码不一致");
			CreateToast("两次输入的密码不一致");

		} else if (nickname.Equals("")) {
			Debug.Log("=== Sign Up ===\n昵称不能为空");
			CreateToast("昵称不能为空");

		} else {
			string postData = "{\"username\":\"" + username + "\",\"password\":\"" + password + "\",\"nickname\":\"" + nickname + "\"}";
			string res = net.Post("/signup", postData);
			SignUpResult result = JsonUtility.FromJson<SignUpResult>(res);
			if (result.error == 0) {
				Debug.Log("=== Sign up ===\n注册成功");

			} else if (result.error == 1) {
				Debug.Log("=== Sign up ===\n用户名已存在");
				CreateToast("用户名已被使用");

			} else if (result.error == 2) {
				Debug.Log("=== Sign up ===\n注册失败");
				CreateToast("注册失败");

			}
		}

		ResetInput();
	}

	private void ResetInput() {
		username = "";
		password = "";
		retypePassword = "";
		nickname = "";

		username_in.text = "";
		password_in.text = "";
		retypePassword_in.text = "";
		nickname_in.text = "";
	}

	private void CreateToast(string message) {
		GameObject toast = Instantiate(Resources.Load("prefab/toast") as GameObject);
		toast.transform.SetParent(GameObject.Find("Canvas").transform, false);
		toast.GetComponent<Toast>().Set(message, 2000);
	}
}

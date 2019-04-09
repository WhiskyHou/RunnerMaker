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
			Debug.Log(result.data);
			LoginStatus.Instance.Login(result.data);
			SceneManager.LoadScene("HomeScene");
		} else if (result.error == 1) {
			Debug.Log("=== Sign in ===\n密码错误");
		} else if (result.error == 2) {
			Debug.Log("=== Sign in ===\n用户名错误");
		}

		ResetInput();
	}

	public void OnClickSignUpButton() {
		username = username_in.text;
		password = password_in.text;

		retypePasswordWindow.SetActive(true);
	}

	public void OnClickSignUpOkButton() {
		retypePassword = retypePassword_in.text;
		nickname = nickname_in.text;

		retypePasswordWindow.SetActive(false);

		if (!password.Equals(retypePassword)) {
			Debug.Log("=== Sign Up ===\n两次密码不一致");
		} else if (nickname.Equals("")) {
			Debug.Log("=== Sign Up ===\n昵称不能为空");
		} else {
			string postData = "{\"username\":\"" + username + "\",\"password\":\"" + password + "\",\"nickname\":\"" + nickname + "\"}";
			string res = net.Post("/signup", postData);
			SignUpResult result = JsonUtility.FromJson<SignUpResult>(res);
			if (result.error == 0) {
				Debug.Log("=== Sign up ===\n注册成功");
			} else if (result.error == 1) {
				Debug.Log("=== Sign up ===\n用户名已存在");
			} else if (result.error == 2) {
				Debug.Log("=== Sign up ===\n注册失败");
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
}

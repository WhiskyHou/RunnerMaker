using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignInManager : MonoBehaviour {

	public InputField username_in;
	public InputField password_in;

    void Start() {

    }

    void Update() {

    }

	public void OnClickSignInButton() {
		string username = username_in.text;
		string password = password_in.text;

		string postData = "{\"username\":\"" + username + "\",\"password\":\"" + password + "\"}";

		NetHelper net = gameObject.GetComponent<NetHelper>();
		string result = net.Post("/signin", postData);
		Debug.Log(result);
	}

	public void OnClickSignUpButton() {

	}
}

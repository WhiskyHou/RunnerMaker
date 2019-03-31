using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateMap : MonoBehaviour {

	public Text nameText;

	public Text widthText;

	public Text heightText;

	public FileHelper fileHelper;

    void Start() {
        
    }

    void Update() {
        
    }

	public void OnClick() {
		Debug.Log("name: " + nameText.text + " width: " + widthText.text + " height: " + heightText.text);

		Map map = new Map {
			nickName = nameText.text,
			createrName = "@houyi",
			width = int.Parse(widthText.text),
			height = int.Parse(heightText.text),
			id = -1,
			countDown = 120
		};

		string path = "Assets/Config/" + nameText.text + ".json";
		fileHelper.WriteToFile(path, JsonUtility.ToJson(map));
	}
}

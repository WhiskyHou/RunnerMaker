using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileHelper : MonoBehaviour {

	private string path = @"Assets/config/outfile.json";

    void Start() {
        
    }

    void Update() {
        
    }

	public void WriteToFile(string data) {
		File.WriteAllText(path, data);
	}
}

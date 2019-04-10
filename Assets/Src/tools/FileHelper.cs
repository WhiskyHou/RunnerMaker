using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileHelper : MonoBehaviour {

	private string path = @"Assets/config/outfile.json";

	private string data = "";

    void Start() {
        
    }

    void Update() {
        
    }

	public void WriteToFile(string path, string data) {
		try {
			File.WriteAllText(path, data, new UTF8Encoding(false));
		} catch (IOException error) {
			Debug.Log("=== FileHelper Error ===\n" + error);
			throw error;
		}
	}

	public void WriteToFileAsync(string path, string data) {
		ThreadStart threadStart = new ThreadStart(() => {
			File.WriteAllText(path, data, new UTF8Encoding(false));
		});
		Thread thread = new Thread(threadStart);
		thread.Start();
	}

	public void RemoveFile(string path) {
		if (File.Exists(path)) {
			File.Delete(path);
		}
	}
}

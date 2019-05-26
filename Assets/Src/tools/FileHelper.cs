using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileHelper {

	private static FileHelper instance = null;
	public static FileHelper Instance {
		get {
			if (instance == null) {
				instance = new FileHelper();
			}
			return instance;
		}
	}

	public string ReadFile(string path) {
		try {
			string result = File.ReadAllText(path);
			return result;
		} catch (IOException error) {
			throw error;
		}
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

	public void RenameFile(string path, string newPath) {
		string data = ReadFile(path);
		File.Delete(path);
		WriteToFile(newPath, data);
	}
}

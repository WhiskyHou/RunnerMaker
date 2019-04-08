using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

public class NetHelper : MonoBehaviour {

	public string host = "http://192.168.1.113";

    void Start() {
        
    }

    void Update() {
        
    }

	public string Post(string url, string json) {
		HttpWebRequest requset = HttpWebRequest.Create(host + url) as HttpWebRequest;
		requset.Method = "POST";
		requset.ContentType = "application/json";
		requset.Timeout = 2000;

		byte[] data = Encoding.UTF8.GetBytes(json);
		requset.ContentLength = data.Length;

		using (Stream reqStream = requset.GetRequestStream()) {
			reqStream.Write(data, 0, data.Length);
			reqStream.Close();
		}

		HttpWebResponse response = requset.GetResponse() as HttpWebResponse;
		Stream stream = response.GetResponseStream();

		string result = "";
		using (StreamReader reader = new StreamReader(stream, Encoding.UTF8)) {
			result = reader.ReadToEnd();
		}

		return result;
	}
}

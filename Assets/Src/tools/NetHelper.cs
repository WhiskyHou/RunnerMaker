using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

public class NetHelper {

	private static NetHelper instance = null;
	public static NetHelper Instance {
		get {
			if (instance == null) {
				instance = new NetHelper();
			}
			return instance;
		}
	}

	public string host = "http://localhost:8686";

	public void SetHost(string ip) {
		host = "http://" + ip + ":8686";
	}

	public void Post(string url, string json, Action<string> callback = null) {
		HttpWebRequest requset = WebRequest.Create(host + url) as HttpWebRequest;
		requset.Method = "POST";
		requset.ContentType = "application/json";
		requset.Timeout = 5000;

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

		if(callback != null) {
			callback(result);
		}
	}
}

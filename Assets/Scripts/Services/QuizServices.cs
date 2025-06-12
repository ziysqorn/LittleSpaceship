using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class QuizServices : MonoBehaviour
{
	private string endpoint = "http://localhost:5000/api/cyber";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
	}

	public void SetAPIUrl(string inUrl)
	{
		endpoint = inUrl;
	}

	public void Get(string url, Action<string> onSuccess, Action<string> onError)
	{
		StartCoroutine(GetRequest(endpoint + url, onSuccess, onError));
	}

	private IEnumerator GetRequest(string url, Action<string> onSuccess, Action<string> onError)
	{
		UnityWebRequest request = UnityWebRequest.Get(url);
		yield return request.SendWebRequest();

		if (request.result != UnityWebRequest.Result.Success)
		{
			onError?.Invoke(request.error);
		}
		else
		{
			onSuccess?.Invoke(request.downloadHandler.text);
		}
	}
}

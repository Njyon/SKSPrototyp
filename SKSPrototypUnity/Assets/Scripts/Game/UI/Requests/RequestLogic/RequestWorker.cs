using System;
using UnityEngine;

[Serializable]
public class RequestWorker : Request
{
	public GameObject WorkerPrefab;

	public override void SendRequest()
	{
		Debug.Log("WorkerRequested");
	}
}

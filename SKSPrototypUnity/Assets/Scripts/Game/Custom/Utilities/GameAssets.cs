using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class GameAssets : MonoBehaviour
{
	private static GameAssets instance;
	public static GameAssets Instance
	{
		get
		{
			if (instance == null)
			{
				//  Path: "Assets/Resources/Prefab/GameAssets"
				instance = (Instantiate(Resources.Load("Prefab/GameAssets")) as GameObject).GetComponent<GameAssets>();
				instance.name = ">> " + instance.name;
			}
			return instance;
		}
	}

	// List of Objects below
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameScene : MonoBehaviour {

	public string GameScene;

	public void LoadGame()
	{
		SceneManager.LoadScene(GameScene);
	}
}

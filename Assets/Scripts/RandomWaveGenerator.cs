using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWaveGenerator : MonoBehaviour {

	public GameObject[] wavesPrefabs;
	public float minWait;
	public float maxWait;

	void Start()
	{
		StartCoroutine (Generate ());
	}

	public IEnumerator Generate()
	{
		while (!GameManager.Instance.GameEnded)
		{
			float waitTime = Random.Range (minWait, maxWait);
			yield return new WaitForSeconds (waitTime);
			int waveId = Random.Range (0, wavesPrefabs.Length);
			GameObject wave = Instantiate (wavesPrefabs [waveId]);
			if (GameManager.Instance != null)
			{
				int colorId = Random.Range (0, GameManager.Instance.inputColors.Length);
				wave.GetComponent<Wave> ().ColorId = colorId;
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DiscoLightChanger : MonoBehaviour {

	public Color[] colors;
	private int index;
	private Light theLight;
	public float duration;
	public float waitTime;

	// Use this for initialization
	void Start () {
		theLight = GetComponent<Light> ();
		StartCoroutine (Change ());	
	}

	IEnumerator Change()
	{
		Tweener t;
		while (true)
		{
			yield return new WaitForSeconds (waitTime);
			theLight.DOColor (colors [index], duration);
			index = (index == colors.Length - 1) ? 0 : index + 1;
		}
	}
}

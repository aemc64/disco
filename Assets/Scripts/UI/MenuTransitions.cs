using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuTransitions : MonoBehaviour {

	private Vector3 originalPos;

	void Start()
	{
		originalPos = transform.localPosition;
		Move();
	}

	public void Move()
	{
		transform.localPosition = originalPos;
		transform.DOLocalMove(new Vector3(0f, 0f, 0f), 1f).SetEase(Ease.OutQuad);
	}
}

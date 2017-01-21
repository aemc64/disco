using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Wave : MonoBehaviour {

	public int damage;
	public float waitTime;

	public Vector3 scaleTarget;
	public float duration;
	public Ease ease;

	private int _colorId;
	private SpriteRenderer _sr;

	public int ColorId
	{
		get { return ColorId; }
		set { _colorId = value; }
	}

	// Use this for initialization
	IEnumerator Start()
	{
		_sr = GetComponent<SpriteRenderer> ();
		_sr.color = GameManager.Instance.inputColors [_colorId].color;
		transform.DOScale (scaleTarget, duration).SetEase (ease);
		yield return new WaitForSeconds (waitTime);
		if (GameManager.Instance != null && !GameManager.Instance.GameEnded)
			GameManager.Instance.CheckInput (_colorId, damage);
		yield return new WaitForSeconds(duration - waitTime);
		Destroy (this.gameObject);
	}
}
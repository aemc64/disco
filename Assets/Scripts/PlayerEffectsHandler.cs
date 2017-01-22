using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerEffectsHandler : MonoBehaviour {

	private static PlayerEffectsHandler _instance;

	public static PlayerEffectsHandler Instance { get { return _instance; } }

	public float messageDuration;
	public Vector3 messageMovement;
	public Ease ease;

	public Transform winLight;

	public GameFeedBack[] goodFeedback;
	public GameFeedBack[] badFeedback;

	private AudioSource effectScr;

	private void Awake()
	{
		if (_instance != null && _instance != this) {
			Destroy (this.gameObject);
		} else {
			_instance = this;
		}
		effectScr = GetComponent<AudioSource> ();
	}
		
	public void PlayRandomGood(Vector3 playerPos)
	{
		int ran = Random.Range (0, goodFeedback.Length);
		StartCoroutine(PlayGood (playerPos, ran));
	}

	public IEnumerator PlayGood(Vector3 playerPos, int id)
	{
		if (!effectScr.isPlaying)
			effectScr.PlayOneShot (goodFeedback[id].clip);
		GameObject obj = new GameObject ();
		obj.transform.position = playerPos;
		SpriteRenderer sp = obj.AddComponent<SpriteRenderer> ();
		sp.sprite = goodFeedback[id].message;
		obj.transform.DOLocalMove ((Vector3)messageMovement, messageDuration).SetEase (ease);
		yield return new WaitForSeconds (messageDuration);
		Destroy (obj);
	}

	public void FinishGame(Vector3 winnerPosition)
	{
		Vector3 targetPos = new Vector3(winnerPosition.x,winnerPosition.y, winLight.position.z);
		winLight.position = targetPos;
		winLight.gameObject.SetActive (true);
	}
}

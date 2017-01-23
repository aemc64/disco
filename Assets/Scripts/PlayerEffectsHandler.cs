using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerEffectsHandler : MonoBehaviour {

	private static PlayerEffectsHandler _instance;

	public static PlayerEffectsHandler Instance { get { return _instance; } }

	public float messageDuration;
	public Vector3 messageMovement;
	public Ease ease;

	public Transform winLight;

	public GameFeedBack[] goodFeedback;
	public GameFeedBack[] badFeedback;
	public Text[] playersScore;
	public GameObject DrawMessage;
	public GameObject WinMessage;
	public AudioClip WinSound;
	public AudioClip DrawSound;

	public UnityEvent onGameFinish;
	public UnityEvent onRestart;

	private AudioSource effectScr;

	private GameObject obj;

	public float endDelay;
	public GameObject getReadyMessage;
	public GameObject danceMessage;
	public AudioClip getReadyclip;
	public AudioClip danceClip;
	public float prepareWait;

	private void Awake()
	{
		if (_instance != null && _instance != this) {
			Destroy (this.gameObject);
		} else {
			_instance = this;
		}
		effectScr = GetComponent<AudioSource> ();
	}

	void Start()
	{
		StartCoroutine (Prepare (false));
	}

	public IEnumerator Prepare(bool replay)
	{
		WinMessage.GetComponent<Animator> ().SetTrigger ("Idle");
		SoundManager.instance.StopMusic ();
		getReadyMessage.SetActive (true);
		effectScr.PlayOneShot (getReadyclip);
		yield return new WaitForSeconds (getReadyclip.length);
		yield return new WaitForSeconds (prepareWait);
		getReadyMessage.SetActive (false);
		danceMessage.SetActive (true);
		effectScr.PlayOneShot (danceClip);
		if (!replay) {
			Manager_container.Instance.cargado = true;
			SoundManager.instance.PlaySelectedSong ();
		} else {
			SoundManager.instance.PlaySelectedSong ();
			Manager_container.Instance.restart = true;
			for (int i = 0; i < playersScore.Length; i++)
				playersScore [i].text = "0";
			WinMessage.GetComponent<Animator> ().SetTrigger ("Idle");
			onRestart.Invoke ();
		}
		yield return new WaitForSeconds (prepareWait);
		danceMessage.SetActive (false);
	}

	public void SetScore(int playerId, int score)
	{
		playersScore [playerId].text = score.ToString ();
	}
		
	public void PlayRandomGood(Vector3 playerPos)
	{
		int ran = Random.Range (0, goodFeedback.Length);
		StartCoroutine(PlayGood (playerPos, ran));
	}

	public IEnumerator PlayGood(Vector3 playerPos, int id)
	{
		if (!effectScr.isPlaying && obj == null)
		{
			effectScr.PlayOneShot (goodFeedback[id].clip);
			obj = new GameObject ();
			obj.transform.position = playerPos;
			SpriteRenderer sp = obj.AddComponent<SpriteRenderer> ();
			sp.sprite = goodFeedback[id].message;
			obj.transform.DOLocalMove ((Vector3)messageMovement, messageDuration).SetEase (ease);
			yield return new WaitForSeconds (messageDuration);
			Destroy (obj);
		}

	}

	public void Win(Vector3 winnerPosition, int winnerNumber)
	{
		StartCoroutine (WinCo (winnerPosition, winnerNumber));
	}

	public void Draw()
	{
		StartCoroutine (DrawCo ());
	}

	public void Replay()
	{
		StartCoroutine (Prepare (true));
	}

	IEnumerator WinCo(Vector3 winnerPosition, int winnerNumber)
	{
		yield return new WaitForSeconds (endDelay);
		Vector3 targetPos = new Vector3(winnerPosition.x,winnerPosition.y, winLight.position.z);
		winLight.position = targetPos;
		winLight.gameObject.SetActive (true);
		WinMessage.GetComponent<Animator> ().SetTrigger ("win" + winnerNumber.ToString ());
		if (WinSound != null)
			effectScr.PlayOneShot (WinSound);
		onGameFinish.Invoke ();
	}

	IEnumerator DrawCo()
	{
		yield return new WaitForSeconds (endDelay);
		DrawMessage.SetActive (true);
		if (DrawSound != null)
			effectScr.PlayOneShot (DrawSound);
		onGameFinish.Invoke ();
	}
}

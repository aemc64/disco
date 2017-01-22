using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundManager : MonoBehaviour
{
	public AudioClip[] musicSource;                 //Drag a reference to the audio source which will play the music.
    public Sprite[] backgrounds;                    //Array of possible backgrounds to choose from
    public GameObject background;                   //The actual background that masks the disk
	public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager. 
	AudioSource As;
	/*public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
	public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.
	
		Let's not use thise for now - Zuil*/


	void Awake()
	{
		//Check if there is already an instance of SoundManager
		if (instance == null)
			//if not, set it to this.
			instance = this;
		//If instance already exists:
		else if (instance != this)
			//FadeOut, this enforces our singleton pattern so there can only be one instance of SoundManager.
			
		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad(gameObject);
		As = gameObject.GetComponent<AudioSource>();
		PlayWithFadeOut(Random.Range(0,4));
	}

	public void PlayWithFadeOutRandom()
	{
		StartCoroutine(PlayClip(Random.Range(0, 4)));
	}

	public void PlayWithFadeOut(int clip)
	{
		StartCoroutine(PlayClip(clip));
	}
	
	//This is used for skipping songs
	private int cont = 6;
	void OnGUI()
	{
		if (GUILayout.Button("Previous"))
		{		
			cont--;
			if (cont < 6) { cont = 9; }
            ChangeBackground(cont);
			PlayWithFadeOut(cont);
		}

		if (GUILayout.Button("Next"))
		{
			cont++;
			if (cont > 9) { cont = 6; }
            ChangeBackground(cont);
			PlayWithFadeOut(cont);
		}
	}

	//Used to play single sound clips. In this case, we want the game song, menu or songlist.
	public IEnumerator PlaySingle(int clip)
	{
		//Set the clip of our musicSource audio source to the clip passed in as a parameter.	
		As.clip = musicSource[clip];
		//Play the clip.
		As.Play();
		yield return null;
	}
		
	public 
	IEnumerator PlayClip(int clip)
	{
		if (As.clip != null)
		{
			yield return StartCoroutine(AudioFadeOut.FadeOut(As, 0.75f));
		}
		yield return StartCoroutine(PlaySingle(clip));
	}

    public void ChangeBackground(int cont) {
        background.GetComponent<Image>().sprite = backgrounds[cont - 6];
    }
}
public static class AudioFadeOut
{

	public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
	{
		float startVolume = audioSource.volume;

		while (audioSource.volume > 0)
		{
			audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
			yield return null;
			
		}

		audioSource.Stop();
		audioSource.volume = startVolume;
		
	}

}
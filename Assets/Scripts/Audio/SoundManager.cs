using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundManager : MonoBehaviour
{
	public AudioClip[] musicSource; //Drag a reference to the audio source which will play the music.
	public AudioClip[] sfxSource; //Drag a reference to the sfx source which will play the music.
	public Sprite[] backgrounds;                    //Array of possible backgrounds to choose from
    public GameObject background;                   //The actual background that masks the disk
	public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager. 
	public AudioSource As;
	public AudioSource sfx;
	public GameObject Titulo;
	public GameObject Artista;
	private int cont = 6;
	/*public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
	public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.
	
		Let's not use thise for now - Zuil*/

	void Awake()
	{
		//Check if there is already an instance of SoundManager
		if (instance == null) {
			//if not, set it to this.
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
		//If instance already exists:
		else if (instance != this) {
			Destroy (gameObject);
		}
			
		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		//FadeOut, this enforces our singleton pattern so there can only be one instance of SoundManager.
		PlayWithFadeOut(Random.Range(0,4));
	}

	public void PlaySelectedSong()
	{
		PlayClip (cont);
	}

	public void PlayWithFadeOutRandom()
	{
		StartCoroutine(PlayClip(Random.Range(0, 4)));
	}

	public void PlaySFX(int clip)
	{
		sfx.clip = sfxSource[clip];
		sfx.Play();
	}

	public void PlayWithFadeOut(int clip)
	{
		StartCoroutine(PlayClip(clip));
	}
	
	//This is used for skipping songs
	
	void OnGUI()
	{
		if (GUILayout.Button("Previous"))
		{		
			cont--;
			if (cont < 6) { cont = 9; }
            ChangeBackground(cont);
			PlayWithFadeOut(cont);
			SongName(cont);
		}

		if (GUILayout.Button("Next"))
		{
			cont++;
			if (cont > 9) { cont = 6; }
            ChangeBackground(cont);
			PlayWithFadeOut(cont);
			SongName(cont);
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

	public void SongName(int cont)
	{
		string song = "";
		string artist = "";
		switch (cont)
		{
			case 6:
				song = "80's Styke Loop";
				artist = "Bones 341";
				break;
			case 7:
				song = "Waveform";
				artist = "MusicByReo";
				break;
			case 8:
				song = "Vortex";
				artist = "PizzaKiller";
				break;
			case 9:
				song = "Sounding Wave";
				artist = "DUVESTECH";
				break;
			default:
				song = "Choose Song";
				artist = "Artist";
				break;
		}
		Text[] songTexts = Titulo.GetComponentsInChildren<Text>();
		for (int i = 0; i < songTexts.Length; i++)
			songTexts[i].text = song;
		Text[] artistTexts = Artista.GetComponentsInChildren<Text>();
		for (int i = 0; i < artistTexts.Length; i++)
			artistTexts[i].text = artist;
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
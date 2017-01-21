using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
	public AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
	public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
	public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             
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
			//Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
			Destroy(gameObject);

		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad(gameObject);
	}


	//Used to play single sound clips. In this case, we want the game song, menu or songlist.
	public void PlaySingle(AudioClip clip)
	{
		//Set the clip of our musicSource audio source to the clip passed in as a parameter.
		musicSource.clip = clip;

		//Play the clip.
		musicSource.Play();
	}
}
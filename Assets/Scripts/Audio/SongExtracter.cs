using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class SongExtracter : MonoBehaviour
{
	public enum SeekDirection { Forward, Backward }

	public AudioSource source;
	public List<AudioClip> clips = new List<AudioClip>();

	[SerializeField]
	[HideInInspector]
	private int currentIndex = 0;

	private FileInfo[] soundFiles;
	private List<string> validExtensions = new List<string> { ".mp3", ".wav" };
	private string absolutePath = "./"; // relative path to where the app is running - change this to "./music" in your case

	void Start()
	{
		//being able to test in unity
		if (Application.isEditor) absolutePath = "Assets/";

		if (source == null) source = gameObject.AddComponent<AudioSource>();

	}

	void OnGUI()
	{
		if (GUILayout.Button("Previous"))
		{
			Seek(SeekDirection.Backward);
			PlayCurrent();
		}

		if (GUILayout.Button("Next"))
		{
			Seek(SeekDirection.Forward);
			PlayCurrent();
		}
	}

	void Seek(SeekDirection d)
	{
		if (d == SeekDirection.Forward)
			currentIndex = (currentIndex + 1) % clips.Count;
		else {
			currentIndex--;
			if (currentIndex < 0) currentIndex = clips.Count - 1;
		}
	}

	void PlayCurrent()
	{
		source.clip = clips[currentIndex];
		source.Play();
	}


	bool IsValidFileType(string fileName)
	{
		return validExtensions.Contains(Path.GetExtension(fileName));
		// Alternatively, you could go fileName.SubString(fileName.LastIndexOf('.') + 1); that way you don't need the '.' when you add your extensions
	}

	IEnumerator LoadFile(string path)
	{
		WWW www = new WWW("file://" + path);
		print("loading " + path);

		AudioClip clip = www.GetAudioClip(false);
		while (!clip.isReadyToPlay)
			yield return www;

		print("done loading");
		clip.name = Path.GetFileName(path);
		clips.Add(clip);
	}
}
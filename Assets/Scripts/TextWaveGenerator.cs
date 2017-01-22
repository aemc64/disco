using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextWaveGenerator : WaveGenerator {

	private List<float> waves;
	private float startTime;
	private int index;
	private readonly float threshold = 0.5f;

	public TextWaveGenerator(Vector3 waveOrigin, GameObject wavePrefab, Color[] colors, float[] wave_grow_rate, float[] wave_active_time, string path) : base(waveOrigin, wavePrefab, colors, wave_grow_rate, wave_active_time) 
	{
		TextAsset ta = (TextAsset)Resources.Load (path);
		string[] timeline = ta.text.Split (new string[] {"\n"}, System.StringSplitOptions.None);
		waves = new List<float> ();
		for (int i = 0; i < timeline.Length; i++)
			waves.Add (float.Parse (timeline [i]));
		startTime = Time.time;
		index = 0;
	}

	public override Wave update()
	{
		float currentTime = Time.time - startTime;
		if ((currentTime >= waves [index] - threshold) && (currentTime <= waves [index] + threshold))
		{
			int value = Random.Range(0, 4);
			index++;
			return createWave(value);
		}
		return null;
	}
}

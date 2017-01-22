using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator {

    private Vector3 waveOrigin;
    private GameObject wavePrefab;
    private Color[] colors;
	private float[] wave_grow_rate;
	private float[] wave_active_time;

	public WaveGenerator(Vector3 waveOrigin, GameObject wavePrefab, Color[] colors, float[] wave_grow_rate, float[] wave_active_time)
    {
        this.waveOrigin = waveOrigin;
        this.wavePrefab = wavePrefab;
        this.colors = colors;
		this.wave_grow_rate = wave_grow_rate;
		this.wave_active_time = wave_active_time;
    }

    protected Wave createWave(int value)
    {
		int random = Random.Range (0, wave_active_time.Length);
		Wave newWave = new Wave(value, wavePrefab, colors[value],waveOrigin, wave_grow_rate[random], wave_active_time[random]);
        return newWave;
    }

    public virtual Wave update()
    {
        return null;
    }
}

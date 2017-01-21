using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator {

    private Vector3 waveOrigin;
    private GameObject wavePrefab;
    private Color[] colors;

    public WaveGenerator(Vector3 waveOrigin, GameObject wavePrefab, Color[] colors)
    {
        this.waveOrigin = waveOrigin;
        this.wavePrefab = wavePrefab;
        this.colors = colors;
    }

    protected Wave createWave(int value)
    {
        Wave newWave = new Wave(value, wavePrefab, colors[value],waveOrigin);
        return newWave;
    }

    public virtual Wave update()
    {
        return null;
    }
}

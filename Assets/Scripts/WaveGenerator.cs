using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator {

    private Vector3 waveOrigin;
    private GameObject wavePrefab;

    public WaveGenerator(Vector3 waveOrigin, GameObject wavePrefab)
    {
        this.waveOrigin = waveOrigin;
        this.wavePrefab = wavePrefab;
    }

    private Wave createWave()
    {
        //Wave newWave = new Wave();
        //newWave.transform.position = waveOrigin;
        //newWave.transform.localScale = Vector3.zero;
        return null;
    }

    public Wave Update()
    {

        return null;
    }
}

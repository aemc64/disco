using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWaveGenerator : WaveGenerator{

	public float minWait = 1f;
	public float maxWait = 3f;
    //Num between [0, 100]
    public int frequency = 50;

    private float lastBornTime;

    public RandomWaveGenerator(Vector3 waveOrigin, GameObject wavePrefab, Color[] colors) : base(waveOrigin, wavePrefab, colors) { }

    public void setValues(float minWait, float maxWait, int frequency)
    {
        this.minWait = minWait;
        this.maxWait = maxWait;
        this.frequency = frequency;
    }

    public override Wave update()
    {
        if ((Time.time - lastBornTime > minWait) || (Time.time - lastBornTime > maxWait))
        {
            if (frequency > Random.Range(0, 100))
            {
                lastBornTime = Time.time;
                int value = Random.Range(0, 4);
                return createWave(value);
            }
        }

        return null;
    }

}

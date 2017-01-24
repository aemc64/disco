using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWaveGenerator : WaveGenerator{

	public float minWait = 1f;
	public float maxWait = 3f;
    //Num between [0, 100]
    public int frequency = 50;

    private float lastBornTime;
	private float changer = 0.8f;
	private int everyWave = 10;
	private int waves = 0;

	public RandomWaveGenerator(Vector3 waveOrigin, GameObject wavePrefab, Color[] colors, float[] wave_grow_rate, float[] wave_active_time) : base(waveOrigin, wavePrefab, colors, wave_grow_rate, wave_active_time) { }

    public void Reset()
    {
        this.minWait = 1f;
        this.maxWait = 3f;
    }

    public override Wave update()
    {
		if (waves == everyWave)
		{
			minWait *= changer;
			maxWait *= changer;
			waves = 0;
		}
        if ((Time.time - lastBornTime > minWait) || (Time.time - lastBornTime > maxWait))
        {
            if (frequency > Random.Range(0, 100))
            {
                lastBornTime = Time.time;
                int value = Random.Range(0, 4);
				waves++;
				return createWave(value);

            }
        }
        return null;
    }

}

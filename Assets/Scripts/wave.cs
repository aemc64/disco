using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave {
	public float create_time;
	public int correctVal;
	public GameObject displayer;
	private float wave_grow_rate;
	private float wave_active_time;

	public float WaveActiveTime
	{
		get { return wave_active_time; }
		set { wave_active_time = value; }
	}

    // Constructor
	public Wave(int valor, GameObject wavePrefab, Color color, Vector3 initPos, float wave_grow_rate, float wave_active_time){
		create_time = Time.time;
		correctVal = valor;
        displayer = GameObject.Instantiate(wavePrefab);
        displayer.transform.position = initPos;
        SpriteRenderer renderer = displayer.GetComponent<SpriteRenderer>();
		renderer.color = color;
		this.wave_active_time = wave_active_time;
		this.wave_grow_rate = wave_grow_rate;
	}

	public void clean(){
		GameObject.Destroy (displayer);
	}

	public void update(){
		float grow = wave_grow_rate * Time.deltaTime;
		displayer.transform.localScale += new Vector3 (grow,grow,0);
	}
}

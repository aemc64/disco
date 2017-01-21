using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave {
	public float create_time;
	public int correctVal;
	private GameObject displayer;

    // Constructor
	public Wave(int valor, GameObject wavePrefab, Color color, Vector3 initPos){
		create_time = Time.time;
		correctVal = valor;
        displayer = GameObject.Instantiate(wavePrefab);
        displayer.transform.position = initPos;
        SpriteRenderer renderer = displayer.GetComponent<SpriteRenderer>();
		renderer.color = color;
	}

	public void clean(){
		GameObject.Destroy (displayer);
	}

	public void update(float wave_grow){
		//displayer.transform.localScale += new Vector3 (wave_grow,wave_grow,0);
	}
}

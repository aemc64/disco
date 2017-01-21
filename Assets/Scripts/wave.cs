using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave {
	public float create_time;
	public int correctVal;
	private GameObject displayer;

	public Wave(int valor, Sprite s){
		create_time = Time.time;
		correctVal = valor;

		displayer = new GameObject ();
		SpriteRenderer renderer = displayer.AddComponent<SpriteRenderer> ();
		renderer.sprite = s;
		displayer.transform.localScale = new Vector3 (0, 0, 1);
	}

	public void clean(){
		GameObject.Destroy (displayer);
	}

	public void update(float wave_grow){
		displayer.transform.localScale += new Vector3 (wave_grow,wave_grow,0);
	}
}

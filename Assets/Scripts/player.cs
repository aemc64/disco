using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
	public int vida;
	private int last_key_pressed;
	private float timeAtLastKeyPressed;
	private GameObject displayer;

	public Player(int vida_in, GameObject playerPrefab, Vector3 initPos){
		vida = vida_in;
		timeAtLastKeyPressed = 0;
		last_key_pressed = 1000;
		displayer = GameObject.Instantiate (playerPrefab);
		displayer.transform.position = initPos;
	}

	public void key_press(int i){
		last_key_pressed = i;
		timeAtLastKeyPressed = Time.time;
	}

	public void check_correct_press(int correctVal, float error){
		if (correctVal == last_key_pressed) {
			if ((timeAtLastKeyPressed + error) < Time.time || (timeAtLastKeyPressed - error) > Time.time) {
				vida -= 1;
				Damage ();
			} else {
				Debug.Log ("Correcto");
			}
		} else {
			vida -= 1;
			Damage ();
		}
	}

	public void printVida(){
		Debug.Log (vida.ToString());
	}

	public void Damage()
	{
		displayer.transform.position += new Vector3 (-0.5f, 0f, 0f);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Player {
	public int vida;
	private int last_key_pressed;
	private float timeAtLastKeyPressed;
	public GameObject displayer;
	private Vector2 direction;
	private float movement;
	private Animator anim;
	public int score;
	public int id;

	private uint lastcorrectwave;

	public void clean(){
		GameObject.Destroy (displayer);
	}

	public Player(int vida_in, GameObject playerPrefab, Vector3 initPos, Vector2 direction, float movement, int id){
		vida = vida_in;
		timeAtLastKeyPressed = 0;
		last_key_pressed = 1000;
		this.direction = direction;
		this.movement = movement;
		displayer = GameObject.Instantiate (playerPrefab);
		anim = displayer.GetComponent<Animator> ();
		displayer.transform.position = initPos;
		this.movement = movement / vida_in;
		lastcorrectwave = 0;
		score = 0;
		this.id = id;
	}

	public void key_press(int i){
		last_key_pressed = i;
		timeAtLastKeyPressed = Time.time;
	}

	public void check_correct_press(int correctVal, uint waveid)
	{
		if (lastcorrectwave != waveid) {
			if (correctVal == last_key_pressed && Time.time == timeAtLastKeyPressed) {
				Debug.Log ("Correcto");
				score++;
				lastcorrectwave = waveid;
				PlayerEffectsHandler.Instance.SetScore (id, score);
				PlayerEffectsHandler.Instance.PlayRandomGood (displayer.transform.position); 
			}
		}
	}

	public void waveEnd(uint waveid){
		if (lastcorrectwave != waveid) {
			vida -= 1;
			Damage ();
		}
	}

	public void printVida(){
		Debug.Log (vida.ToString());
	}

	public void Damage()
	{
		Vector3 target = displayer.transform.position + new Vector3 (movement * direction.x, movement * direction.y, 0f);
		displayer.transform.DOLocalMove (target, 1f).SetEase (Ease.OutQuad);
		anim.SetBool ("SimpleDance", false);
		anim.SetTrigger ("Hurt");
		if (vida == 0)
			anim.SetTrigger ("Death");
		else
			anim.SetBool ("SimpleDance", true);
	}

	public void Celebrate()
	{
		anim.SetBool ("SimpleDance", false);
		anim.SetBool ("Dance", true);
	}
}

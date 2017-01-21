﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Manager{
	private int max_waves;
	private int players_vida;
	private int max_players;
	private float wave_life_time;
	private float wave_active_time; //tiempo en el cual la wave activa se evalua
	private float player_input_error;
	private Sprite[] wave_sprites;

	private player[] players;
	private wave[] waves;
	private int next;
	private int actual;
	private int ultimo;
	private float wave_grow_rate;

	public Manager(int o,int j,int k,float l,float m,float n,Sprite[] sprites){
		max_waves = o;
		players_vida = j;
		max_players = k;
		wave_life_time = l;
		wave_active_time = m;
		player_input_error = n;
		wave_sprites = sprites;

		actual = 0;
		next = 0;
		ultimo = 0;
		wave_grow_rate =0.5f;

		players = new player[max_players];
		for (int i = 0;i<max_players;i++){
			players[i]=new player(players_vida);
		}

		waves = new wave[max_waves];
	}

	public void crea_wave(){
		int correctVal = Random.Range (1, 4);
		waves[next] = new wave(correctVal,wave_sprites [correctVal - 1]);
		next = (next + 1) %  max_waves;
	}

	public void input_received(int player_num, int val){
		if (player_num < max_players)
			players [player_num].key_press (val);
	}

	public void on_update(){

		if (waves [ultimo] != null) {
			int i = ultimo;
			while (waves [i].create_time + wave_life_time <= Time.time) {
				waves [i].clean ();
				waves [i] = null;

				i = (i + 1) % max_waves;
				ultimo = (ultimo + 1) % max_waves;
				if (waves [i] == null)
					break;
			}

			while (i != next) {
				waves [i].update (wave_grow_rate * Time.deltaTime);
				i = (i + 1) % max_waves;
			}
				
			if (waves [actual] != null) {
				if (Time.time >= waves [actual].create_time + wave_active_time) {
					foreach (player p in players) {
						p.check_correct_press (waves [actual].correctVal, player_input_error);
					}
					actual = (actual + 1) % max_waves;
				}
			}

		}

	}

}
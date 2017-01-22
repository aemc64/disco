using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Manager{
	private int max_waves;
	private int players_vida;
	private int max_players;
	private float wave_life_time;
	private float[] wave_active_time; //tiempo en el cual la wave activa se evalua
	private float player_input_error;

	private Player[] players;
	private Wave[] waves;
	private int next;
	private int actual;
	private int ultimo;
	private float[] wave_grow_rate;

    private WaveGenerator wg;
	private bool gameEnded;

	public bool b = true;
	public GameObject d;

	public bool GameEnded
	{
		get { return gameEnded; }
		set { gameEnded = value; }
	}

	public Manager(int o,int j,int k,float l,float[] m,float n, Color[] colors,
		GameObject wavePrefab, GameObject[] playerPrefabs, int[] charactersId, 
		float[] wave_grow_rate, Vector3[] playersDefaultPosition, Vector2[] playersDirections, float playersMovement){
		max_waves = o;
		players_vida = j;
		max_players = k;
		wave_life_time = l;
		player_input_error = n;
    
		wg = new RandomWaveGenerator(Vector3.zero, wavePrefab, colors, wave_grow_rate, m);
		//wg = new TextWaveGenerator(Vector3.zero, wavePrefab, colors, wave_grow_rate, m, "Vortex");

		actual = 0;
		next = 0;
		ultimo = 0;

		players = new Player[max_players];
		for (int i = 0;i<max_players;i++){
			players[i]=new Player(players_vida,playerPrefabs[charactersId[i]], playersDefaultPosition[i], playersDirections[i], playersMovement);
		}
		waves = new Wave[max_waves];
	}

	public void waveGeneration(){
        Wave w = wg.update();
		//Debug.Log ("pass");
        if (w != null)
        {
            waves[next] = w;
            next = (next + 1) % max_waves;
        }

	}

	public void input_received(int player_num, int val){
		if (player_num < max_players)
			players [player_num].key_press (val);
	}

	public void on_update(){

        waveGeneration();

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
				waves [i].update ();
				i = (i + 1) % max_waves;
			}
				
			if (waves [actual] != null) {
				if (Time.time >= waves [actual].create_time + waves[actual].WaveActiveTime) {
					if (b) {
						if (d != null)
							GameObject.Destroy (d);
						d = GameObject.Instantiate (waves [actual].displayer);
						d.GetComponent<Animator> ().Stop ();
					}
					List<Player> playersAlive = new List<Player>();
					foreach (Player p in players) {
						if (p.vida > 0)
						{
							float targetTime = waves [actual].create_time + waves [actual].WaveActiveTime;
							p.check_correct_press (waves [actual].correctVal, player_input_error, targetTime);
						}
						if (p.vida > 0)
							playersAlive.Add (p);
					}
					if (playersAlive.Count < 2)
						gameEnded = true;
					actual = (actual + 1) % max_waves;
				}
			}
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Manager{
	private int max_waves;
	private int players_vida;
	private int max_players;
	private float wave_life_time;
	private float[] wave_active_time; //tiempo en el cual la wave activa se evalua
	private float player_input_error;

	public Player[] players;
	private Wave[] waves;
	private int next;
	private int actual;
	private int ultimo;
	private float[] wave_grow_rate;

    private RandomWaveGenerator wg;
	private bool gameEnded;

	private bool cloneWave;
	private GameObject d;

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
			players[i]=new Player(players_vida,playerPrefabs[charactersId[i]], playersDefaultPosition[i], playersDirections[i], playersMovement,i);
		}
		waves = new Wave[max_waves];
		cloneWave = true;
	}

	public void reset(int o,int j,int k,float l,float[] m,float n, Color[] colors,
		GameObject wavePrefab, GameObject[] playerPrefabs, int[] charactersId, 
		float[] wave_grow_rate, Vector3[] playersDefaultPosition, Vector2[] playersDirections, float playersMovement){
		actual = 0;
		next = 0;
		ultimo = 0;

		if (d != null)
			GameObject.Destroy (d);
		for (int i = 0; i < max_waves; i++) {
			if (waves [i] != null) {
				waves [i].clean ();
				waves [i] = null;
			}
		}
		cloneWave = true;
		for (int i = 0;i<max_players;i++){
			players [i].clean ();
			players[i]=new Player(players_vida,playerPrefabs[charactersId[i]], playersDefaultPosition[i], playersDirections[i], playersMovement,i);
		}

		wg.Reset ();
			
		//wg = new RandomWaveGenerator(Vector3.zero, wavePrefab, colors, wave_grow_rate, m);
		GameEnded = false;
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
				float actionTime = waves [actual].create_time + waves [actual].WaveActiveTime;
				if (Time.time > actionTime + player_input_error) {

					List<Player> playersAlive = new List<Player> ();
					foreach (Player p in players) {
						if (p.vida > 0) {
							p.waveEnd (waves [actual].id);
						}
						if (p.vida > 0)
							playersAlive.Add (p);
					}
					if ((playersAlive.Count < 2 && max_players > 1) || (playersAlive.Count < 1 && max_players == 1)) {
						gameEnded = true;
						if (playersAlive.Count == 1) {
							playersAlive [0].Celebrate ();
							PlayerEffectsHandler.Instance.Win (playersAlive [0].displayer.transform.position, playersAlive[0].id + 1);
						} else {
							PlayerEffectsHandler.Instance.Draw ();
						}
					}

					actual = (actual + 1) % max_waves;
					cloneWave = true;
				} else {
					Debug.Log (waves [actual].id.ToString() +" "+cloneWave.ToString());
					if (cloneWave && Time.time >= actionTime) {
						if (d != null)
							GameObject.Destroy (d);
						d = GameObject.Instantiate (waves [actual].displayer);
						d.GetComponent<Animator> ().Stop ();
						cloneWave = false;
					}
					if (Time.time <= actionTime + player_input_error && Time.time >= actionTime - player_input_error) {
						foreach (Player p in players) p.check_correct_press (waves [actual].correctVal, waves [actual].id);
					}
				}
			}
		}
	}
}
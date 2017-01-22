using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_container : MonoBehaviour {

	private static Manager_container _instance;

	public static Manager_container Instance { get { return _instance; } }

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		} else {
			_instance = this;
		}
		DontDestroyOnLoad(gameObject);
	}

	public int max_waves;
	public int players_vida;
	public int max_players;
	public float wave_life_time;
	public float[] wave_active_time;
	public float[] wave_grow_rate;
	public float player_input_error;
    public Color[] colors;
    public GameObject wavePrefab;
	public string[] players;

	public Vector3[] playersDefaultPosition;
	public Vector2[] playersDirections;
	public float playersMovement;
	public GameObject[] playersPrefabs;
	public int[] charactersId;

	public bool cargado; //boleano para cambiar la funcion a correr en el update
	private delegate void delegado(); // crea el tipo apuntador a funcion que retorna void y no recibe argumentos
	private delegado d; //una variable de tipo apuntador a funcion que retorna void y no recibe argumentos

	private Manager m;
	public bool restart;

	// Use this for initialization
	void Start () {
		d = new delegado(nullFun);
	}

	private void nullFun(){
		//Debug.Log ("No cargado");
		if (cargado) {
			m = new Manager (max_waves,players_vida,max_players,wave_life_time,wave_active_time, 
				player_input_error, colors, wavePrefab, playersPrefabs, charactersId, wave_grow_rate, 
				playersDefaultPosition, playersDirections, playersMovement);
			d = new delegado(myupdate);
			cargado = false;
		}
	}

	private void waitForRestart(){
		//Debug.Log ("No cargado");
		if (restart) {
			m.reset (max_waves,players_vida,max_players,wave_life_time,wave_active_time, 
				player_input_error, colors, wavePrefab, playersPrefabs, charactersId, wave_grow_rate, 
				playersDefaultPosition, playersDirections, playersMovement);
			//m = new Manager (max_waves,players_vida,max_players,wave_life_time,wave_active_time, 
			//	player_input_error, colors, wavePrefab, playersPrefabs, charactersId, wave_grow_rate, 
			//	playersDefaultPosition, playersDirections, playersMovement);
			d = new delegado(myupdate);
			restart = false;
		}
	}

	private void myupdate(){
		if (m.GameEnded) {
			d = new delegado (waitForRestart);
			restart = false;
		} else {
			for(int i = 0; i<4; i++) { 
				if (players[i] == "T") {
					if (Input.GetKey("k"))
						m.input_received(i, 0);
					if (Input.GetKey("o"))
						m.input_received(i, 1);
					if (Input.GetKey("l"))
						m.input_received(i, 2);
					if (Input.GetKey("p"))
						m.input_received(i, 3);
				} else {
					if (Input.GetButton ("A_" + players [i])) 
						m.input_received (i, 0);
					if (Input.GetButton("B_" + players[i]))
						m.input_received(i, 1);
					if (Input.GetButton("X_" + players[i]))
						m.input_received(i, 2);
					if (Input.GetButton("Y_" + players[i]))
						m.input_received(i, 3);
				}
			}
			m.on_update ();
		}
	}

    void Update () {
		d ();
    }

	public void Restart()
	{
		cargado = true;
	}
}

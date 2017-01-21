using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_container : MonoBehaviour {
	public int max_waves;
	public int players_vida;
	public int max_players;
	public float wave_life_time;
	public float wave_active_time;
	public float player_input_error;

	private float last_wave;

	private Manager m;
	// Use this for initialization
	void Start () {
		m = new Manager (max_waves,players_vida,max_players,wave_life_time,wave_active_time,player_input_error);

		last_wave = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("z"))
			m.input_received (0, 1);
		if (Input.GetKey ("x"))
			m.input_received (0, 2);
		if (Input.GetKey ("c"))
			m.input_received (0, 3);
		if (Input.GetKey ("v"))
			m.input_received (0, 4);

		if (Input.GetKey ("a"))
			m.input_received (1, 1);
		if (Input.GetKey ("s"))
			m.input_received (1, 2);
		if (Input.GetKey ("d"))
			m.input_received (1, 3);
		if (Input.GetKey ("f"))
			m.input_received (1, 4);

		if (Input.GetKey ("q"))
			m.input_received (2, 1);
		if (Input.GetKey ("w"))
			m.input_received (2, 2);
		if (Input.GetKey ("e"))
			m.input_received (2, 3);
		if (Input.GetKey ("r"))
			m.input_received (2, 4);

		if (Input.GetKey ("1"))
			m.input_received (3, 1);
		if (Input.GetKey ("2"))
			m.input_received (3, 2);
		if (Input.GetKey ("3"))
			m.input_received (3, 3);
		if (Input.GetKey ("4"))
			m.input_received (3, 4);

		if (last_wave + 6 < Time.time) {
			Debug.Log ("Creada wave");
			m.crea_wave ();
			last_wave = Time.time;
		}

		m.on_update ();
	}
}

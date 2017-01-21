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
	public float wave_grow_rate;
	public int players_vida;
	public int max_players;
	public float wave_life_time;
	public float wave_active_time;
	public float player_input_error;
    public Color[] colors;
    public GameObject wavePrefab;
	public GameObject[] playersPrefabs;
	public int[] charactersId;
	public string[] players;
	public Vector3[] playersDefaultPosition;
	public Vector2[] playersDirections;
	public float playersMovement;

    private float last_wave;
	private float currentWait;
	private Manager m;
	// Use this for initialization
	void Start () {
		m = new Manager (max_waves,players_vida,max_players,wave_life_time,wave_active_time, 
			player_input_error, colors, wavePrefab, playersPrefabs, charactersId, wave_grow_rate, 
			playersDefaultPosition, playersDirections, playersMovement);
		last_wave = Time.time;
	}

	// Update is called once per frame
	/// <summary>
    /// 
    /// </summary>
    void Update () {
		
		for (int i = 0; i < 4; i++) { 
			if (players [i] == "T") {
				if (Input.GetKey ("k"))
					m.input_received (i, 1);
				if (Input.GetKey ("o"))
					m.input_received (i, 2);
				if (Input.GetKey ("l"))
					m.input_received (i, 3);
				if (Input.GetKey ("p"))
					m.input_received (i, 4);
			} else {
				if (Input.GetButton ("A_" + players [i]))
					m.input_received (i, 1);
				if (Input.GetButton ("B_" + players [i]))
					m.input_received (i, 2);
				if (Input.GetButton ("X_" + players [i]))
					m.input_received (i, 3);
				if (Input.GetButton ("Y_" + players [i]))
					m.input_received (i, 4);
			}
		}

		m.on_update ();
    }
}

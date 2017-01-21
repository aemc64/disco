using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public const string PLAYERINPUT = "_P";
	private readonly string[] AXESNAMES = { "LHorizontal", "LVertical" };
	private readonly string[] BUTTONSNAMES = { "A", "B", "X", "Y" };

	public int playersNumber;
	public int playersLife;
	public float inputTime;
	public InputColor[] inputColors;

	private static GameManager _instance;

	private Player[] _players;
	private bool _checkTimeEnded;
	private bool _gameEnded;

	public static GameManager Instance { get { return _instance; } }

	public Player[] Players 
	{
		get { return _players; }
		set { _players = value; }
	}

	public bool GameEnded
	{
		get { return _gameEnded; }
		set { _gameEnded = value; }
	}

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		} else {
			_instance = this;
		}
		DontDestroyOnLoad (gameObject);
	}

	IEnumerator StartGame()
	{
		while (PlayersAlive.Count != 1)
		{

		}
		yield return null;
	}

	public List<Player> PlayersAlive
	{
		get
		{
			List<Player> playersAlive = new List<Player> ();
			for (int i = 0; i < playersNumber; i++)
				if (_players [i].IsAlive)
					playersAlive.Add(_players[i]);
			return playersAlive;
		}
	}

	public void CheckInput(int colorId, int damage)
	{
		List<Player> badPlayers = PlayersAlive;
		_checkTimeEnded = true;
		StartCoroutine (Timer ());
		while (!_checkTimeEnded)
			badPlayers.RemoveAll(player => IsCorrectInput (player.Id, colorId));
		for (int i = 0; i < badPlayers.Count; i++)
			badPlayers [i].Hit (damage);
	}

	public void CheckGame()
	{
		if (PlayersAlive.Count < 2)
		{
			_gameEnded = true;
		}
	}

	public IEnumerator Timer()
	{
		yield return new WaitForSeconds (inputTime);
		_checkTimeEnded = true;
	}

	public bool IsCorrectInput(int playerId, int colorId)
	{
		int[] actions = inputColors [colorId].actionIds;
		List<int> pressedActions = new List<int> ();
		if (Input.GetAxisRaw (GetActionName(AXESNAMES [0], playerId)) == -1)
			pressedActions.Add (0);
		if (Input.GetAxisRaw (GetActionName(AXESNAMES [0], playerId)) == 1)
			pressedActions.Add (1);
		if (Input.GetAxisRaw (GetActionName(AXESNAMES [1], playerId)) == -1)
			pressedActions.Add (2);
		if (Input.GetAxisRaw (GetActionName(AXESNAMES [1], playerId)) == 1)
			pressedActions.Add (3);
		for (int i = 0; i < BUTTONSNAMES.Length; i++)
			if (Input.GetButtonDown(GetActionName(BUTTONSNAMES[i],playerId)))
				pressedActions.Add(i+4);
		if (actions.Length != pressedActions.Count)
			return false;
		else
		{
			for (int i = 0; i < actions.Length; i++)
				if (actions [i] != pressedActions [i])
					return false;
		}
		return true;	
	}

	public string GetActionName(string action, int playerId)
	{
		return action + PLAYERINPUT + playerId.ToString ();
	}
}
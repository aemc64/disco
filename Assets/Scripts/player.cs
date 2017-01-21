using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private int _lifePoints;
	private int _id;

	public int Id
	{
		get { return _id; }
		set { _id = value; } 
	}

	public int LifePoints 
	{
		get { return _lifePoints; }
		set { _lifePoints = value; }
	}

	public bool IsAlive
	{
		get { return _lifePoints > 0; }
	}

	public void Hit(int damage)
	{
		_lifePoints -= damage;
	}
}
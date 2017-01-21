using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tester : MonoBehaviour {
	public Manager_container m;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("z")) m.cargado = true;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wave {
	public float create_time;
	public int correctVal;
	public wave(){
		create_time = Time.time;
		correctVal = 1;
		//correctVal = Random.Range (1, 4);
	}
}

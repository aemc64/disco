using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager {
	public int max_waves;
	private player[] players;
	private wave[] waves;

	public manager(){
		for (int i = 0;i<4;i++){
			players = new player[4];
			players[i]=new player();
		}
		for (int i = 0;i<max_waves;i++){
			waves = new wave[max_waves];
			waves[i]=new wave();
		}
	}
}

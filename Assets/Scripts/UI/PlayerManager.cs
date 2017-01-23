using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    // Use this for initialization
    private int actual = 0;
    public Sprite[] SelectedImages;
	private Sprite[] originalImages;
	private string[] originalText;
	public GameObject[] panels = new GameObject[4];
    private string[] players = new string[4];

    void Start() {
		originalImages = new Sprite[panels.Length];
		originalText = new string[panels.Length];
		for (int i = 0; i < panels.Length; i++) {
			originalImages [i] = panels [i].GetComponentInChildren<Image> ().sprite;
			originalText [i] = panels [i].GetComponentInChildren<Text> ().text;
		}
    }

    // Update is called once per frame

	public void Reset()
	{
		actual = 0;
		players = new string[4];
		for (int i = 0; i < panels.Length; i++) {
			panels [i].GetComponentInChildren<Image> ().sprite = originalImages [i];
			panels [i].GetComponentInChildren<Text> ().text = originalText [i];
		}
	}

    void Update() {

		if (players[0] != null && (Input.GetKeyDown("return") || Input.GetKeyDown("joystick button 7")))
		{
            players.CopyTo(Manager_container.Instance.players,0);
			Manager_container.Instance.max_players = actual;
			GetComponent<LoadGameScene> ().LoadGame ();
        }

        if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0))
        {
            bool there = false;
            for (int l = 0; l < actual; l++)
            {
                if (players[l] == "T1") there = true;
            }
            if (!there)
            {
                players[actual] = "T1";
                panels[actual].GetComponentsInChildren<Text>()[0].text = "T1";
				panels [actual].GetComponentsInChildren<Image> () [0].sprite = SelectedImages [actual];
                actual++;
            }
        }
            

        for (int j = 1; j <= 4; j++) {
            for (int i = 0; i < 20; i++)
            {

                if (Input.GetKeyDown("joystick " + j + " button " + i))
                {
                    print("joystick " + j + " button " + i);
                    bool there = false;
                    for (int l = 0; l < actual; l++)
                    {
                        if (players[l] == "P" + j) there = true;
                    }
                    if (!there)
                    {
                        players[actual] = "P" + j;
                        panels[actual].GetComponentsInChildren<Text>()[0].text = "P" + j;
						panels [actual].GetComponentsInChildren<Image> () [0].sprite = SelectedImages [actual];
                        actual++;
                        
                    }
                    break;
                }
            }
            
        }
    }
}

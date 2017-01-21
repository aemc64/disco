using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    // Use this for initialization
    private int actual = 0;
    public GameObject Panel1;
    public GameObject Panel2;
    public GameObject Panel3;
    public GameObject Panel4;
    private GameObject[] panels = new GameObject[4];
    private string[] players = new string[4];

    void Start() {
        panels[0] = Panel1;
        panels[1] = Panel2;
        panels[2] = Panel3;
        panels[3] = Panel4;

    }

    // Update is called once per frame

    
    void Update() {

        if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0))
        {
            bool there = false;
            for (int l = 0; l < actual; l++)
            {
                if (players[l] == "T") there = true;
            }
            if (!there)
            {
                players[actual] = "T";
                panels[actual].GetComponentsInChildren<Text>()[0].text = "T";
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
                        actual++;
                        
                    }
                    break;
                }
            }
            
        }
    }
}

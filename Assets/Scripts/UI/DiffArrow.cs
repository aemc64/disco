using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiffArrow : MonoBehaviour {

    // Use this for initialization
    public bool direction;
    public GameObject gameMode;
    public GameObject arrow;
    public Sprite[] images;
    public int set = 0;


    public void Modify()
    {
        if (direction)
        {
            GameSettings.difficulty++;
            if (GameSettings.difficulty == 4) GameSettings.difficulty = 0;
        }

        else
        {
            GameSettings.difficulty--;
            if (GameSettings.difficulty == -1) GameSettings.difficulty = 3;
        }

        switch (GameSettings.difficulty)
        {
            case 1:
                gameMode.GetComponent<Image>().sprite = images[0];
                break;
            case 2:
                gameMode.GetComponent<Image>().sprite = images[1];
                break;
            case 3:
                gameMode.GetComponent<Image>().sprite = images[2];
                break;
            default:
                gameMode.GetComponent<Image>().sprite = images[3];
                break;
        }
    }
}

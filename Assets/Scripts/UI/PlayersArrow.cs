using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayersArrow : MonoBehaviour {

    // Use this for initialization
    public bool direction;
    public GameObject gameMode;
    public GameObject arrow;
    public int set = 0;

    public void Modify()
    {
        if (direction)
        {
            set = (set == 4) ? 4 : set + 1;
        }

        else
        {
            set = (set == 1) ? 1 : set - 1;
        }

        switch (set)
        {
            case 1:
                gameMode.GetComponent<Text>().text = "1";
                gameObject.GetComponent<Button>().interactable = false;
                arrow.GetComponent<PlayersArrow>().set = 1;

                break;
            case 2:
                gameMode.GetComponent<Text>().text = "2";
                arrow.GetComponent<Button>().interactable = true;
                arrow.GetComponent<PlayersArrow>().set = 2;
                break;
            case 3:
                gameMode.GetComponent<Text>().text = "3";
                arrow.GetComponent<Button>().interactable = true;
                arrow.GetComponent<PlayersArrow>().set = 3;
                break;
            default:
                gameMode.GetComponent<Text>().text = "4";
                arrow.GetComponent<PlayersArrow>().set = 4;
                gameObject.GetComponent<Button>().interactable = false;
                break;
        }
    }
}

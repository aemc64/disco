using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GMArrow : MonoBehaviour
{

    // Use this for initialization
    public bool direction;
    public GameObject gameMode;
    public GameObject arrow;
    public int set = 0;

    public void Modify()
    {
        if (direction)
        {
            set++;
            set = (set == 3) ? 0 : set;
        }

        else
        {
            set--;
            set = (set == -1) ? 2 : set;
        }

        switch (set) {
            case 1:
                gameMode.GetComponent<Text>().text = "GROOVE";
                arrow.GetComponent<GMArrow>().set = 1;
                break;
            case 2:
                gameMode.GetComponent<Text>().text = "MOVE";
                arrow.GetComponent<GMArrow>().set = 2;
                break;
            default:
                gameMode.GetComponent<Text>().text = "DANCE";
                arrow.GetComponent<GMArrow>().set = 0;
                break;
        }
    }
}

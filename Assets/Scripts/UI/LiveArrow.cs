using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiveArrow : MonoBehaviour {

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
            set = (set == 5) ? 5 : set+1;
        }

        else
        {
            set = (set == 1) ? 1 : set-1;
        }

        switch (set)
        {
            case 1:
                gameMode.GetComponent<Image>().sprite = images[0];
                gameObject.GetComponent<Button>().interactable = false;
                arrow.GetComponent<LiveArrow>().set = 1;

                break;
            case 2:
                gameMode.GetComponent<Image>().sprite = images[1];
                arrow.GetComponent<Button>().interactable = true;
                arrow.GetComponent<LiveArrow>().set = 2;
                break;
            case 3:
                gameMode.GetComponent<Image>().sprite = images[2];
                arrow.GetComponent<LiveArrow>().set = 3;
                break;
            case 4:
                gameMode.GetComponent<Image>().sprite = images[3];
                arrow.GetComponent<Button>().interactable = true;
                arrow.GetComponent<LiveArrow>().set = 4;
                break;
            default:
                gameMode.GetComponent<Image>().sprite = images[4];
                arrow.GetComponent<LiveArrow>().set = 5;
                gameObject.GetComponent<Button>().interactable = false;
                break;
        }
    }
}

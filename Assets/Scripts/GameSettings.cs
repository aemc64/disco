using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameSettings : MonoBehaviour {

    public enum Diff
    {
        Noob,
        Sir,
        Master,
        Chaos
    };

    public enum GameM
    {
        Move,
        Groove,
        Dance
    };

    public static int lives, players;
    public static int difficulty;
    public static GameM gameMode;

    public static int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            lives = value;
        }
    }

    public static int Players
    {
        get
        {
            return players;
        }
        set
        {
            players = value;
        }
    }

    public static int Difficulty
    {
        get
        {
            return difficulty;
        }
        set
        {
            difficulty = value;
        }
    }

    public static GameM GameMode
    {
        get
        {
            return gameMode;
        }
        set
        {
            gameMode = value;
        }
    }
}

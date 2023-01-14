using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static int Score
    {
        get => PlayerPrefs.GetInt("SCORE", 0);
        set
        {
            PlayerPrefs.SetInt("SCORE", value);
            GameUIManager.Instance.UpdateScore();

            if (value > Highscore)
            {
                Highscore = value;
            }
        }
    }

    public static int Highscore
    {
        get => PlayerPrefs.GetInt("HIGHSCORE", 0);
        set
        {
            PlayerPrefs.SetInt("HIGHSCORE", value);
        }
    }

    public static int Health 
    {
        get => PlayerPrefs.GetInt("HEALTH", 0);
        set
        {
            PlayerPrefs.SetInt("HEALTH", value > 5 ?  5 : value);
            if (true)
            {
                
            }
        }
    }
        
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static int Highscore
    {
        get => PlayerPrefs.GetInt("HIGHSCORE", 0);
        set
        {
            PlayerPrefs.SetInt("HIGHSCORE", value);
            GameUIManager.Instance.UpdateHighscore();
        }
    }
}

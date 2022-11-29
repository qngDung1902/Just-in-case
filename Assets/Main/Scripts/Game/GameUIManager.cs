using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : SingletonMonoBehaviour<GameUIManager>
{
    public TextMesh highscoreDisplay;
    public override void Awake()
    {
        UpdateHighscore();

    }
    public void UpdateHighscore()
    {
        highscoreDisplay.text = $"HIGHSCORE: {PlayerDataManager.Highscore}";
    }
}

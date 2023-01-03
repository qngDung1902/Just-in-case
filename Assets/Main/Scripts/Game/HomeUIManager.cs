using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeUIManager : SingletonMonoBehaviour<HomeUIManager>
{
    public TextMesh highscoreDisplay;

    public override void Awake()
    {
        highscoreDisplay.text = $"HIGHSCORE: {PlayerDataManager.Highscore}";
    }
    
    public void OnStartClick()
    {
        GlobalSetting.Instance.ChangeToScene(Const.SCENE_DEMO);
    }
}

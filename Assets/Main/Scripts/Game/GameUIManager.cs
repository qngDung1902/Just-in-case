using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : SingletonMonoBehaviour<GameUIManager>
{
    public TextMesh highscoreDisplay;
    public List<GameObject> healths;
    public GameObject popupLose;
    private void Start()
    {
        UpdateScore();
        UpdateHealth();
    }

    public void UpdateScore()
    {
        highscoreDisplay.text = $"SCORE: {PlayerDataManager.Score}";
    }

    public void UpdateHealth()
    {
        for (int i = 0; i < 5; i++)
        {
            healths[i].SetActive(i < PlayerDataManager.Health);
        }
    }

    public void Home()
    {
        PlayerDataManager.Health = 3;
        PlayerPrefs.DeleteKey("autosaveX"); 
        GlobalSetting.Instance.ChangeToScene(Const.SCENE_HOME);
    }

    public void Back()
    {
        GlobalSetting.isContinue = false;
        GlobalSetting.Instance.ChangeToScene(Const.SCENE_HOME);
    }

    public void PlayAgain()
    {
        GlobalSetting.isContinue = false;
        PlayerDataManager.Health = 3;
        PlayerPrefs.DeleteKey("autosaveX"); 
        GlobalSetting.Instance.ChangeToScene(Const.SCENE_GAME);
    }

    public void ShowPopupLose(bool value)
    {
        popupLose.SetActive(value);
    }
}

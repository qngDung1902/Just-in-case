using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeUIManager : SingletonMonoBehaviour<HomeUIManager>
{
    public GameObject continueBtn;
    public TextMesh highscoreDisplay;

    public override void Awake()
    {
        SoundManager.Instance.music.Stop();
        highscoreDisplay.text = $"HIGHSCORE: {PlayerDataManager.Highscore}";
        continueBtn.SetActive(PlayerPrefs.HasKey("autosaveX"));
        GlobalSetting.isContinue = false;
    }
    
    public void OnStartClick()
    {
        PlayerDataManager.Health = 3;
        GlobalSetting.Instance.ChangeToScene(Const.SCENE_GAME);

        for (int i = 0; i < 101; i++)
        {
            PlayerPrefs.DeleteKey($"coin_Coin ({i})");
        }
        
    }

    public void OnContinueClick()
    {
        GlobalSetting.isContinue = true;
        GlobalSetting.Instance.ChangeToScene(Const.SCENE_GAME);
    }
}

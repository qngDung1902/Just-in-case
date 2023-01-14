using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GlobalSetting : SingletonMonoBehaviour<GlobalSetting>
{
    public static bool isContinue = false;
    public override void Awake()
    {
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(this);

        if (GlobalSetting.Exists())
        {
            Destroy(this.gameObject);
        }
    }

    public void ChangeToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static Vector2 autosaveCheckpoint
    {
        get
        {
            return new Vector2(PlayerPrefs.GetFloat("autosaveX", 0), PlayerPrefs.GetFloat("autosaveY", 0));
        }
        set
        {
            PlayerPrefs.SetFloat("autosaveX", value.x);
            PlayerPrefs.SetFloat("autosaveY", value.y);
        }
    }
}

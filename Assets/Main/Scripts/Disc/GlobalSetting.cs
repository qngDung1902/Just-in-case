using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GlobalSetting : SingletonMonoBehaviour<GlobalSetting>
{

    public override void Awake()
    {
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(this);
    }

    public void ChangeToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        // ChangeToScene(Const.SCENE_GAME);
    }
}

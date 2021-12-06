using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GlobalSetting : MonoBehaviour
{
    private static GlobalSetting instance;

    public static GlobalSetting Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<GlobalSetting>();
            }
            return instance;
        }
    }

    public static bool Exist => instance;

    private void Awake()
    {
        instance = this;
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
        ChangeToScene(Const.SCENE_GAME);
    }
}

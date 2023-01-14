using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public GameObject setting;

    public GameObject on, off;
    bool isShowing;
    private void Start()
    {
        on.SetActive(SettingData.Instance.MUSIC);
        off.SetActive(!SettingData.Instance.MUSIC);
    }

    public void OnClick()
    {
        isShowing = !isShowing;
        setting.SetActive(isShowing);
    }

    public void OnMusic()
    {
        SettingData.Instance.MUSIC = !SettingData.Instance.MUSIC; 
        on.SetActive(SettingData.Instance.MUSIC);
        off.SetActive(!SettingData.Instance.MUSIC);
    }
}

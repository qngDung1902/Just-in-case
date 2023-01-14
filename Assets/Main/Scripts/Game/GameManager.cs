using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public Player player;
    public int updateTime;


    public void Start()
    {
        SoundManager.Instance.PlayGameMusic();
        StartCoroutine(Autosave());
    }

    IEnumerator Autosave()
    {
        while (true)
        {
            yield return new WaitForSeconds(updateTime);
            GlobalSetting.autosaveCheckpoint = player.transform.position;
        }
    }
}

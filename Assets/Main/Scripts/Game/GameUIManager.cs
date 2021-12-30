using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : SingletonMonoBehaviour<GameUIManager>
{
    public GameObject flickArea, jumpArea;
    public void Active()
    {
        flickArea.gameObject.SetActive(true);
        jumpArea.gameObject.SetActive(false);
    }
}

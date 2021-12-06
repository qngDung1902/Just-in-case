using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobalEventManager : MonoBehaviour
{
    private static GlobalEventManager instance;

    public static GlobalEventManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<GlobalEventManager>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }
    
    public static bool Exist => instance;
    public Action EvtSend;
}

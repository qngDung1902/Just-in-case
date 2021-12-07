using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public void OnDelta(Vector2 value)
    {
        Debug.Log(value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : SingletonMonoBehaviour<EffectManager>
{
    public GameObject ghostEffect;

    public void SpawnGhostEffect(Vector2 position)
    {
        var obj = SimplePool.Spawn(ghostEffect, position, Quaternion.identity);
    }
}

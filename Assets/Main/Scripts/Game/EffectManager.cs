using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : SingletonMonoBehaviour<EffectManager>
{
    public GameObject ghostEffect, hitEffect;

    public void SpawnGhostEffect(Vector2 position)
    {
        var obj = SimplePool.Spawn(ghostEffect, position, Quaternion.identity);
    }

    public void SpawnHitEffect(Vector2 position, Vector2 direction)
    {
        var obj = SimplePool.Spawn(hitEffect, position, Quaternion.identity);
        obj.GetComponent<Hit>().UpdateDirection(direction);
    }
}

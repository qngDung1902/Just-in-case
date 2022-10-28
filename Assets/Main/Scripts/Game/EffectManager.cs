using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using BreakersNQD;

public class EffectManager : SingletonMonoBehaviour<EffectManager>
{
    public GameObject ghostEffect, hitEffect;
    public UnityEngine.Rendering.Universal.Light2D light2D;

    private Tween tween;

    public void SpawnGhostEffect(Vector2 position)
    {
        var obj = SimplePool.Spawn(ghostEffect, position, Quaternion.identity);
    }

    public void SpawnHitEffect(Vector2 position, Vector2 direction)
    {
        var obj = SimplePool.Spawn(hitEffect, position, Quaternion.identity);
        obj.GetComponent<Hit>().UpdateDirection(direction);

        light2D.intensity = 0.15f;
        tween.Kill();
        tween = DOTween.To(() => light2D.intensity, x => light2D.intensity = x, 1f, 0.25f).SetUpdate(true).SetDelay(0.35f).OnStart(() =>
        {
            
        });
    }

}

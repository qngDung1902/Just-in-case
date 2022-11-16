using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ghost : MonoBehaviour
{
    public float destroyTime;
    public Color color;
    // public Material material;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {

        // spriteRenderer.material = material;
        spriteRenderer.color = color;
        Despawn();
    }

    private void Despawn()
    {
        spriteRenderer.DOFade(0f, destroyTime + 0.1f).OnComplete(() =>
        {
            GhostController.Instance.Release(this);
        });
    }

    public void Init(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}

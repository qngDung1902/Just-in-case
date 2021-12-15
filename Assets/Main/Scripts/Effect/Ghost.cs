using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ghost : MonoBehaviour
{
    public float destroyTime;
    public Color color, colorRed;
    public Material material;
    private SpriteRenderer spriteRenderer;
    private InputController inputController;
    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        inputController = InputController.Instance;
    }

    private void OnEnable()
    {
        spriteRenderer.sprite = inputController.spriteRenderer.sprite;
        spriteRenderer.transform.localScale = inputController.transform.localScale;
        switch (inputController.playerForm)
        {
            case PlayerForm.BASIC:
                spriteRenderer.color = color;
                break;
            case PlayerForm.DEMON:
                destroyTime = 0.25f;
                spriteRenderer.color = colorRed;
                break;
        }
        spriteRenderer.material = material;
        Despawn();
    }

    private void Despawn()
    {
        spriteRenderer.DOFade(0f, destroyTime + 0.1f).OnComplete(() => { SimplePool.Despawn(this.gameObject); });
    }
}

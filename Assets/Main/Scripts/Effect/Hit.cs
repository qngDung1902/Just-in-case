using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hit : MonoBehaviour
{
    private Vector2 tmpDirection;

    public void UpdateDirection(Vector2 direction)
    {
        tmpDirection = direction;
        float angleRad = Mathf.Atan2(direction.y, direction.x);
        float angleDeg = (180 / Mathf.PI) * angleRad;
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);

        transform.position -= (Vector3)direction * 5f;
        transform.DOMove(transform.position + (Vector3)direction * 25f, 0.5f).SetUpdate(true).OnComplete(() => { SimplePool.Despawn(this.gameObject); });
    }
}

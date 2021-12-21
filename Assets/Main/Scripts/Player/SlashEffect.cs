using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    public void DisableSlashEffect()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.CompareTag("Enemy"))
        {
            InputController.Instance.HitEffect(collider.transform.position);
        }
    }
}

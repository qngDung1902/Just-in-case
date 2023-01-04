using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPortal : MonoBehaviour
{
    [SerializeField] private TeleportPortal transformTo;
    private bool cantTeleport;
    // public AudioClip teleIn;
    // public AudioClip teleOut;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (cantTeleport) return;

        if (collider.gameObject.CompareTag("Player"))
        {
            transformTo.cantTeleport = true;
            collider.transform.position = new Vector2(transformTo.transform.position.x, transformTo.transform.position.y);
            // if (teleIn)
            // {
            //     SoundManager.Instance.PlaySfxRewind(teleIn);
            // }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if (cantTeleport) cantTeleport = false;
            // if (teleOut)
            // {
            //     SoundManager.Instance.PlaySfxRewind(teleOut);
            // }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public SpriteRenderer renderer1;
    public Sprite sprite;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            renderer1.sprite = sprite;
            collider.GetComponent<Player>().lastCheckpointPosition = transform.position;
        }
    }
}

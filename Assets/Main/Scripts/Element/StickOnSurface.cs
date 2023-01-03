using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickOnSurface : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.contacts[0].normal == Vector2.down)
            {
                collision.transform.SetParent(transform);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D _collision) 
    {
        if (_collision.gameObject.CompareTag("Player"))
        {
            _collision.transform.SetParent(null);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butllet : MonoBehaviour
{
    [HideInInspector] public Vector2 direction = Vector2.zero;
    float despawnTime = 0;

    void OnEnable()
    {
        despawnTime = 8f;
    }

    void Update()
    {
        transform.position += (Vector3)(direction * 8f * Time.deltaTime);
        despawnTime -= Time.deltaTime;
        if (despawnTime <= 0)
        {
            Weapon.Instance.DestroyBullet(this);
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        Weapon.Instance.DestroyBullet(this);
        if (other.CompareTag("Enemy"))
        {
            PlayerDataManager.Highscore += 1;
        }
    }
}

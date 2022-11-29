using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Pool;
public class Weapon : SingletonMonoBehaviour<Weapon>
{
    [Header("---GUN---")]
    public Player player;
    public Butllet butlletPrefab;
    public Transform gunPivot;

    //====================OBJECT POOLING=====================
    Butllet butlletInstance;
    ObjectPool<Butllet> bulletPool;
    public override void Awake()
    {
        bulletPool = new ObjectPool<Butllet>(
            () => { return Instantiate(butlletPrefab); },
            bullet => bullet.gameObject.SetActive(true),
            bullet => bullet.gameObject.SetActive(false),
            bullet => Destroy(bullet.gameObject),
            false, 400, 1000
        );
    }
    //========================================================
    bool isFire;
    float fireRate = 0.18f;
    float nextFire = 0;

    private void Update()
    {
        if (isFire && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            bulletPool.Get(out butlletInstance);
            butlletInstance.direction.Set(player.Core.Movement.FacingDirection, 0);
            butlletInstance.transform.position = gunPivot.position;
        }
    }

    public void DestroyBullet(Butllet butllet)
    {
        bulletPool.Release(butllet);
    }

    public void StartFire()
    {
        isFire = true;

    }

    public void StopFire()
    {
        isFire = false;
    }
}

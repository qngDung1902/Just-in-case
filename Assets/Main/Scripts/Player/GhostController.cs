using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GhostController : SingletonMonoBehaviour<GhostController>
{
    public SpriteRenderer render;
    public Ghost ghostPrefab;
    public float delay;
    public bool display;
    private float delta = 0;
    private ObjectPool<Ghost> pool;
    public override void Awake()
    {
        pool = new ObjectPool<Ghost>(
            () => Instantiate(ghostPrefab, transform.position, Quaternion.identity),
            (prefab) =>
            {
                prefab.gameObject.SetActive(true);
                prefab.transform.position = transform.position;
            },
            (prefab) => prefab.gameObject.SetActive(false),
            (prefab) => Destroy(prefab.gameObject),
            false, 20, 40);
    }

    private void Update()
    {
        if (!display) return;
        if (delta > 0)
        {
            delta -= Time.deltaTime;
        }
        else
        {
            delta = delay;
            CreateGhost();
        }
    }

    public void Release(Ghost prefab)
    {
        pool.Release(prefab);
    }

    Ghost prefab;
    private void CreateGhost()
    {
        pool.Get(out prefab);
        prefab.Init(render.sprite);
    }
}

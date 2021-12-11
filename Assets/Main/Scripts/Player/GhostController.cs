using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public GameObject ghostPrefab;
    public float delay;
    private float delta = 0;
    
    private void Update() 
    {
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

    private void CreateGhost()
    {
        GameObject ghostObj = SimplePool.Spawn(ghostPrefab, transform.position, Quaternion.identity);
    }
}

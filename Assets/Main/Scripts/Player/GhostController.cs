using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public GameObject ghostPrefab;
    public float delay;
    private float delta = 0;

    private InputController player;
    private void Awake() 
    {
        player = InputController.Instance;    
    }
    
    private void Update() 
    {
        if (delta > 0)
        {
            delta -= Time.deltaTime;
        }
        else
        {
            delta = delay;
            // if (player.state == PlayerState.IDLE) return;
            
            CreateGhost();
        }
    }

    private void CreateGhost()
    {
        GameObject ghostObj = SimplePool.Spawn(ghostPrefab, transform.position, Quaternion.identity);
    }
}

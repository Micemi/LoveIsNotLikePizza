using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProfileSpawner : MonoBehaviour
{
    
    public GameObject Prefab;
    
    void Start()
    {
        Instantiate (Prefab, transform.position, transform.rotation);
        WhenSpawn.EventWhenSpawn += spawnprefab;
    }

    public void spawnprefab()
    {
        Instantiate (Prefab, transform.position, transform.rotation);
    }

}
public static class WhenSpawn
{
    
    public static Action EventWhenSpawn;

}
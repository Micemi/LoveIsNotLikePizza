using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProfileSpawner : MonoBehaviour
{
    
    public GameObject Prefab;
    public Transform CanvasTransform;
    


    void Start()
    {

        spawnprefab();

        WhenSpawn.EventRechaso += spawnprefab;
        
    }

    public void spawnprefab()
    {

        GameObject elcositoquespawnee;

        elcositoquespawnee = Instantiate (Prefab, CanvasTransform);


    }

}
public static class WhenSpawn
{
    
    public static Action EventRechaso;

    public static Action<Transform> EventSpawn;

    public static Action<Rigidbody2D,Pizza> EventSpawnRb2d;



}

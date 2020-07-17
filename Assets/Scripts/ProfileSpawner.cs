using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProfileSpawner : MonoBehaviour
{
    
    public GameObject Prefab;
    public Transform CanvasTransform;
    

    void OnEnable()
    {
        WhenSpawn.EventRechaso += spawnprefab;
    }
    
    void OnDisable()
    {
        WhenSpawn.EventRechaso -= spawnprefab;
    }

    void Start()
    {

        spawnprefab();

        
    }

    public void spawnprefab()
    {
        if ( Game.Current.RemainingPizzas.Count == 0)
        {
            Debug.Log("No hay mas pizzas xd");
        }
        else
        {

            GameObject elcositoquespawnee;

            elcositoquespawnee = Instantiate (Prefab,transform.position,transform.rotation, CanvasTransform);
        
        }

    }

}
public static class WhenSpawn
{
    
    public static Action EventRechaso;

    public static Action<Transform> EventSpawn;

    public static Action<Rigidbody2D,Pizza> EventSpawnRb2d;



}

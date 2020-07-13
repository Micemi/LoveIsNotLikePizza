using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
   private float speed = 0f;
   private Rigidbody2D rb2d;

    void Start()
    {
     
     rb2d = GetComponent<Rigidbody2D>();

    }
     

    void FixedUpdate() {
        
        
        rb2d.AddForce (new Vector2(speed,0));
        

    }
    
    public void Rechazo()
    {
        speed = -500f;
        
        Debug.Log("Rechasao");

        Destroy(gameObject, 5);


    }


}

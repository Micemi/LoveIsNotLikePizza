using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ImageProfileSelec : MonoBehaviour
{
    public Image AvatarPizzaProfile; 
    public Pizza unaPizzaRandom;

    void Start()
    {
        unaPizzaRandom = Game.Current.RemainingPizzas.GetRandom();
        Debug.Log("obtuviste: " + unaPizzaRandom.Name);

        
        WhenSpawn.EventSpawn(transform);
        
        Rigidbody2D rb2dprefab;

        rb2dprefab = GetComponent<Rigidbody2D>();

        WhenSpawn.EventSpawnRb2d(rb2dprefab, unaPizzaRandom);

        AvatarPizzaProfile.sprite = unaPizzaRandom.Image;

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ImageProfileSelec : MonoBehaviour
{
    public Image AvatarPizzaProfile; 

    void Start()
    {
        Pizza unaPizzaRandom = Game.Current.RemainingPizzas.GetRandom();
        Debug.Log("obtuviste: " + unaPizzaRandom.Name);
        
        AvatarPizzaProfile.sprite = unaPizzaRandom.Image;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

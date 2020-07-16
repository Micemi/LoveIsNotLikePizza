using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SwiperManager : MonoBehaviour
{

    /*
    |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
            Este script controla bastantes cosas de la escena del swiper
    |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    */

    float speed = 0f;
    public Rigidbody2D rb2d;
    public Button YesBtn;
    public Button NoBtn;
    public Button ProfileBtn;

    public Image AvatarPizzaProfile;

    
  
    void Start()
    {
        
        Pizza unaPizzaRandom = Game.Current.RemainingPizzas.GetRandom();
        Debug.Log("obtuviste: " + unaPizzaRandom.Name);
        
        AvatarPizzaProfile.sprite = unaPizzaRandom.Image;


        YesBtn.interactable = false;
        NoBtn.interactable = false;
        ProfileBtn.interactable = false;

        // Escucha si los eventos son disparados y activa cositas UwU
        SwiperManagerBtn.event_btn += BtnActivation;
        SwiperManagerBtn.event_NoBtn += RechazoBtn;
        
    }

    //solamente lo uso con Rigidbody2D para mover el perfil.
    void FixedUpdate() {
        
        
        rb2d.AddForce (new Vector2(speed,0));
        

    }
    
    public void Rechazo()
    {
        speed = -500f;
        
        Debug.Log("Rechasao");

        //en un futuro muy cercano hay que cambiar el destroy por un cambio de estado a los perfiles de las pizzas

        Destroy(rb2d.gameObject, 5);
        
        WhenSpawn.EventWhenSpawn();
        SwiperManagerBtn.event_NoBtn();
        

    }
    public void BtnActivation()
    {

        // esto practicamente es lo que activa el evento event_btn. En resumen habilita los botones
        if (YesBtn)
        {
            YesBtn.interactable = true;
        }

        if (NoBtn)
        {
            NoBtn.interactable = true;
        }

        if (YesBtn)
        {
            ProfileBtn.interactable = true;
        }

        

    }
    public void RechazoBtn()
    {
        YesBtn.interactable = false;
        NoBtn.interactable = false;
        ProfileBtn.interactable = false;
    }


}
// esta es otra clase donde cree los eventos
public static class SwiperManagerBtn
{

    public static Action event_btn;
    public static Action event_NoBtn;
    public static Action event_Pizza;
    

}


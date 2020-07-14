using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SwiperManager : MonoBehaviour
{

    ///////////////
    //Boton de NO//
    ///////////////

    float speed = 0f;
    public Rigidbody2D rb2d;
    public Button YesBtn;
    public Button NoBtn;
    public Button ProfileBtn;

    
  
    void Start()
    {
        
        YesBtn.interactable = false;
        NoBtn.interactable = false;
        ProfileBtn.interactable = false;

        SwiperManagerBtn.event_btn += BtnActivation;
        SwiperManagerBtn.event_NoBtn += Start;
    }

    void FixedUpdate() {
        
        
        rb2d.AddForce (new Vector2(speed,0));
        

    }
    
    public void Rechazo()
    {
        speed = -500f;
        
        Debug.Log("Rechasao");

        Destroy(gameObject, 5);

        SwiperManagerBtn.event_NoBtn();






    }
    public void BtnActivation()
    {

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


}
public static class SwiperManagerBtn
{

    public static Action event_btn;
    public static Action event_NoBtn;

}


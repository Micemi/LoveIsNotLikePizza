using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class SwiperManager : MonoBehaviour
{

    /*
    |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
            Este script controla bastantes cosas de la escena del swiper
    |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    */

    public float speed = 0f;
    public Rigidbody2D rb2d;
    public Button YesBtn;
    public Button NoBtn;
    public Button ProfileBtn;
    private Pizza PizzaPedida;
    public TextMeshProUGUI PizzaName;
    public TextMeshProUGUI PizzaDescription;

    void OnEnable()
    {
        // Escucha si los eventos son disparados y activa cositas UwU
        WhenSpawn.EventSpawnRb2d += rb2dchanger;
        SwiperManagerBtn.event_btn += BtnActivation;
        SwiperManagerBtn.event_NoBtn += RechazoBtn;
    }

    void OnDisable()
    {
        // Desactiva la escucha al desactivarse este gameobject UwU
        WhenSpawn.EventSpawnRb2d -= rb2dchanger;
        SwiperManagerBtn.event_btn -= BtnActivation;
        SwiperManagerBtn.event_NoBtn -= RechazoBtn;
    }
    
  
    void Start()
    {
        
        
        YesBtn.interactable = false;
        NoBtn.interactable = false;
        ProfileBtn.interactable = false;

       

        
    }

    //solamente lo uso con Rigidbody2D para mover el perfil.
    void FixedUpdate()
    {
        
        PizzaName.text = PizzaPedida.Name;
        PizzaDescription.text = PizzaPedida.Description;
    }
    
    public void Rechazo()
    {
        
        rb2d.AddForce (new Vector2(speed,0));
        
        Debug.Log("Rechasao");

        

        //en un futuro muy cercano hay que cambiar el destroy por un cambio de estado a los perfiles de las pizzas

        //cambio de estado "pedida" 

        PizzaPedida.State = PizzaState.Rejected;

        Destroy(rb2d.gameObject, 5);
        
        WhenSpawn.EventRechaso();
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

    
    public void rb2dchanger (Rigidbody2D rb2dpref, Pizza unaPizzaran)
    {

        rb2d = rb2dpref;

        PizzaPedida = unaPizzaran;
    }

    IEnumerator CoroutineReLoca()
    {
        
        yield return new WaitForSeconds(0.6f);
        SceneChanger.GoToChat();

    }

    public void aceptacion()
    {

        
        StartCoroutine(CoroutineReLoca());

        PizzaPedida.State = PizzaState.Matched;



        
    }


}
// esta es otra clase donde cree los eventos
public static class SwiperManagerBtn
{

    public static Action event_btn;
    public static Action event_NoBtn;
    public static Action event_Pizza;
    

}


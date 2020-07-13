using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatPresenter : MonoBehaviour
{
    // TODO: TEMP WILL REMOVE
    [SerializeField] private PizzaData pizzaData;
    
    private Chat chat;

    public float CurrentTime => chat.CurrentTime;
    public float CurrentHotness => chat.CurrentHotness;

    private void Awake()
    {
        chat = new Chat(new Pizza(pizzaData));
    }

    private void Start()
    {
        chat.Start();
    }

    private void Update()
    {
        chat.Tick(Time.deltaTime);
    }
}

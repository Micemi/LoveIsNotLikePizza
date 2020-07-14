using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatPresenter : MonoBehaviour
{
    [Header("Messages")]

    [SerializeField]
    private Transform messageContainer;

    [SerializeField]
    private ScrollRect messageScroller;

    [SerializeField]
    private MessagePresenter playerMessagePrefab;

    [SerializeField]
    private MessagePresenter pizzaMessagePrefab;

    [SerializeField]
    private float timeBetweenMessages = 1f;

    private readonly Queue<Message> messageQueue = new Queue<Message>();
    private float timeUntilNextMessage = 1f;

    private Chat chat;
    public Chat Chat => chat;

    private Pizza pizza;

    public float CurrentTime => chat.CurrentTime;
    public float CurrentHotness => chat.CurrentHotness;
    
    public Action WaitingForPlayerEmoji;
    public bool wasWaitingForPlayerEmojiFired;
    
    public void SendPlayerEmoji(Emoji emoji)
    {
        wasWaitingForPlayerEmojiFired = false;
        EnqueuePlayerMessage(emoji);
        chat.SendPlayerEmoji(emoji);
    }

    private void Awake()
    {
        pizza = Game.Current.RemainingPizzas.GetRandom();
        
        // TODO: no sacar una pizza random, sacar la pizza que me pasó el Swiper
        chat = new Chat(pizza);
        chat.OnPizzaSendsEmoji += EnqueuePizzaMessage;
        chat.OnPizzaSendsReaction += EnqueuePizzaMessage;
        chat.OnChatFinish += FinishChat;
    }

    private void Start()
    {
        chat.Start();
    }

    private void Update()
    {
        chat.Tick(Time.deltaTime);
        MessageInstantiation();
    }

    private void MessageInstantiation()
    {
        timeUntilNextMessage -= Time.deltaTime;

        if (messageQueue.Count == 0)
        {
            if (!wasWaitingForPlayerEmojiFired && chat.State == Chat.ChatState.WaitingForPlayerEmoji)
            {
                WaitingForPlayerEmoji?.Invoke();
                wasWaitingForPlayerEmojiFired = true;
            }

            return;
        }

        if (timeUntilNextMessage <= 0)
        {
            Message nextMessage = messageQueue.Dequeue();
            InstantiateMessage(nextMessage);
            timeUntilNextMessage = timeBetweenMessages;
        }
    }

    private void EnqueuePizzaMessage(Emoji emoji)  => EnqueueMessage(emoji, chat.Pizza);
    private void EnqueuePlayerMessage(Emoji emoji) => EnqueueMessage(emoji, null);
    private void EnqueueMessage(Emoji emoji, Pizza pizza) =>
        messageQueue.Enqueue(new Message { Emoji = emoji, Author = pizza, IsPizzaMessage = pizza != null });

    private void InstantiateMessage(Message message)
    {
        MessagePresenter messagePrefab = message.IsPizzaMessage ? pizzaMessagePrefab : playerMessagePrefab;
        MessagePresenter messagePresenter = Instantiate(messagePrefab, messageContainer);
        messagePresenter.SetMessage(message.Emoji, message.Author);
        messageScroller.verticalNormalizedPosition = 0; // scrolls to bottom
    }

    private void FinishChat(float points)
    {
        // Acá de alguna forma hay que pasarle estos puntos a una pantalla de éxito
        Debug.Log($"Yay! You got {points} points!");
    }
}

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

    private bool chatRunning;
    public bool ChatRunning => chatRunning;

    private Pizza pizza;

    public float CurrentTime => chat.CurrentTime;
    public float CurrentHotness => chat.CurrentHotness;

    public Action OnChatStarted = delegate {  };
    public Action WaitingForPlayerEmoji = delegate {  };
    public bool wasWaitingForPlayerEmojiFired;
    public Action OnChatFinished = delegate {  };

    public void SendPlayerEmoji(Emoji emoji)
    {
        wasWaitingForPlayerEmojiFired = false;
        EnqueuePlayerMessage(emoji);
        chat.SendPlayerEmoji(emoji);
    }

    [ContextMenu("StartChatWithRandomPizza")]
    public void StartChatWithRandomPizza() => StartChat(Game.Current.RemainingPizzas.GetRandom());
    public void StartChat(Pizza pizza)
    {
        this.pizza = pizza;
        chat                      =  new Chat(pizza);
        chat.OnPizzaSendsEmoji    += EnqueuePizzaMessage;
        chat.OnPizzaSendsReaction += EnqueuePizzaMessage;
        chat.OnChatFinish         += FinishChat;
        ClearMessages();
        OnChatStarted.Invoke();
        chat.Start();
        chatRunning = true;
    }

    private void ClearMessages()
    {
        messageQueue.Clear();
        foreach (Transform child in messageContainer)
            Destroy(child.gameObject);
    }

    private void Update()
    {
        if (!chatRunning) return;

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
                WaitingForPlayerEmoji.Invoke();
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
        chatRunning = false;
        OnChatFinished.Invoke();
        // Acá de alguna forma hay que pasarle estos puntos a una pantalla de éxito
        Debug.Log($"Yay! You got {points} points!");
    }
}

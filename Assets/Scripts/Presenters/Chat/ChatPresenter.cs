using System;
using UnityEngine;

public class ChatPresenter : MonoBehaviour
{
    [SerializeField]
    private MessageContainerPresenter messageContainer;

    private Chat chat;
    public Chat Chat => chat;

    private bool chatRunning;
    public bool ChatRunning => chatRunning;

    private Pizza pizza;
    public Pizza Pizza => pizza;

    public float CurrentTime => chat.CurrentTime;
    public float CurrentHotness => chat.CurrentHotness;

    public Action OnChatStarted = delegate {  };
    public Action OnWaitingForPlayerEmoji = delegate {  };
    public Action OnChatFinished = delegate {  };

    private void OnEnable()
    {
        SubscribeToChat();
    }

    private void OnDisable()
    {
        UnsubscribeToChat();
    }

    private void Update()
    {
        if (!chatRunning) return;

        chat.Tick(Time.deltaTime);
    }

    private void SubscribeToChat()
    {
        if (chat == null) return;
        chat.OnPizzaSendsEmoji    += messageContainer.EnqueuePizzaMessage;
        chat.OnPizzaSendsReaction += messageContainer.EnqueuePizzaMessage;
        chat.OnChatFinish         += FinishChat;
    }

    private void UnsubscribeToChat()
    {
        if (chat == null) return;
        chat.OnPizzaSendsEmoji    -= messageContainer.EnqueuePizzaMessage;
        chat.OnPizzaSendsReaction -= messageContainer.EnqueuePizzaMessage;
        chat.OnChatFinish         -= FinishChat;
    }

    [ContextMenu("StartChatWithRandomPizza")]
    public void StartChatWithRandomPizza() => StartChat(Game.Current.RemainingPizzas.GetRandom());
    public void StartChat(Pizza pizza)
    {
        this.pizza = pizza;
        chat = new Chat(pizza);
        SubscribeToChat();
        OnChatStarted.Invoke();
        chat.Start();
        chatRunning = true;
    }

    public void SendPlayerEmoji(Emoji emoji)
    {
        messageContainer.EnqueuePlayerMessage(emoji);
        chat.SendPlayerEmoji(emoji);
    }

    private void FinishChat(float points)
    {
        chatRunning = false;
        OnChatFinished.Invoke();
        // Acá de alguna forma hay que pasarle estos puntos a una pantalla de éxito
        Debug.Log($"Yay! You got {points} points!");
    }
}

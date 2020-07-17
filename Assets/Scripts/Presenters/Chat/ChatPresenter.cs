using System;
using UnityEngine;
using UnityEngine.Events;

public class ChatPresenter : MonoBehaviour
{
    [SerializeField]
    private MessageContainerPresenter messageContainer;

    private Chat chat;

    private bool chatRunning;
    public bool ChatRunning => chatRunning;

    private Pizza pizza;
    public Pizza Pizza => pizza;

    public float CurrentTime => chat.CurrentTime;
    public float CurrentHotness => chat.CurrentHotness;

    public Action OnChatStarted = delegate {  };
    public Action OnWaitingForPlayerEmoji = delegate {  };
    public Action<Emoji> OnPizzaSendsEmoji;   
    public Action<Emoji> OnPizzaSendsReaction;
    public Action<Emoji> OnPlayerSendsEmoji;

    public UnityEvent OnChatFinished;

    private void Start()
    {
        StartChat(); //Deberia funcionar dicen las malas lenguaaaaaas
    }
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
        chat.OnPizzaSendsEmoji    += PizzaSendsEmoji;
        chat.OnPizzaSendsReaction += PizzaSendsReaction;
        chat.OnChatFinish         += FinishChat;
    }

    private void UnsubscribeToChat()
    {
        if (chat == null) return;
        chat.OnPizzaSendsEmoji    -= PizzaSendsEmoji;
        chat.OnPizzaSendsReaction -= PizzaSendsReaction;
        chat.OnChatFinish         -= FinishChat;
    }

    public void StartChat()
    {
        pizza = Game.Current.MatchedPizzas[0];
        chat = new Chat(pizza);
        SubscribeToChat();
        OnChatStarted.Invoke();
        chat.Start();
        chatRunning = true;
    }

    private void PizzaSendsEmoji(Emoji emoji) => OnPizzaSendsEmoji.Invoke(emoji);
    private void PizzaSendsReaction(Emoji emoji) => OnPizzaSendsReaction.Invoke(emoji);
    public void SendPlayerEmoji(Emoji emoji)
    {
        OnPlayerSendsEmoji.Invoke(emoji);
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

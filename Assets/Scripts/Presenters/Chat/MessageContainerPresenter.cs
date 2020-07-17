using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MessageContainerPresenter : MonoBehaviour
{
    [SerializeField]
    private ChatPresenter chat;

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
    private MessagePresenter pizzaReactionPrefab;

    [SerializeField]
    private float timeBetweenMessages = 1f;

    private readonly Queue<Message> messageQueue = new Queue<Message>();
    private float timeUntilNextMessage = 1f;
    private Dictionary<MessageType, MessagePresenter> messagePrefabByType;

    private bool wasWaitingForPlayerEmojiFired;

    public UnityEvent OnPizzaMessageSent;
    public UnityEvent OnPlayerMessageSent;

    private void OnEnable()
    {
        chat.OnPizzaSendsEmoji    += EnqueuePizzaMessage;
        chat.OnPizzaSendsReaction += EnqueuePizzaReaction;
        chat.OnPlayerSendsEmoji   += EnqueuePlayerMessage;
        // chat.OnChatStarted += ClearMessages;
    }

    private void OnDisable()
    {
        chat.OnPizzaSendsEmoji    -= EnqueuePizzaMessage;
        chat.OnPizzaSendsReaction -= EnqueuePizzaReaction;
        chat.OnPlayerSendsEmoji   -= EnqueuePlayerMessage;
        // chat.OnChatStarted -= ClearMessages;
    }

    /*
    private void ClearMessages()
    {
        messageQueue.Clear();
        foreach (Transform child in messageContainer)
            Destroy(child.gameObject);
    }
    */

    private void Awake()
    {
        messagePrefabByType = new Dictionary<MessageType, MessagePresenter>
        {
            [MessageType.Player] = playerMessagePrefab,
            [MessageType.PizzaEmoji] = pizzaMessagePrefab,
            [MessageType.PizzaReaction] = pizzaReactionPrefab,
        };
    }

    private void Update()
    {
        if (!chat.ChatRunning) return;
        
        timeUntilNextMessage -= Time.deltaTime;

        if (messageQueue.Count == 0)
        {
            if (!wasWaitingForPlayerEmojiFired)// && chat.State == Chat.ChatState.WaitingForPlayerEmoji)
            {
                chat.OnWaitingForPlayerEmoji.Invoke();
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
    
    private void InstantiateMessage(Message message)
    {
        MessagePresenter messagePresenter = Instantiate(messagePrefabByType[message.Type], messageContainer);
        messagePresenter.SetMessage(message.Emoji, message.Author);
        messageScroller.verticalNormalizedPosition = 0; // scrolls to bottom
        if (message.Type == MessageType.Player)
            OnPlayerMessageSent.Invoke();
        else
            OnPizzaMessageSent.Invoke();
    }
    
    public void EnqueuePizzaMessage(Emoji emoji)   => EnqueueMessage(emoji, chat.Pizza, MessageType.PizzaEmoji);
    public void EnqueuePizzaReaction(Emoji emoji)  => EnqueueMessage(emoji, chat.Pizza, MessageType.PizzaReaction);

    public void EnqueuePlayerMessage(Emoji emoji)
    {
        wasWaitingForPlayerEmojiFired = false;
        EnqueueMessage(emoji, null, MessageType.Player);
    }

    private void EnqueueMessage(Emoji emoji, Pizza pizza, MessageType type) =>
        messageQueue.Enqueue(new Message { Emoji = emoji, Author = pizza, Type = type});

}

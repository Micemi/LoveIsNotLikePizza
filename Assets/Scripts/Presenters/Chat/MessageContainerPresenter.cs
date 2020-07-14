using System.Collections.Generic;
using UnityEngine;
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
    private float timeBetweenMessages = 1f;

    private readonly Queue<Message> messageQueue = new Queue<Message>();
    private float timeUntilNextMessage = 1f;

    private bool wasWaitingForPlayerEmojiFired;

    private void OnEnable()
    {
        chat.OnChatStarted += ClearMessages;
    }

    private void OnDisable()
    {
        chat.OnChatStarted -= ClearMessages;
    }

    private void ClearMessages()
    {
        messageQueue.Clear();
        foreach (Transform child in messageContainer)
            Destroy(child.gameObject);
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
        MessagePresenter messagePrefab = message.IsPizzaMessage ? pizzaMessagePrefab : playerMessagePrefab;
        MessagePresenter messagePresenter = Instantiate(messagePrefab, messageContainer);
        messagePresenter.SetMessage(message.Emoji, message.Author);
        messageScroller.verticalNormalizedPosition = 0; // scrolls to bottom
    }
    
    public void EnqueuePizzaMessage(Emoji emoji)  => EnqueueMessage(emoji, chat.Pizza);

    public void EnqueuePlayerMessage(Emoji emoji)
    {
        wasWaitingForPlayerEmojiFired = false;
        EnqueueMessage(emoji, null);
    }

    private void EnqueueMessage(Emoji emoji, Pizza pizza) =>
        messageQueue.Enqueue(new Message { Emoji = emoji, Author = pizza, IsPizzaMessage = pizza != null });

}

using UnityEngine;
using UnityEngine.UI;

public class ChatPresenter : MonoBehaviour
{
    // TODO: TEMP WILL REMOVE
    // de alguna forma hay que inyectarle la pizza a esto
    [SerializeField]
    private PizzaData pizzaData;

    [Header("Messages")]

    [SerializeField]
    private Transform messageContainer;

    [SerializeField]
    private ScrollRect messageScroller;

    [SerializeField]
    private MessagePresenter playerMessagePrefab;

    [SerializeField]
    private MessagePresenter pizzaMessagePrefab;


    private Chat chat;
    public Chat Chat => chat;

    public float CurrentTime => chat.CurrentTime;
    public float CurrentHotness => chat.CurrentHotness;
    public void SendPlayerEmoji(Emoji emoji)
    {
        InstantiatePlayerMessage(emoji);
        chat.SendPlayerEmoji(emoji);
    }

    private void Awake()
    {
        chat = new Chat(new Pizza(pizzaData));
        chat.OnPizzaSendsEmoji += InstantiatePizzaMessage;
        chat.OnPizzaSendsReaction += InstantiatePizzaMessage;
        chat.OnChatFinish += FinishChat;
    }

    private void Start()
    {
        chat.Start();
    }

    private void Update()
    {
        chat.Tick(Time.deltaTime);
    }

    private void InstantiatePizzaMessage(Emoji emoji)
    {
        MessagePresenter message = Instantiate(pizzaMessagePrefab, messageContainer);
        message.SetMessage(emoji, chat.Pizza);
    }

    private void InstantiatePlayerMessage(Emoji emoji)
    {
        MessagePresenter message = Instantiate(playerMessagePrefab, messageContainer);
        message.SetMessage(emoji);
        messageScroller.verticalNormalizedPosition = 0; // scrolls to bottom
    }

    private void FinishChat(float points)
    {
        // Acá de alguna forma hay que pasarle estos puntos a una pantalla de éxito
        Debug.Log($"Yay! You got {points} points!");
    }
}

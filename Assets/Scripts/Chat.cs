using System;
using System.Linq;
using UnityEngine;

public class Chat
{
    public enum ChatState
    {
        WaitingForPlayerEmoji,
        WaitingForPizzaEmoji,
        Finished,
    }

    public readonly Pizza Pizza;

    private float currentTime;
    public float CurrentTime => Mathf.Max(0, currentTime);

    private float currentHotness = 0.5f;
    public float CurrentHotness => currentHotness;

    private ChatState state = ChatState.WaitingForPizzaEmoji;
    public ChatState State => state;

    public bool ChatFinished => state == ChatState.Finished;
    public bool CanSendPlayerEmoji => state == ChatState.WaitingForPlayerEmoji;

    public event Action<Emoji> OnPizzaSendsEmoji;
    public event Action<Emoji> OnPizzaSendsReaction;
    public event Action<float> OnChatFinish; // float is the points

    public Chat(Pizza pizza)
    {
        Pizza = pizza;
    }

    public void Start()
    {
        currentTime = Pizza.Difficulty.InitialTime;
    }

    public void Tick(float deltaTime)
    {
        switch (state)
        {
            case ChatState.Finished:
                return;
            case ChatState.WaitingForPizzaEmoji:
                SendPizzaEmoji();
                break;
            case ChatState.WaitingForPlayerEmoji:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        DecreaseTime(deltaTime);
        DecreaseHotness(deltaTime);
    }

    private void DecreaseHotness(float deltaTime)
    {
        float newHotness = currentHotness - Pizza.Difficulty.CoolingPerSec * deltaTime;
        currentHotness = Mathf.Max(0, newHotness);
    }

    private void DecreaseTime(float deltaTime)
    {
        currentTime -= deltaTime;
        if (currentTime <= 0)
            EndChat();
    }

    private void SendPizzaEmoji()
    {
        Emoji emoji = Pizza.GetRandomEmoji();
        OnPizzaSendsEmoji?.Invoke(emoji);
        state = ChatState.WaitingForPlayerEmoji;
    }

    public void SendPlayerEmoji(Emoji emoji)
    {
        if (!CanSendPlayerEmoji) return;

        Emoji reactionEmoji;
        bool acceptedEmoji = Pizza.Flavors.Contains(emoji.Flavor);
        if (acceptedEmoji)
        {
            float newHotness = currentHotness + Pizza.Difficulty.HotnessBonus;
            currentHotness = Mathf.Min(1, newHotness);
            reactionEmoji = new Emoji(EmojiData.EmojisByCategory[EmojiCategory.GoodReaction].GetRandom());
        }
        else
        {
            currentTime -= Pizza.Difficulty.TimePenalty;
            float newHotness = currentHotness - Pizza.Difficulty.HotnessPenalty;
            currentHotness = Mathf.Max(0, newHotness);
            reactionEmoji = new Emoji(EmojiData.EmojisByCategory[EmojiCategory.BadReaction].GetRandom());
        }
        OnPizzaSendsReaction?.Invoke(reactionEmoji);
        state = ChatState.WaitingForPizzaEmoji;
    }

    private void EndChat()
    {
        float points = ChatPoints.GetPoints(CurrentHotness);
        OnChatFinish?.Invoke(points);
        state = ChatState.Finished;
    }

}

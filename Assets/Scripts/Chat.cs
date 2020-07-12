using System;
using System.Linq;
using UnityEngine;

public class Chat
{
    public enum ChatState
    {
        WaitingForPlayerEmoji,
        WaitingForPizzaEmoji,
        Finished
    }

    public readonly Pizza Pizza;

    public float CurrentTime;
    public float CurrentHotness = 0.5f;

    private ChatState state = ChatState.WaitingForPizzaEmoji;
    public ChatState State => state;

    public bool ChatFinished => state == ChatState.Finished;

    public event Action<Emoji> OnPizzaSendsEmoji;
    public event Action<Emoji> OnPizzaSendsReaction;

    public Chat(Pizza pizza)
    {
        Pizza = pizza;
    }
    
    public void Start()
    {
        CurrentTime = Pizza.Difficulty.InitialTime;
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
        float newHotness = CurrentHotness - Pizza.Difficulty.CoolingPerSec * deltaTime;
        CurrentHotness = Mathf.Max(0, newHotness);
    }

    private void DecreaseTime(float deltaTime)
    {
        CurrentTime -= deltaTime;
        if (CurrentTime <= 0)
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
        Emoji reactionEmoji;
        bool acceptedEmoji = Pizza.Flavors.Contains(emoji.Flavor);
        if (acceptedEmoji)
        {
            float newHotness = CurrentHotness + Pizza.Difficulty.HotnessBonus;
            CurrentHotness = Mathf.Min(1, newHotness);
            reactionEmoji = new Emoji(EmojiData.EmojisByCategory[EmojiCategory.GoodReaction].GetRandom());
        }
        else
        {
            float newHotness = CurrentHotness - Pizza.Difficulty.HotnessPenalty;
            CurrentHotness = Mathf.Max(0, newHotness);
            reactionEmoji = new Emoji(EmojiData.EmojisByCategory[EmojiCategory.BadReaction].GetRandom());
        }
        OnPizzaSendsReaction?.Invoke(reactionEmoji);
        state = ChatState.WaitingForPizzaEmoji;
    }

    private void EndChat()
    {
        state = ChatState.Finished;
    }
    
}

public class Message
{
    public Pizza Author;
    public Emoji Emoji;
    public MessageType Type;
}

public enum MessageType
{
    Player,
    PizzaEmoji,
    PizzaReaction,
}

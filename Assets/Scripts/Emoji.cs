using UnityEngine;

public class Emoji
{
    public string Name;
    public Sprite Image;
    public Flavor Flavor;
    public EmojiCategory Category;

    public Emoji() {}

    public Emoji(EmojiData emojiData)
    {
        Name = emojiData.Name;
        Image = emojiData.Image;
        Flavor = emojiData.Flavor;
        Category = emojiData.Category;
    }
}

public enum EmojiCategory
{
    Emoji,
    GoodReaction,
    BadReaction,
}

using UnityEngine;

public class Emoji
{
    public string Name;
    public Sprite Image;
    public Flavor Flavor;
    public EmojiCategory Category;

    public Emoji(string name, Sprite image, Flavor flavor)
    {
        Name = name;
        Image = image;
        Flavor = flavor;
    }

    public Emoji(EmojiData emojiData)
    {
        Name = emojiData.Name;
        Image = emojiData.Image;
        Flavor = emojiData.Flavor;
    }
}

public enum EmojiCategory
{
    Emoji,
    GoodReaction,
    BadReaction,
}

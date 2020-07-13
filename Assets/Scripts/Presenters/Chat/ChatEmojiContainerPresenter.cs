using UnityEngine;

public class ChatEmojiContainerPresenter : MonoBehaviour
{
    [SerializeField]
    private EmojiPresenter[] emojiPresenters = new EmojiPresenter[4];

    [SerializeField]
    private ChatPresenter chat;

    private readonly Flavor[] flavors = { Flavor.Creepy, Flavor.Cute, Flavor.Geek, Flavor.Spicy };

    private void OnEnable()
    {
        for (int i = 0; i < emojiPresenters.Length; i++)
            emojiPresenters[i].OnEmojiClicked += ClickEmoji;
    }

    private void OnDisable()
    {
        for (int i = 0; i < emojiPresenters.Length; i++)
            emojiPresenters[i].OnEmojiClicked -= ClickEmoji;
    }

    private void Start()
    {
        chat.Chat.OnPizzaSendsEmoji += _ => EnableEmojis();
        chat.Chat.OnChatFinish      += _ => DisableEmojis();
    }

    private void EnableEmojis()
    {
        flavors.Shuffle();
        for (int i = 0; i < emojiPresenters.Length; i++)
            emojiPresenters[i].SetEmoji(new Emoji(EmojiData.EmojisByFlavor[flavors[i]].GetRandom()));
    }

    private void DisableEmojis()
    {
        for (int i = 0; i < emojiPresenters.Length; i++)
            emojiPresenters[i].DisableEmoji();
    }

    private void ClickEmoji(Emoji emoji)
    {
        chat.SendPlayerEmoji(emoji);
        DisableEmojis();
    }
}

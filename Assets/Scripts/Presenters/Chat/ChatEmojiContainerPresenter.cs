using UnityEngine;

public class ChatEmojiContainerPresenter : MonoBehaviour
{
    [SerializeField]
    private EmojiPresenter[] emojiPresenters = new EmojiPresenter[4];

    [SerializeField]
    private ChatPresenter chat;

    private Flavor[] flavors = { Flavor.Creepy, Flavor.Cute, Flavor.Geek, Flavor.Spicy };

    private void OnEnable()
    {
        emojiPresenters[0].OnEmojiClicked += ClickEmoji;
        emojiPresenters[1].OnEmojiClicked += ClickEmoji;
        emojiPresenters[2].OnEmojiClicked += ClickEmoji;
        emojiPresenters[3].OnEmojiClicked += ClickEmoji;
    }

    private void OnDisable()
    {
        emojiPresenters[0].OnEmojiClicked -= ClickEmoji;
        emojiPresenters[1].OnEmojiClicked -= ClickEmoji;
        emojiPresenters[2].OnEmojiClicked -= ClickEmoji;
        emojiPresenters[3].OnEmojiClicked -= ClickEmoji;
    }

    private void Start()
    {
        chat.Chat.OnPizzaSendsEmoji += _ => EnableEmojis();
    }

    private void EnableEmojis()
    {
        flavors.Shuffle();
        for (int i = 0; i < emojiPresenters.Length; i++)
        {
            emojiPresenters[i].SetEmoji(new Emoji(EmojiData.EmojisByFlavor[flavors[i]].GetRandom()));
        }
    }

    private void DisableEmojis()
    {
        emojiPresenters[0].DisableEmoji();
        emojiPresenters[1].DisableEmoji();
        emojiPresenters[2].DisableEmoji();
        emojiPresenters[3].DisableEmoji();
    }

    private void ClickEmoji(Emoji emoji)
    {
        chat.SendPlayerEmoji(emoji);
        DisableEmojis();
    }
}

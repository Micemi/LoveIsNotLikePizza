using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessagePresenter : MonoBehaviour
{
    [SerializeField]
    private Image avatar;

    [SerializeField]
    private Image emojiImage;

    [SerializeField]
    private Image secondEmojiImage;

    [SerializeField]
    private TextMeshProUGUI timeText;

    public void SetMessage(Emoji emoji, Pizza pizza = null)
    {
        if (avatar != null)
        {
            if (pizza == null)
                avatar.enabled = false;
            else
                avatar.sprite = pizza.Image;
        }

        if (timeText != null)
            timeText.text = DateTime.Now.ToString("h:mm tt", CultureInfo.InvariantCulture);

        if (emoji == null) return;

        emojiImage.sprite = emoji?.Image;

        if (secondEmojiImage != null)
            secondEmojiImage.sprite = new Emoji(EmojiData.EmojisByFlavor[emoji.Flavor].GetRandom()).Image;
    }
}

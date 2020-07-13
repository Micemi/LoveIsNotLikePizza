using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EmojiPresenter : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image image;

    private Emoji emoji;

    public event Action<Emoji> OnEmojiClicked;

    public void SetEmoji(Emoji emoji)
    {
        this.emoji = emoji;
        image.enabled = true;
        image.sprite = emoji.Image;
    }

    public void DisableEmoji()
    {
        image.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnEmojiClicked?.Invoke(emoji);
    }
}

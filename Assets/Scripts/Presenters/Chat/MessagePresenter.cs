using UnityEngine;
using UnityEngine.UI;

public class MessagePresenter : MonoBehaviour
{
    [SerializeField]
    private Image avatar;

    [SerializeField]
    private Image emojiImage;

    public void SetMessage(Emoji emoji, Pizza pizza = null)
    {
        if (avatar != null)
        {
            if (pizza == null)
                avatar.enabled = false;
            else
                avatar.sprite = pizza.Image;
        }

        emojiImage.sprite = emoji?.Image;
    }
}

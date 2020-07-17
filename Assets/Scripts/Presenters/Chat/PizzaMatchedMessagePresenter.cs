using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PizzaMatchedMessagePresenter : MonoBehaviour
{
    [SerializeField]
    private ChatPresenter chatPresenter;

    [SerializeField]
    private Image pizzaImage;

    [SerializeField]
    private TextMeshProUGUI pizzaName;

    private void Awake()
    {
        chatPresenter.OnChatStarted += SetPizza;
    }

    private void SetPizza()
    {
        pizzaImage.sprite = chatPresenter.Pizza.Image;
        pizzaName.text = chatPresenter.Pizza.Name + "!";
    }
}

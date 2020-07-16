using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointsPresenter : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private TextMeshProUGUI pizzaNameText;

    public void SetPizza(Pizza pizza)
    {
        image.sprite = pizza.Image;
        text.text = pizza.Points.ToString("0");
        pizzaNameText.text = pizza.Name;
    }
}

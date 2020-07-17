using UnityEngine;
using UnityEngine.UI;

public class PizzaSlicePresenter : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private Sprite[] spritesBySliceCount;

    private void OnEnable()
    {
        image.sprite = spritesBySliceCount[Game.Current.ChattedWithPizzas.Count];
    }
}

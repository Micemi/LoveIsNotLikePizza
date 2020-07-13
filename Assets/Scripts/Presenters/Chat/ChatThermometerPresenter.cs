using UnityEngine;
using UnityEngine.UI;

public class ChatThermometerPresenter : MonoBehaviour
{
    [SerializeField]
    private Image image;
    
    [SerializeField]
    private ChatPresenter chat;
    
    private void Update()
    {
        image.fillAmount = chat.CurrentHotness;
    }
}

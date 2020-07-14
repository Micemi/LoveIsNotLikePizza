using System.Globalization;
using TMPro;
using UnityEngine;

public class ChatTimerPresenter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private ChatPresenter chat;

    [Header("Danger Settings")]

    [SerializeField]
    private float dangerThreshold = 10f;

    [SerializeField]
    private Color dangerColor = Color.red;

    private Color originalColor = Color.white;

    private void Awake()
    {
        originalColor = text.color;
    }

    private void Update()
    {
        if (!chat.ChatRunning) return;

        float currentTime = chat.CurrentTime;

        if (currentTime > 0 && currentTime <= dangerThreshold)
            text.color = dangerColor;
        else
            text.color = originalColor;

        text.text = currentTime.ToString("0.00", NumberFormatInfo.InvariantInfo);
    }
}

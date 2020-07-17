using UnityEditor;
using UnityEngine.SceneManagement;

public static class SceneChanger
{
    [MenuItem("Love Pizza/Scenes/Go to start menu")]
    public static void GoToStartMenu() => SceneManager.LoadScene("StartMenu");

    [MenuItem("Love Pizza/Scenes/Go to profile")]
    public static void GoToProfile() => SceneManager.LoadScene("ProfileScene");

    [MenuItem("Love Pizza/Scenes/Go to swiper")]
    public static void GoToSwiper() => SceneManager.LoadScene("Swiper");

    [MenuItem("Love Pizza/Scenes/Go to chat")]
    public static void GoToChat() => SceneManager.LoadScene("ChatScene");
}

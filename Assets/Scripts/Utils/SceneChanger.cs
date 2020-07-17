using UnityEditor;
using UnityEngine.SceneManagement;

public static class SceneChanger
{
#if UNITY_EDITOR
    [MenuItem("Love Pizza/Scenes/Go to start menu")]
#endif
    public static void GoToStartMenu() => SceneManager.LoadScene("StartMenu");

#if UNITY_EDITOR
    [MenuItem("Love Pizza/Scenes/Go to profile")]
#endif
    public static void GoToProfile() => SceneManager.LoadScene("ProfileScene");

#if UNITY_EDITOR
    [MenuItem("Love Pizza/Scenes/Go to swiper")]
#endif
    public static void GoToSwiper() => SceneManager.LoadScene("Swiper");

#if UNITY_EDITOR
    [MenuItem("Love Pizza/Scenes/Go to chat")]
#endif
    public static void GoToChat() => SceneManager.LoadScene("ChatScene");
}

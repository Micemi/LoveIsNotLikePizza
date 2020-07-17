using UnityEngine;

public class SceneKey : MonoBehaviour
{
    private static SceneKey instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //  Literally what it says there
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // if (Input.GetKeyDown("1"))
        //     SceneChanger.GoToStartMenu();
        if (Input.GetKeyDown("1"))
            SceneChanger.GoToSwiper();
        // else if (Input.GetKeyDown("2"))
        //     SceneChanger.GoToChat();
        else if (Input.GetKeyDown("2"))
            SceneChanger.GoToProfile();
    }
}

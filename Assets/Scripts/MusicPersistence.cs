using UnityEngine;

public class MusicPersistence : MonoBehaviour
{
    private static MusicPersistence instance;
    
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            // This is like a Singleton!
            instance = this;
            DontDestroyOnLoad(gameObject); //  Literally what it says there
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

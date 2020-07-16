using UnityEngine;

public class MusicPersistence : MonoBehaviour {
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);    //  Literally what it says there
    }
}
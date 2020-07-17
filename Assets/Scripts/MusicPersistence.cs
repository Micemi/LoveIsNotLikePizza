using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPersistence : MonoBehaviour
{
    private static MusicPersistence instance;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip otherMusicClip;

    [SerializeField]
    private AudioClip chatMusicClip;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            // This is like a Singleton!
            instance = this;
            DontDestroyOnLoad(gameObject); //  Literally what it says there
            PlayOtherMusic();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayChatMusic()
    {
        audioSource.clip = chatMusicClip;
        audioSource.Play();
    }

    public void PlayOtherMusic()
    {
        audioSource.clip = otherMusicClip;
        audioSource.Play();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "ChatScene")
        {
            if (audioSource.clip != chatMusicClip)
                PlayChatMusic();
        }
        else
        {
            if (audioSource.clip != otherMusicClip)
                PlayOtherMusic();
        }
    }
}

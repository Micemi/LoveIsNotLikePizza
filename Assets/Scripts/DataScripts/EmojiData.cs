using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Emoji")]
public class EmojiData : ScriptableObject
{
    public static List<EmojiData> Emojis = new List<EmojiData>();
    
    public string Name;
    public Sprite Image;
    public Flavor Flavor;

    private void OnEnable()
    {
        Debug.Log("OnEnable for Emoji called");
        Emojis.Add(this);
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable for Emoji called");
        Emojis.Remove(this);
    }
}
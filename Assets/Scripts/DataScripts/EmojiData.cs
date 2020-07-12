using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Emoji")]
public class EmojiData : ScriptableObject
{
    public static readonly List<EmojiData> Emojis = new List<EmojiData>();
    public static readonly Dictionary<Flavor, List<EmojiData>> EmojisByFlavor = new Dictionary<Flavor, List<EmojiData>>();
    public static readonly Dictionary<EmojiCategory, List<EmojiData>> EmojisByCategory = new Dictionary<EmojiCategory, List<EmojiData>>();
    
    public string Name;
    public Sprite Image;
    public Flavor Flavor;
    public EmojiCategory Category;

    private void OnEnable()
    {
        if (!EmojisByFlavor.ContainsKey(Flavor))
            EmojisByFlavor[Flavor] = new List<EmojiData>();
        if (!EmojisByCategory.ContainsKey(Category))
            EmojisByCategory[Category] = new List<EmojiData>();
        
        Emojis.Add(this);
        EmojisByFlavor[Flavor].Add(this);
        EmojisByCategory[Category].Add(this);
    }

    private void OnDisable()
    {
        Emojis.Remove(this);
        EmojisByFlavor[Flavor].Remove(this);
        EmojisByCategory[Category].Remove(this);
    }
}
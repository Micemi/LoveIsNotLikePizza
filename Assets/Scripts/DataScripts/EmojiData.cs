using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Emoji")]
public class EmojiData : ScriptableObject
{
    #region Static Collections
    
    private static List<EmojiData> emojis;
    public static List<EmojiData> Emojis
    {
        get
        {
            if (emojis == null)
                ReloadEmojis();

            return emojis;
        }
    }
    
    private static Dictionary<Flavor, List<EmojiData>> emojisByFlavor;
    public static Dictionary<Flavor, List<EmojiData>> EmojisByFlavor
    {
        get
        {
            if (emojisByFlavor == null)
                ReloadEmojis();

            return emojisByFlavor;
        }
    }

    private static Dictionary<EmojiCategory, List<EmojiData>> emojisByCategory;
    public static Dictionary<EmojiCategory, List<EmojiData>> EmojisByCategory
    {
        get
        {
            if (emojisByCategory == null)
                ReloadEmojis();
            return emojisByCategory;
        }
    }

    [MenuItem("Love Pizza/Refresh Emoji Database")]
    public static void ReloadEmojis()
    {
        emojis = new List<EmojiData>(Resources.LoadAll<EmojiData>("Data/Emojis"));
        
        emojisByFlavor = new Dictionary<Flavor, List<EmojiData>>();
        emojisByCategory = new Dictionary<EmojiCategory, List<EmojiData>>();
        
        foreach (EmojiData emoji in Emojis)
        {
            if (!emojisByFlavor.ContainsKey(emoji.Flavor))
                emojisByFlavor[emoji.Flavor] = new List<EmojiData>();
            if (!emojisByCategory.ContainsKey(emoji.Category))
                emojisByCategory[emoji.Category] = new List<EmojiData>();

            emojisByFlavor[emoji.Flavor].Add(emoji);
            emojisByCategory[emoji.Category].Add(emoji);
        }
    }
    #endregion

    public string Name;
    public Sprite Image;
    public Flavor Flavor;
    public EmojiCategory Category;
}
using System.Collections.Generic;
using UnityEngine;

public class Pizza
{
    public string Name;
    public Sprite Image;
    public Difficulty Difficulty;
    [Tooltip("Son los flavors disponibles para que la pizza randomice. No puede ser menor que la cantidad de la dificultad. " +
             "SelectFlavors elije una cantidad (de Difficulty) según esta lista.")]
    public List<Flavor> AvailableFlavors;
    

    private List<Flavor> flavors = new List<Flavor>();
    public IReadOnlyList<Flavor> Flavors => flavors.AsReadOnly();

    public Pizza(string name, Sprite image, Difficulty difficulty, List<Flavor> availableFlavors)
    {
        Name = name;
        Image = image;
        Difficulty = difficulty;
        AvailableFlavors = availableFlavors;
        SetFlavors();
    }

    public Pizza(PizzaData pizzaData)
    {
        Name = pizzaData.Name;
        Image = pizzaData.Image;
        Difficulty = new Difficulty(pizzaData.Difficulty);
        AvailableFlavors = pizzaData.AvailableFlavors;
        SetFlavors();
    }
    
    private void SetFlavors()
    {
        AvailableFlavors.Shuffle();
        flavors = AvailableFlavors.GetRange(0, Difficulty.FlavorsQuantity);
    }

    public Emoji GetRandomEmoji()
    {
        if (flavors.Count == 0)
        {
            Debug.LogError("No Random Emoji available because there is no flavors");
            return null;
        }

        Flavor randomFlavor = flavors.GetRandom();
        List<EmojiData> availableEmojis = EmojiData.EmojisByFlavor[randomFlavor];
        return new Emoji(availableEmojis.GetRandom());
    }
}

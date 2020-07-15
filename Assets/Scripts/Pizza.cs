using System.Collections.Generic;
using UnityEngine;

public class Pizza
{
    private static Dictionary<PizzaState, List<Pizza>> pizzasByState;
    public static Dictionary<PizzaState, List<Pizza>> PizzasByState
    {
        get
        {
            if (pizzasByState == null)
                ClearPizzasCollections();

            return pizzasByState;
        }
    }
    private static List<Pizza> pizzas;
    public static List<Pizza> Pizzas
    {
        get
        {
            if (pizzas == null)
                ClearPizzasCollections();

            return pizzas;
        }
    }
    public static void ClearPizzasCollections()
    {
        pizzas = new List<Pizza>();
        pizzasByState = new Dictionary<PizzaState, List<Pizza>>
        {
            [PizzaState.Pending]  = new List<Pizza>(),
            [PizzaState.Rejected] = new List<Pizza>(),
            [PizzaState.Matched]  = new List<Pizza>(),
            [PizzaState.Chatted]  = new List<Pizza>(),
        };
    }


    public string Name;
    public Sprite Image;
    public Difficulty Difficulty;
    [Tooltip("Son los flavors disponibles para que la pizza randomice. No puede ser menor que la cantidad de la dificultad. " +
             "SelectFlavors elije una cantidad (de Difficulty) según esta lista.")]
    public List<Flavor> AvailableFlavors;
    
    private PizzaState state = PizzaState.Pending;
    public PizzaState State
    {
        get => state;
        set
        {
            PizzasByState[state].Remove(this);
            state = value;
            PizzasByState[state].Add(this);
        }
    }

    public float Points = 0;
    

    private List<Flavor> flavors = new List<Flavor>();
    public IReadOnlyList<Flavor> Flavors => flavors.AsReadOnly();

    public Pizza(string name, Sprite image, Difficulty difficulty, List<Flavor> availableFlavors)
    {
        Name = name;
        Image = image;
        Difficulty = difficulty;
        AvailableFlavors = availableFlavors;
        SetFlavors();
        Pizzas.Add(this);
        PizzasByState[state].Add(this);
    }

    public Pizza(PizzaData pizzaData)
    {
        Name = pizzaData.Name;
        Image = pizzaData.Image;
        Difficulty = new Difficulty(pizzaData.Difficulty);
        AvailableFlavors = pizzaData.AvailableFlavors;
        SetFlavors();
        Pizzas.Add(this);
        PizzasByState[state].Add(this);
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


public enum PizzaState
{
    Pending,
    Rejected,
    Matched,
    Chatted,
}

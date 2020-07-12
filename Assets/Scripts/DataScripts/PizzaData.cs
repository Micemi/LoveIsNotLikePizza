using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Pizza")]
public class PizzaData : ScriptableObject
{
    public static List<PizzaData> Pizzas = new List<PizzaData>();

    public string Name;
    public Sprite Image;
    public DifficultyData Difficulty;
    [Tooltip("Son los flavors disponibles para que la pizza randomice. No puede ser menor que la cantidad de la dificultad. " +
             "SelectFlavors elije una cantidad (de Difficulty) según esta lista.")]
    public List<Flavor> AvailableFlavors = new List<Flavor>();
    
    private void OnEnable()
    {
        Pizzas.Add(this);
    }

    private void OnDisable()
    {
        Pizzas.Remove(this);
    }

    private void OnValidate()
    {
        if (Difficulty != null && Difficulty.FlavorsQuantity > AvailableFlavors.Count)
            Debug.LogError($"{nameof(AvailableFlavors)} can't have less than {Difficulty.FlavorsQuantity} because of the set difficulty.");
    }
}

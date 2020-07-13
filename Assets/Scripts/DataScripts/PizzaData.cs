using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Pizza")]
public class PizzaData : ScriptableObject
{

    #region Static Collections

    private static List<PizzaData> pizzas;
    public static List<PizzaData> Pizzas
    {
        get
        {
            if (pizzas == null)
                ReloadPizzas();

            return pizzas;
        }
    }

    [MenuItem("Love Pizza/Refresh Pizza Database"), RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void ReloadPizzas()
    {
        pizzas = new List<PizzaData>(Resources.LoadAll<PizzaData>("Data/Pizzas"));
    }

    #endregion

    public string Name;
    public Sprite Image;
    public DifficultyData Difficulty;
    [Tooltip("Son los flavors disponibles para que la pizza randomice. No puede ser menor que la cantidad de la dificultad. " +
             "SelectFlavors elije una cantidad (de Difficulty) según esta lista.")]
    public List<Flavor> AvailableFlavors = new List<Flavor>();

    private void OnValidate()
    {
        if (Difficulty != null && Difficulty.FlavorsQuantity > AvailableFlavors.Count)
            Debug.LogError($"{nameof(AvailableFlavors)} can't have less than {Difficulty.FlavorsQuantity} because of the set difficulty.");
    }
}

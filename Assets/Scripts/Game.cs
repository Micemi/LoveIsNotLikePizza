using System.Collections.Generic;

public class Game
{
    private static Game current;
    public static Game Current => current ?? (current = NewGame());

    public static Game NewGame()
    {
        current = new Game();
        return current;
    }

    public List<Pizza> AllPizzas => Pizza.Pizzas;
    public List<Pizza> RemainingPizzas => Pizza.PizzasByState[PizzaState.Pending];
    public List<Pizza> ChattedWithPizzas => Pizza.PizzasByState[PizzaState.Chatted];
    public List<Pizza> MatchedPizzas => Pizza.PizzasByState[PizzaState.Matched];

    private Game()
    {
        Pizza.ClearPizzasCollections();
        for (int i = 0; i < PizzaData.Pizzas.Count; i++)
        {
            // While we don't use this, by instancing a Pizza it gets
            // added to the pizza list (see the Constructor)
            Pizza pizza = new Pizza(PizzaData.Pizzas[i]);
        }
    }
}

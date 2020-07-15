using System.Collections.Generic;
using UnityEngine;

public class PointsSummaryPresenter : MonoBehaviour
{
    [SerializeField]
    private Transform pointsContainer;

    [SerializeField]
    private PointsPresenter pointsPrefab;

    private readonly List<PointsPresenter> pointsPresenters = new List<PointsPresenter>();


    private void Awake()
    {
        for (int i = 0; i < Game.Current.AllPizzas.Count; i++)
        {
            PointsPresenter pointsPresenter = Instantiate(pointsPrefab, pointsContainer);
            pointsPresenters.Add(pointsPresenter);
            pointsPresenter.gameObject.SetActive(false);
        }
    }
    
    // for testing only
    [ContextMenu("MarkAllPizzasAsChatted")]
    private void MarkAllPizzasAsChatted()
    {
        foreach (Pizza pizza in Game.Current.AllPizzas)
            pizza.State = PizzaState.Chatted;
    }

    [ContextMenu("Show")]
    public void Show()
    {
        List<Pizza> pizzasChattedWith = Game.Current.ChattedWithPizzas;
        for (int i = 0; i < pizzasChattedWith.Count; i++)
        {
            pointsPresenters[i].SetPizza(pizzasChattedWith[i]);
            pointsPresenters[i].gameObject.SetActive(true);
        }
    }

    [ContextMenu("Hide")]
    public void Hide()
    {
        for (int i = 0; i < pointsPresenters.Count; i++)
        {
            pointsPresenters[i].gameObject.SetActive(false);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PointsSummaryPresenter : MonoBehaviour
{
    [Header("Total points")]
    [SerializeField]
    private TextMeshProUGUI totalPointsText;

    [SerializeField]
    private TextMeshProUGUI reputazzioneText;

    [Header("Points per Pizza")]
    [SerializeField]
    private Transform pointsContainer;

    [SerializeField]
    private ScrollRect pointsScroller;

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
        {
            pizza.State = PizzaState.Chatted;
            pizza.Points = Random.Range(0, 10000);
        }
    }

    [ContextMenu("Show")]
    public void Show()
    {
        List<Pizza> pizzasChattedWith = Game.Current.ChattedWithPizzas;
        pizzasChattedWith.Sort((p, q) => q.Points.CompareTo(p.Points));
        for (int i = 0; i < pizzasChattedWith.Count; i++)
        {
            pointsPresenters[i].SetPizza(pizzasChattedWith[i]);
            pointsPresenters[i].gameObject.SetActive(true);
        }
        StartCoroutine(SetScrollerToTop());

        float points = pizzasChattedWith.Sum(pizza => pizza.Points);
        totalPointsText.text = points.ToString("0") + " °c";
        reputazzioneText.text = ReputazzioneData.GetReputazzione(points);
    }

    [ContextMenu("Hide")]
    public void Hide()
    {
        for (int i = 0; i < pointsPresenters.Count; i++)
        {
            pointsPresenters[i].gameObject.SetActive(false);
        }
    }

    private IEnumerator SetScrollerToTop()
    {
        yield return null; // Wait 1 frame
        yield return null; // Wait 1 frame
        pointsScroller.verticalNormalizedPosition = 1;
    }
}

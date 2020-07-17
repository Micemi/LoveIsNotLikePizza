using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
public class AddPointsToText : MonoBehaviour {
    [SerializeField]
    private ChatPresenter chatPresenter;
    [SerializeField]
    private TextMeshProUGUI TMPtext;

    private float endPointValue;
    private float tempValue = 0;
    private float V=10; //  redundant

    [SerializeField]
    private float time = 3;

    [SerializeField]
    private Image fill;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //tempValue = tempValue/endPointValue;
        this.V=endPointValue/this.time;
        if(tempValue < endPointValue) tempValue += this.V*Time.deltaTime;   //  crece

        tempValue = Mathf.Min(endPointValue, tempValue);
        TMPtext.text = tempValue.ToString("0");

        fill.fillAmount = tempValue/endPointValue;
    }

    /*
    private void OnEnable() {
        chatPresenter.OnChatFinished.AddListener(UpdatePoints);
    }
    private void OnDisable(){
        chatPresenter.OnChatFinished.RemoveListener(UpdatePoints);
    }
    */

    public void UpdatePoints()
    {
        //  TMPtext.text = chatPresenter.Pizza.Points.ToString();
        endPointValue = chatPresenter.Pizza.Points;
    }

}
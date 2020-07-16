using UnityEngine;
using DG.Tweening;

public class RepeatBG : MonoBehaviour {
    [SerializeField]
    private GameObject bgPanel;
    
    void Start()
    {
        GameObject newBG = GameObject.Instantiate(bgPanel, transform);  //  simple instantiation from menu of prefab
        newBG.transform.SetAsFirstSibling();    //  Sets its transform so its the first of all childs
        //newBG.GetComponent<RectTransform>().localPosition.DOAnchorPos(Vector2.zero,0.25f);    //  continuar que no anda
    }
    private void LateUpdate() {
        
    }

}
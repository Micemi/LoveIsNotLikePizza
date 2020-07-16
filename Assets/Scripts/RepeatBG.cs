using UnityEngine;

public class RepeatBG : MonoBehaviour {
    [SerializeField]
    private GameObject bgPanel;
    void Start()
    {
        GameObject newBG = GameObject.Instantiate(bgPanel, transform);  //  simple instantiation from menu of prefab
        newBG.transform.SetAsFirstSibling();    //  Sets its transform so its the first of all childs
    }
    private void LateUpdate() {
        
    }

}
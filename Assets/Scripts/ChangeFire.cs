using UnityEngine;
using UnityEngine.UI;
using System.Collections;   //  For the coroutine testing

public class ChangeFire : MonoBehaviour {
    [SerializeField]
    private Sprite[] firesType;
    private Image fireComponent;

    private void Start() {
        fireComponent = GetComponent<Image>();
        //  StartCoroutine(TestCoroutine());
    }

    public void GoCold(){
        fireComponent.sprite = firesType[0];
    }
    public void GoWarm(){
        fireComponent.sprite = firesType[1];
    }
    public void GoHOTTT(){
        fireComponent.sprite = firesType[2];
    }



    //  Dummy code for testing:
    IEnumerator TestCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        GoCold();
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Coroutine at timestamp : " + Time.time);
        GoWarm();
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Coroutine at timestamp : " + Time.time);
        GoHOTTT();

    }
    

}
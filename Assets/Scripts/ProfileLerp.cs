using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileLerp : MonoBehaviour
{
    private bool shouldLerp = false;

    public float timeStartedLerping;
    public float lerpTime;

    public Vector2 endPosition;
    public Vector2 startPosition;

    private void StartLerping()
    {
        timeStartedLerping = Time.time;

        shouldLerp = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartLerping(); 

        //Start the coroutine we define below named ExampleCoroutine.
        StartCoroutine(ExampleCoroutine());   
    }
    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(lerpTime);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);

        shouldLerp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldLerp)
        {
            transform.position = Lerp(startPosition, endPosition, timeStartedLerping, lerpTime);
        }
       

    }

    public Vector3 Lerp(Vector3 start,Vector3 end,float timeStartedLerping,float lerpTime = 1)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;

        float percentageComplete = timeSinceStarted / lerpTime;

        var result = Vector3.Lerp(start, end, percentageComplete);

        return result;
    }
}

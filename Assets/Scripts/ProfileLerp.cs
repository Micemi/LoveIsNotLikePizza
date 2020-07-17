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
 
    public Transform ProfileStart;
    public Transform ProfileEnd;

    public Transform WhatMove;

    private void StartLerping(Transform Whatmove)
    {
        timeStartedLerping = Time.time;

        shouldLerp = true;
        WhatMove = Whatmove;
        
        //Start the coroutine we define below named ExampleCoroutine.
        StartCoroutine(ExampleCoroutine());   
    }

    // Start is called before the first frame update (.)Y(.)
    void OnEnable()
    {
        WhenSpawn.EventSpawn += StartLerping;

    }

    void OnDisable()
    {
        WhenSpawn.EventSpawn -= StartLerping;
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

        SwiperManagerBtn.event_btn();
        Debug.Log("DOOOU?");
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldLerp)
        {
           WhatMove.position = Lerp(ProfileStart.position,ProfileEnd.position, timeStartedLerping, lerpTime);
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

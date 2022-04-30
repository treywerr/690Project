using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthController : MonoBehaviour
{
    private static int currentRequestPriority = -1;
    private static float waitTime = 0f;
    private static bool waiting = false;

    void Update()
    {
        if (waiting)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                waiting = false;
            }
        }
        else
        {
            currentRequestPriority = -1;
        }
    }

    public static void Request(float mult, int priority)
    {
        if (priority >= currentRequestPriority)
        {
            It4Enemy.hearMult = mult;
            currentRequestPriority = priority;
            Debug.Log("Request " + It4Enemy.hearMult + " priority " + priority);
        }
    }

    public static void TimeRequest(float mult, int priority, float duration)
    {
        if (waiting)
        {
            if (priority > currentRequestPriority)
            {
                currentRequestPriority = priority;
                It4Enemy.hearMult = mult;
                waitTime = duration;
            }
        }
    }

    /*IEnumerator FulfillRequest(float duration)
    {
        waiting = true;
        yield return new WaitForSeconds(duration);
        waiting = false;
    }*/
}

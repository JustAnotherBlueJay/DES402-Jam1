using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    //Action to connect to in the script using the timer
    public Action OnTimeout;

    //length of the timer
    [SerializeField] private float waitTime;
    private float timeLeft;

    //if the timer auto repeats or not
    [SerializeField] private bool oneShot = true;

    //if the timer is currently ticking
    private bool paused = true;

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            timeLeft -= Time.deltaTime;
        }

        if (timeLeft < 0 && !paused)
        {
            //Timeout
            Timeout();
        }
    }

    //when the timer ends
    private void Timeout()
    {
        //call the function in the script using the timer
        OnTimeout.Invoke();

        //if its a one shot stop the timer
        if (oneShot)
        {
            paused = true;
        }

        //if its not a one shot start the timer again
        else if (!oneShot)
        {
            StartTimer(waitTime);
        }

    }

    //call this to start the timer
    public void StartTimer(float seconds = -1)
    {
        //if a time is passed then set that time, otherwise time left is equal to whatever is in wait time
        if (seconds != -1)
        {
            waitTime = seconds;
        }

        timeLeft = waitTime;
        paused = false;
    }

    //used to stop a repeating timer
    public void StopTimer()
    {
        paused = true;
    }

    public bool IsStopped()
    {
        return paused;
    }


}

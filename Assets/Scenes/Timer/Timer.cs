using UnityEngine;

public class Timer
{
    float startTime;
    float finishTime;

    public void Begin(float duration)
    {
        startTime = Time.time;
        finishTime = startTime + duration;
    }

    public bool IsFinished()
    {
        return Time.time > finishTime;
    }

    public float GetProgress()
    {
        float percent = Mathf.InverseLerp(startTime, finishTime, Time.time);
        return percent;
    }

}
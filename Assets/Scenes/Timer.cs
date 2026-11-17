using UnityEngine;

public class Timer
{
	float startTime;
	float dueTime;
	private float duration;

	public Timer() {}

	public void Start(float duration)
	{
		this.duration = duration;
		startTime = Time.time;
		dueTime = startTime + this.duration;
	}

	public bool IsFinished()
	{
		return Time.time >= dueTime;
	}

	public float GetProgress()
	{
		float percent = Mathf.InverseLerp(startTime, dueTime, Time.time); // takes A-B and a value in the middle, gives us a %
		return Mathf.Clamp(percent, 0f, 1f); // if value is smaller or bigger than min-max, returns the min or max value instead
	}
}
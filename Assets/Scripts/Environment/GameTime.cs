using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour 
{
	public float gameTime = 0.0f; 
	
	public float timeInDay;
	public float halfDay;
	public int daysPassed;
	public float addTime; //Time to add every second
	public int timeSpeedMultiplier;
	
	
	public int currentHour;
	public int currentMinute;
	string timeSuffix;
	
	void Awake()
	{
		halfDay = timeInDay * 0.5f;
	}
	
	void Update()
	{
		gameTime += addTime * timeSpeedMultiplier * Time.deltaTime;
		TimeLimitCheck();
		ChangeToHourMinutes();
		/*Debug.Log("Current Time: " + gameTime + ", daysPassed: " + daysPassed + "." 
		+ " Time: " + currentHour + ":" + ((currentMinute < 10)?currentMinute.ToString("D2"):currentMinute.ToString ()) 
		+ " " + timeSuffix
		);
		*/
		
	}
	void TimeLimitCheck()
	{
		if (gameTime >= timeInDay)
		{
			gameTime = 0.0f + (gameTime - timeInDay); 
			daysPassed++;
		}
	}
	void ChangeToHourMinutes()
	{
		if (gameTime <= halfDay)
		{
			timeSuffix = "AM";
			currentHour = (int)((gameTime / 60.0f));
			currentMinute = (int)(gameTime % 60.0f);
		}
		else 
		{
			timeSuffix = "PM";
			currentHour = (int)((gameTime / 60.0f) - 12.0f);
			currentMinute = (int)(gameTime % 60.0f);
		}
	}

}

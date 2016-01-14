using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour 
{
	public Light sun;
	Light sunLight; // Light Component of Sun
	public Color ambientLightNightColor;
	public Color ambientLightMorningColor;
	public Color ambientLightMiddayColor;
	public Color ambientLightDuskColor;
	
	public AnimationCurve duskToNightTrans;
	
	public GameTime timeObject;
	
	private float morningTime;
	private float middayTime;
	private float nightTime;
	private float duskTime;
	
	void Start()
	{
		nightTime = 0.0f;
		morningTime = timeObject.timeInDay / 4.0f;
		middayTime = timeObject.halfDay;
		duskTime = timeObject.halfDay + morningTime;
		sunLight = sun.GetComponent<Light>();
		InitializeDefaultLight();
		
	}
	
	void LateUpdate()
	{
		float t = timeObject.gameTime;
		if (t < morningTime && t >= nightTime + 5.0f*60.0f)
		{
			float d = 5.0f*60.0f; //delay the lerping; please refactor to actual adjustable property later
			RenderSettings.ambientLight = Color.Lerp (ambientLightNightColor, ambientLightMorningColor, (t-d) / (morningTime - d));
		}
		else if (t < middayTime && t >= morningTime + 2.0f * 60.0f)
		{
			float d = 2.0f*60.0f;
			RenderSettings.ambientLight = Color.Lerp (ambientLightMorningColor, ambientLightMiddayColor, (t - morningTime - d) / (middayTime - morningTime - d));
		}
		else if (t < duskTime && t >= middayTime + 5.0f*60.0f)
		{
			float d = 5.0f*60.0f;
			RenderSettings.ambientLight = Color.Lerp (ambientLightMiddayColor, ambientLightDuskColor, (t - middayTime - d) / (duskTime - middayTime - d));
		}
		else if (t < timeObject.timeInDay && t >= duskTime)
		{
			RenderSettings.ambientLight = Color.Lerp (ambientLightDuskColor, ambientLightNightColor, duskToNightTrans.Evaluate( (t - duskTime) / (timeObject.timeInDay - duskTime)));
		}
		RotateSun(t, sun);
	}
	
	void InitializeDefaultLight()
	{
		float t = timeObject.gameTime;
		if (t < morningTime && t >= nightTime)
		{
			RenderSettings.ambientLight = ambientLightNightColor;
			sunLight.intensity = 0.2f;
		}
		else if (t < middayTime && t >= morningTime)
		{
			RenderSettings.ambientLight = ambientLightMorningColor;
			sunLight.intensity = 0.45f;
		}
		else if (t < duskTime && t >= middayTime)
		{
			RenderSettings.ambientLight = ambientLightMiddayColor;
			sunLight.intensity = 0.8f;
		}
		else if (t < timeObject.timeInDay && t >= duskTime)
		{
			RenderSettings.ambientLight = ambientLightDuskColor;
			sunLight.intensity = 0.35f;
		}
	}
	void RotateSun(float currentTime, Light sunObject)
	{
		if (currentTime < morningTime && currentTime >= nightTime + 4.5f * 60.0f) 
		{
			float d = 4.5f * 60.0f;
			//sun.transform.localEulerAngles = new Vector3(0,((currentTime - d/middayTime - d)*90+270),0);
			sun.transform.localEulerAngles = Vector3.Lerp(new Vector3(0,270,0), new Vector3(0,300,0), (currentTime - d)/(morningTime - d));
			sunLight.intensity = Mathf.Lerp(0.2f, 0.45f, (currentTime - d)/(morningTime - d));
		}
		else if (currentTime < middayTime && currentTime >= morningTime + 1.0f * 60.0f) 
		{
			float d = 1.0f * 60.0f;
			//sun.transform.localEulerAngles = new Vector3(0,((currentTime - d/middayTime - d)*90+270),0);
			sun.transform.localEulerAngles = Vector3.Lerp(new Vector3(0,300,0), new Vector3(0,315,0), (currentTime - morningTime - d)/(middayTime - morningTime - d));
			sunLight.intensity = Mathf.Lerp(0.45f, 0.80f, (currentTime - morningTime - d)/(middayTime - morningTime - d));
		} 
		else if (currentTime < duskTime && currentTime >= middayTime + 5.0f * 60.0f)
		{
			float d = 5.0f * 60.0f;
			//sun.transform.localEulerAngles = new Vector3(0,((currentTime - d/timeObject.timeInDay - d)*180+90),0);
			sun.transform.localEulerAngles = Vector3.Lerp(new Vector3(0,315,0), new Vector3(0,360,0), (currentTime - middayTime - d)/(duskTime - middayTime - d));
			sunLight.intensity = Mathf.Lerp(0.80f, 0.35f, (currentTime - middayTime - d)/(duskTime - middayTime - d));
		}
		else if (currentTime < timeObject.timeInDay && currentTime >= duskTime)
		{
			//sun.transform.localEulerAngles = new Vector3(0,((currentTime/timeObject.timeInDay)*180+90),0);
			//sun.transform.localEulerAngles = Vector3.Lerp(new Vector3(0,0,0), new Vector3(0,270,0), currentTime/timeObject.timeInDay);
			sun.transform.localEulerAngles = Vector3.Lerp(new Vector3(0,0,0), new Vector3(0,270,0), duskToNightTrans.Evaluate ((currentTime - duskTime)/(timeObject.timeInDay - duskTime)));
			sunLight.intensity = Mathf.Lerp(0.35f, 0.2f, ((currentTime - duskTime)/(timeObject.timeInDay - duskTime)));
		}
	}
}

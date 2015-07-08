using UnityEngine;
using System.Collections;

public class LightCircle : InteractiveObject {

	public Light light;
	public SphereCollider sphere;
	public float shrinkTime;

	private bool refreshed;

	// Use this for initialization
	public override void StartMe () 
	{
		//StartCoroutine(Shrink (shrinkTime));
		FullLight ();
	}
	
	// Update is called once per frame
	public override void UpdateMe () 
	{
		base.UpdateMe();
		if(Input.GetKeyDown(KeyCode.R)) {
			StartCoroutine(Refresh(110.0f, 17.0f, 0.3f, true));
		}
	}

	public override void DoActivation ()
	{
		sphere.enabled = true;
		StartCoroutine(Refresh(110.0f, 17.0f, 0.3f, true));
		isActivated = false;
	}

	public void FullLight ()
	{
		StartCoroutine(Refresh(110.0f, 17.0f, 0.3f, false));
		sphere.enabled = false;
	}

	private IEnumerator Shrink (float time)
	{
		bool shrinking = true;
		float elapsedTime = 0.0f;
		float angle = light.spotAngle;
		float targetAngle = 10.0f;
		float radius = sphere.radius;
		float targetRadius = 0.5f;
		while(shrinking) {
			if(refreshed)
				break;
			elapsedTime += Time.deltaTime;
			float newAngle = Mathf.Lerp (angle, targetAngle, elapsedTime / time);
			light.spotAngle = newAngle;

			float newRadius = Mathf.Lerp (radius, targetRadius, elapsedTime / time);
			sphere.radius = newRadius;

			if(light.spotAngle == targetAngle && sphere.radius == targetRadius)
				shrinking = false;

			yield return null;
		}
	}

	private IEnumerator Refresh(float targetAngle, float targetRadius, float time, bool shrink)
	{
		refreshed = true;
		bool refreshing = true;
		float elapsedTime = 0.0f;
		float angle = light.spotAngle;
		float radius = sphere.radius;
		while(refreshing) {
			elapsedTime += Time.deltaTime;
			float newAngle = Mathf.Lerp (angle, targetAngle, elapsedTime / time);
			light.spotAngle = newAngle;
			
			float newRadius = Mathf.Lerp (radius, targetRadius, elapsedTime / time);
			sphere.radius = newRadius;
			
			if(light.spotAngle == targetAngle && sphere.radius == targetRadius)
				refreshing = false;
			
			yield return null;
		}
		refreshed = false;
		if(shrink)
			StartCoroutine(Shrink(shrinkTime));
	}


}

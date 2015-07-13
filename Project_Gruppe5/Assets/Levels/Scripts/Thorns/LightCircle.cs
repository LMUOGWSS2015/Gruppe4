using UnityEngine;
using System.Collections;

public class LightCircle : InteractiveObject {

	public Light light;
	public SphereCollider sphere;
	public float shrinkTime;
	public Light directionalLight;

	private bool refreshed;
	private float lightIntensity;
	private bool fullLight;

	// Use this for initialization
	public override void StartMe () 
	{
		//StartCoroutine(Shrink (shrinkTime));
		lightIntensity = directionalLight.intensity;
		FullLight ();
	}
	
	// Update is called once per frame
	public override void UpdateMe () 
	{
		base.UpdateMe();
		if(Input.GetKeyDown(KeyCode.R)) {
			StartCoroutine(Refresh(110.0f, 17.0f, 0.3f, true));
		}

		if(Player.Instance.isDead () && sphere.enabled)
			gameObject.SetActive(false);
	}

	public override void DoActivation ()
	{
		sphere.enabled = true;
		StartCoroutine(Refresh(110.0f, 17.0f, 0.3f, true));
		isActivated = false;
	}

	public void FullLight ()
	{
		gameObject.SetActive(true);
		StartCoroutine(Refresh(110.0f, 17.0f, 0.3f, false));
		sphere.enabled = false;
	}

	private IEnumerator Shrink (float time, bool kill)
	{
		bool shrinking = true;
		float elapsedTime = 0.0f;
		float angle = light.spotAngle;
		float targetAngle = 1f;
		float radius = sphere.radius;
		float targetRadius = 2.0f;
		float intensity = directionalLight.intensity;
		fullLight = false;

		while(shrinking) {
			if(refreshed)
				break;
			elapsedTime += Time.deltaTime;
			float newAngle = Mathf.Lerp (angle, targetAngle, elapsedTime / time);
			light.spotAngle = newAngle;

			float newRadius = Mathf.Lerp (radius, targetRadius, elapsedTime / time);
			sphere.radius = newRadius;

			float newLightIntensity = Mathf.Lerp (intensity, 0, elapsedTime / time);
			directionalLight.intensity = newLightIntensity;

			if(light.spotAngle == targetAngle && sphere.radius == targetRadius) {
				shrinking = false;
				if(kill)
					Player.Instance.Kill ();
			}

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
		float intensity = directionalLight.intensity;
		while(refreshing) {
			elapsedTime += Time.deltaTime;
			float newAngle = Mathf.Lerp (angle, targetAngle, elapsedTime / time);
			light.spotAngle = newAngle;
			
			float newRadius = Mathf.Lerp (radius, targetRadius, elapsedTime / time);
			sphere.radius = newRadius;

			float newLightIntensity = Mathf.Lerp (intensity, lightIntensity, elapsedTime / time);
			directionalLight.intensity = newLightIntensity;
			
			if(light.spotAngle == targetAngle && sphere.radius == targetRadius)
				refreshing = false;
			
			yield return null;
		}
		refreshed = false;
		if(shrink)
			StartCoroutine(Shrink(shrinkTime, true));
		else
			sphere.enabled = false;
	}


}

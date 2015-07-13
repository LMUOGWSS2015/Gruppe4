using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VanishingObject : MyMonoBehaviour {

	public bool instantVanish;

	private Renderer[] renderers;
	private Collider[] colliders;

	private bool shrinking;
	private bool growing;

	void Start ()
	{
		StartMe ();
	}

	public override void StartMe ()
	{
		if(transform.tag == "ShowingObject")
			Disable();
	}
	private void SetupRenderers ()
	{
		renderers = gameObject.GetComponentsInChildren<Renderer>();
	}

	private void SetupColliders () 
	{
		colliders = gameObject.GetComponentsInChildren<Collider>();
	}

	public void Disable ()
	{
		if(instantVanish) {
			if(renderers == null)
				SetupRenderers ();

			if(colliders == null)
				SetupColliders ();

			foreach(Renderer renderer in renderers)
				renderer.enabled = false;

			foreach(Collider collider in colliders)
				collider.isTrigger = true;

		} else {
			StartCoroutine(Shrink(4.0f));
		}
	}

	public void Enable ()
	{
		if(instantVanish) {
			if(renderers == null)
				SetupRenderers ();

			if(colliders == null)
				SetupColliders ();

			foreach(Renderer renderer in renderers)
				renderer.enabled = true;

			foreach(Collider collider in colliders)
				collider.isTrigger = false;

			if(transform.tag == "ShowingObject") {		
				GetComponent<Collider>().isTrigger = true;
			}

		} else {
			StartCoroutine(Grow(2.0f));
		}
	}

	private IEnumerator Shrink (float speed)
	{
		growing = false;
		shrinking = true;
		Vector3 targetScale = Vector3.zero;
		while(shrinking) {
			Vector3 newScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);
			transform.localScale = newScale;
			
			if(transform.localScale == targetScale) 
				shrinking = false;
			
			yield return null;
		}
	}

	private IEnumerator Grow (float speed)
	{
		shrinking = false;
		growing = true;
		Vector3 targetScale = Vector3.one;
		while(growing) {
			Vector3 newScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);
			transform.localScale = newScale;
			
			if(transform.localScale == targetScale)
				growing = false;
			
			yield return null;
		}
	}
	
}

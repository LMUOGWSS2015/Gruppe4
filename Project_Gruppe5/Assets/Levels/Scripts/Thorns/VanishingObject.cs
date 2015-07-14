using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Object that either vanishes or shows when the light hits it
public class VanishingObject : MyMonoBehaviour {

	//vanishes or shows instantly without an animation
	public bool instantVanish;

	//list of all renderers of the obejct and its childs
	private Renderer[] renderers;
	//list of all colliders of the object and its childs
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

	//Get all the renderers in all children of the object
	private void SetupRenderers ()
	{
		renderers = gameObject.GetComponentsInChildren<Renderer>();
	}

	//Get all the colliders in all children of the object
	private void SetupColliders () 
	{
		colliders = gameObject.GetComponentsInChildren<Collider>();
	}

	//Triggers the object and shows it
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

	//Triggers the object and hides it
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

	//shrinks the object to zero over time
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

	//grows the object to full size over time
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

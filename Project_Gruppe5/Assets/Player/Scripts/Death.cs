using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

	private float onTime = 0.8f;
	private float offTime = 0.1f;
	private Renderer[] renderers;
	private bool triggered;

	// Use this for initialization
	void Start () 
	{
		SetupRenderers();
	}

	private void SetupRenderers ()
	{
		renderers = gameObject.GetComponentsInChildren<Renderer>();
	}

	public void Trigger ()
	{
		triggered = true;
		StartCoroutine (CoTrigger());
	}

	private IEnumerator CoTrigger()
	{
		yield return new WaitForSeconds (onTime);
		//off
		SwitchRenderers ();
		yield return new WaitForSeconds (offTime);
		//on
		SwitchRenderers ();
		yield return new WaitForSeconds (onTime);
		//off
		SwitchRenderers ();
		yield return new WaitForSeconds (offTime);
		//on
		SwitchRenderers ();
		yield return new WaitForSeconds (onTime);
		//off
		SwitchRenderers ();
		yield return new WaitForSeconds (offTime);
		Player.Instance.Kill();
		//on
		SwitchRenderers ();
		yield return new WaitForSeconds (onTime);

		triggered = false;
	}

	private void SwitchRenderers ()
	{
		foreach (Renderer r in renderers)
			r.enabled = !r.enabled;
	}
}

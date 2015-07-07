using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

	public float time;
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
		yield return new WaitForSeconds (time);
		//off
		SwitchRenderers ();
		yield return new WaitForSeconds (time);
		//on
		SwitchRenderers ();
		yield return new WaitForSeconds (time);
		//off
		SwitchRenderers ();
		yield return new WaitForSeconds (time);
		//on
		SwitchRenderers ();
		yield return new WaitForSeconds (time);
		//off
		SwitchRenderers ();
		yield return new WaitForSeconds (time);
		Player.Instance.Kill();
		//on
		SwitchRenderers ();
		yield return new WaitForSeconds (time);

		triggered = false;
	}

	private void SwitchRenderers ()
	{
		foreach (Renderer r in renderers)
			r.enabled = !r.enabled;
	}
}

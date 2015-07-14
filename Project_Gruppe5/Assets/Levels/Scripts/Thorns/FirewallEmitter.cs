using UnityEngine;
using System.Collections;

//Single firestream used in a Firewall
public class FirewallEmitter : MonoBehaviour {

	public Light light;
	public ParticleSystem particleSystem;

	public void Activate ()
	{
		light.enabled = true;
		particleSystem.gameObject.SetActive(true);
	}

	public void Deactivate ()
	{
		light.enabled = false;
		particleSystem.gameObject.SetActive(false);
	}

}

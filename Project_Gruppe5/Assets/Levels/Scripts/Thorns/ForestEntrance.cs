using UnityEngine;
using System.Collections;

public class ForestEntrance : MonoBehaviour {

	public LightCircle light;
	public bool isEntrance;

	void OnTriggerEnter (Collider other)
	{
		if(other.tag == "Player") {
			if(isEntrance)
				light.DoActivation();
			else
				light.FullLight ();
		}
	}
}

using UnityEngine;
using System.Collections;

//Entrance of the forest in the thorns level
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

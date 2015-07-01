using UnityEngine;
using System.Collections;

public class LightCircleCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.tag == "VanishingObject") {
			other.GetComponent<VanishingObject>().Disable();
		}
		if(other.tag == "ShowingObject") {
			other.GetComponent<VanishingObject>().Enable();
		}
	}
	
	void OnTriggerExit (Collider other)
	{
		if(other.tag == "VanishingObject") {
			other.GetComponent<VanishingObject>().Enable();
		}
		if(other.tag == "ShowingObject") {
			other.GetComponent<VanishingObject>().Disable();
		}
	}
}

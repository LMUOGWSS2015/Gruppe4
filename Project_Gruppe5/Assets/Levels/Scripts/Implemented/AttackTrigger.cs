using UnityEngine;
using System.Collections;

public class AttackTrigger : MonoBehaviour {

	public Aggressor aggressor;

	void OnTriggerEnter (Collider other) 
	{
		Debug.Log ("trig");
		if(other.tag == "Player") {
			aggressor.Attack(other.transform);
		}
	}
}

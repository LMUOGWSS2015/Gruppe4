using UnityEngine;
using System.Collections;

public class Pushback : MyMonoBehaviour {

	public float power = 800.0f;

	void OnCollisionEnter (Collision other)
	{
		if(other.gameObject.tag == "Player") {
			Vector3 pushbackPoint = new Vector3(other.contacts[0].normal.x, other.transform.position.y - 1, other.contacts[0].normal.z);
			Player.Instance.gameObject.GetComponent<Rigidbody>().AddExplosionForce(power, pushbackPoint, 20.0f);
		}
	}	
}
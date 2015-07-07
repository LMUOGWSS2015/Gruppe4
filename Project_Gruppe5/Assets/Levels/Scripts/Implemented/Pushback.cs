using UnityEngine;
using System.Collections;

public class Pushback : MyMonoBehaviour {

	public float power = 800.0f;

	void OnCollisionEnter (Collision other)
	{
		if(other.gameObject.tag == "Player") {
			//Vector3 pushbackPoint = new Vector3(other.contacts[0].normal.x * -1, 0.0f, other.contacts[0].normal.z * -1);
			//Player.Instance.gameObject.GetComponent<Rigidbody>().AddExplosionForce(power, pushbackPoint, 20.0f);

			Vector3 pushback = new Vector3(other.contacts[0].normal.x * -1 * power, other.contacts[0].normal.y * -1 * (power), other.contacts[0].normal.z * -1 * power);
			Player.Instance.Hit(pushback);
		}
	}	
}
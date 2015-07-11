using UnityEngine;
using System.Collections;

public class HarmfulObject : MyMonoBehaviour {

	void OnCollisionEnter(Collision other) 
	{
		if(other.transform.tag == "Player" && !Player.Instance.isDead()) {
			Player.Instance.KillByObject();
		}
	}
}

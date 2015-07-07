using UnityEngine;
using System.Collections;

public class HarmfulObject : MyMonoBehaviour {

	void OnCollisionEnter(Collision other) 
	{
		if(other.transform.tag == "Player") {
			Debug.Log ("Kill");
			Player.Instance.KillByObject();
		}
	}
}

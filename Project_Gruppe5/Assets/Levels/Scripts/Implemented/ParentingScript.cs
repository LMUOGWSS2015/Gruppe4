using UnityEngine;
using System.Collections;

public class ParentingScript : MonoBehaviour {


	public GameObject parent;

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "Player"){ 
			col.gameObject.transform.SetParent(parent.transform);
			}
	}
	void OnCollisionExit(Collision col) {
		if (col.gameObject.tag == "Player") {
			col.gameObject.transform.parent = null;
		}
	}
}

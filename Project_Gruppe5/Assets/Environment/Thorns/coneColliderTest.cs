using UnityEngine;
using System.Collections;

public class coneColliderTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll) {
		Debug.Log("triggered");
	}
}

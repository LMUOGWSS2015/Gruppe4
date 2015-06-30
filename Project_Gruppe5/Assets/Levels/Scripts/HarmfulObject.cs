using UnityEngine;
using System.Collections;

public class HarmfulObject : MyMonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Debug.Log (transform.name);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) 
	{
		Debug.Log ("Kill");
		if(other.transform.tag == "Player") {
			Player.Instance.Kill();
		}
	}
}

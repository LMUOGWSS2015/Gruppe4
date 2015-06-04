using UnityEngine;
using System.Collections;

public class LightFollow : MonoBehaviour {

	Vector3 up = new Vector3 (0,6,0);
	Vector3 lightrot = new Vector3(10,0,0);
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		GameObject bear = GameObject.Find("lowpolybear");
		transform.position = bear.transform.position+up;
		transform.eulerAngles = lightrot + bear.transform.eulerAngles;

	}
}

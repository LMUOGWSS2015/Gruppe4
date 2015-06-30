using UnityEngine;
using System.Collections;

public class GhostLantern : MonoBehaviour {

	public float maxRotation;
	public float speed;
	public Transform ghost;

	private Vector3 targetRotation;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		MoveLantern();
	}

	private void MoveLantern ()
	{
		float rotValue = Mathf.PingPong(Time.time * speed, maxRotation * 2) - maxRotation;
		Vector3 newRotation = new Vector3(transform.rotation.eulerAngles.x, ghost.rotation.eulerAngles.y + rotValue, ghost.rotation.eulerAngles.z);
		transform.rotation = Quaternion.Euler(newRotation);

	}
}

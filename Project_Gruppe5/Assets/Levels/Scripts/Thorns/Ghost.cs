using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ghost : MyMonoBehaviour {

	public List<Transform> wayPoints;
	public float speed;
	public float turningSpeed;

	private bool moving;
	private bool turning;
	private bool forward;
	private int currentPoint;

	// Use this for initialization
	void Start () 
	{
		forward = true;
		GameObject initialPoint = new GameObject();
		initialPoint.transform.position = transform.position;
		initialPoint.name = "Waypoint";
		wayPoints.Insert(0, initialPoint.transform);
		currentPoint = 1;

		StartCoroutine(Turning ());
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	private IEnumerator Move ()
	{
		Debug.Log ("Move: " + wayPoints[currentPoint].position);
		moving = true;
		while(moving) {
			Vector3 newPos = Vector3.MoveTowards(transform.position, wayPoints[currentPoint].position, Time.deltaTime * speed);
			transform.position = newPos;

			Debug.Log (newPos);

			if(transform.position == wayPoints[currentPoint].position) {
				moving = false;
				if(currentPoint == (wayPoints.Count - 1)) {
					forward = false;
				} else if(currentPoint == 0) {
					forward = true;
				}

				currentPoint = forward ? currentPoint + 1 : currentPoint - 1;

				StartCoroutine(Turning());
			}
			yield return null;
		}
	}

	private IEnumerator Turning () {
		turning = true;

		while(turning) {
			Vector3 direction = (wayPoints[currentPoint].position - transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation(direction);

			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turningSpeed);

			if(transform.rotation == lookRotation) {
				Debug.Log ("turning false");
				turning = false;
				StartCoroutine(Move ());
			}

			yield return null;
		}
	}
}

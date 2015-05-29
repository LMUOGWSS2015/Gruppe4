using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingPlatform : InteractivePhysicsObject {

	public float speed;
	public List<Transform> pathPoints;

	private int currentPoint = 1;
	private bool movingForward = true;
	private bool newActivation;

	private Vector3 directionVector;

	// Use this for initialization
	public override void StartMe () 
	{
		GameObject go = new GameObject();
		go.transform.position = transform.position;
		pathPoints.Insert(0, go.transform);
	}

	public override void DoActivation () 
	{

		if(newActivation && !movingForward) {
			currentPoint++;
			movingForward = true;
			newActivation = false;
		}

		rb.MovePosition(Vector3.MoveTowards(transform.position, pathPoints[currentPoint].position, Time.deltaTime * speed));

		if(movingForward && transform.position == pathPoints[currentPoint].position) {
			if(currentPoint == pathPoints.Count - 1) {
				movingForward = false;
				currentPoint--;
			} else {
				currentPoint++;
			}
		} else if(!movingForward && transform.position == pathPoints[currentPoint].position) {
			if(currentPoint == 0) { 
				movingForward = true;
				currentPoint++;
			} else {
				currentPoint--;
			}
		} 
	}

	public override void DoDeactivation ()
	{
		newActivation = true;
		if(movingForward) {
			currentPoint--;
			movingForward = false;
		}

		rb.MovePosition(Vector3.MoveTowards(transform.position, pathPoints[currentPoint].position, Time.deltaTime * speed));

		if(transform.position == pathPoints[currentPoint].position) {
			if(currentPoint == 0) { 
				movingForward = true;
				currentPoint++;
				EndDeactivation();
			} else {
				currentPoint--;
			}
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingPlatform : InteractivePhysicsObject {

	public bool randomize;
	public float speed;
	public List<Transform> pathPoints;

	private int currentPoint = 1;
	private bool movingForward = true;
	private bool newActivation = true;

	private Vector3 directionVector;

	private int lastPoint = -1;

	// Use this for initialization
	public override void StartMe () 
	{
		GameObject go = new GameObject();
		go.transform.position = transform.position;
		go.name = transform.name + "_StartingPoint";
		pathPoints.Insert(0, go.transform);

		if(pathPoints.Count < 3) {
			randomize = false;
		}
	}

	public override void DoActivation () 
	{
		if(pathPoints.Count > 1) {
			if(newActivation && !movingForward && !randomize) {
				currentPoint++;
				movingForward = true;
				newActivation = false;
			}

			if(randomize && transform.position == pathPoints[currentPoint].position) {
				int newPoint = currentPoint;

				while(newPoint == currentPoint || newPoint == lastPoint) {
					newPoint = Random.Range(0, pathPoints.Count);
				}

				lastPoint = currentPoint;
				currentPoint = newPoint;
			}

			rb.MovePosition(Vector3.MoveTowards(transform.position, pathPoints[currentPoint].position, Time.deltaTime * speed));

			if(movingForward && transform.position == pathPoints[currentPoint].position && !randomize) {
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
	}

	public override void DoDeactivation ()
	{
		if(pathPoints.Count > 1) {
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
}

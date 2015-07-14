using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Object that moves along a given path (more complex than SimpleMovingPlatform)
public class MovingPlatform : InteractivePhysicsObject {

	//moves randomly between the given path points
	public bool randomize;
	//speed of movement
	public float speed;
	//List of points that give the path of the object
	public List<Transform> pathPoints;
	//moves only one way (no automatic return)
	public bool oneWay;

	//current point on path
	private int currentPoint = 1;
	private bool movingForward = true;
	private bool newActivation = true;

	private Vector3 directionVector;

	private int lastPoint = -1;

	// Use this for initialization
	public override void StartMe () 
	{
		//create the initiating start position for returning
		GameObject go = new GameObject();
		go.transform.position = transform.position;
		go.name = transform.name + "_StartingPoint";
		pathPoints.Insert(0, go.transform);
		go.transform.SetParent (transform.parent);

		if(pathPoints.Count < 3) {
			randomize = false;
		}
	}

	//Start movement along the given path and return if wanted
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
					if(oneWay) {
						isActivated = false;
					} else {
						movingForward = false;
						currentPoint--;
					}
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

	//move back to original position
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

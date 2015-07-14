using UnityEngine;
using System.Collections;

//object that simple moves in one direction
public class SimpleMovingPlatform : InteractivePhysicsObject {

	//goes back and forth
	public bool pingPong;
	//speed of movement
	public float speed;
	//direction of movement
	public Direction direction;
	//distance the object should move
	public float distance;
	
	private Vector3 startPosition;
	private Vector3 endPosition;

	public enum Direction {
		RIGHT,
		LEFT,
		BACK,
		FORWARD,
		UP,
		DOWN
	}
	
	// Use this for initialization
	public override void StartMe ()
	{
		startPosition = transform.position;
		endPosition = transform.position + GetDistanceVector();
	}

	//calculates the vector in which the object is supposed to move
	private Vector3 GetDistanceVector() 
	{
		Vector3 distanceVector;
		if (distance > 0) {
			switch (direction) {
			case Direction.RIGHT:
				distanceVector = transform.right * distance;
				break;
			case Direction.LEFT:
				distanceVector = -transform.right * distance;
				break;
			case Direction.BACK:
				distanceVector = -transform.forward * distance;
				break;
			case Direction.FORWARD:
				distanceVector = transform.forward * distance;
				break;
			case Direction.UP:
				distanceVector = transform.up * distance;
				break;
			case Direction.DOWN:
				distanceVector = -transform.up * distance;
				break;
			default:
				distanceVector = -transform.right * distance;
				break;
			}
		} else {
			switch (direction) {
			case Direction.RIGHT:
				distanceVector = transform.right * transform.localScale.x;
				break;
			case Direction.LEFT:
				distanceVector = -transform.right * transform.localScale.x;
				break;
			case Direction.BACK:
				distanceVector = -transform.forward * transform.localScale.z;
				break;
			case Direction.FORWARD:
				distanceVector = transform.forward * transform.localScale.z;
				break;
			case Direction.UP:
				distanceVector = transform.up * transform.localScale.y;
				break;
			case Direction.DOWN:
				distanceVector = -transform.up * transform.localScale.y;
				break;
			default:
				distanceVector = -transform.right * transform.localScale.x;
				break;
			}
		}

		return distanceVector;
	}

	//starts the movement
	public override void DoActivation()
	{
		if(pingPong && transform.position == endPosition) {
			isActivated = false;
			isDeactivated = true;
		}
		rb.MovePosition(Vector3.MoveTowards(transform.position, endPosition, Time.deltaTime * speed));
	}

	//reverts the movement
	public override void DoDeactivation()
	{
		if(pingPong && transform.position == startPosition) {
			isActivated = true;
			isDeactivated = false;
		}
		rb.MovePosition(Vector3.MoveTowards(transform.position, startPosition, Time.deltaTime * speed));
	}
}

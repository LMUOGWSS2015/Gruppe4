using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ghost : MyMonoBehaviour {

	public List<Transform> wayPoints;
	public float speed;
	public float turningSpeed;
	public Collider coneCollider;
	public GameObject blackHolePrefab;
	public bool move;

	private bool moving;
	private bool turning;
	private bool forward;
	private int currentPoint;
	private bool isAttacking;

	// Use this for initialization
	void Start () 
	{
		if(wayPoints.Count > 0) {
			forward = true;
			GameObject initialPoint = new GameObject();
			initialPoint.transform.position = transform.position;
			initialPoint.name = "Waypoint";
			initialPoint.transform.SetParent(transform.parent);
			wayPoints.Insert(0, initialPoint.transform);
			currentPoint = 1;

			StartCoroutine(Turning ());
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void StopMoving ()
	{
		moving = false;
	}

	public IEnumerator AttackPlayer ()
	{
		isAttacking = true;
		if(isAttacking) {
			Player.Instance.Freeze (true);
			StopMoving ();
			yield return new WaitForSeconds (1f);
			Vector3 playerPos = Player.Instance.transform.position;
			GameObject blackHole = GameObject.Instantiate(blackHolePrefab);
			blackHole.transform.position = playerPos;
			yield return new WaitForSeconds (2.0f);
			Player.Instance.Kill ();
			isAttacking = false;
			Destroy(blackHole);
		}
	}

	private IEnumerator Move ()
	{
		moving = true;
		while(moving) {
			Vector3 newPos = Vector3.MoveTowards(transform.position, wayPoints[currentPoint].position, Time.deltaTime * speed);

			transform.position = newPos;

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
				turning = false;
				StartCoroutine(Move ());
			}

			yield return null;
		}
	}
}

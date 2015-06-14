using UnityEngine;
using System.Collections;

public class EnergyBall : MonoBehaviour {

	// Possible states of the energy ball's behavior
	enum EnergyBallState {WAIT, ATTACK, EXPLODE, RESET};

	// Constant for the speed of the ball (speed increases over time)
	public float speedConstant = 0.05f;

	// Distance at which the player will be detected
	public float detectionDistance = 15.0f;

	// Distance at which the ball can hurt the player
	public float explosionDistance = 2.0f;

	// Durance of the explosion
	public float explosionDurance = 2.0f;

	// Timer for the explosion
	private float explosionTimer;

	// Timer for the movement
	private float movementTimer;

	// State of the energy ball
	private EnergyBallState state;

	// Reference to the own rigidbody
	private Rigidbody rb;

	// Reference to the player Transform
	private Transform playerTransform;

	// Position at which the energy ball aims
	private Vector3 targetPosition;

	// Distance of the player
	private float playerDistance;

	// Use this for initialization
	void Start () {
		state = EnergyBallState.WAIT;
		rb = GetComponent<Rigidbody> ();
		playerTransform = GameObject.Find ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		playerDistance = (this.transform.position - playerTransform.position).magnitude;
		switch (state) {
			case EnergyBallState.WAIT:
				// Is the player so close that the ball immediately starts exploding?
				if (playerDistance <= explosionDistance) {
					SetupExplosion();
				} else if (playerDistance <= detectionDistance) { // Has the player been detected?
					SetupAttack();
				}
				break;
			case EnergyBallState.ATTACK:
				// Is the ball close enough to the target position or to the player himself?
				if ((this.transform.position - targetPosition).magnitude <= explosionDistance
				     || playerDistance <= explosionDistance)
				{
					SetupExplosion();
				} else {
					movementTimer += Time.deltaTime;
					// multiply the speed constant with the movementTimer squared to make the ball go fast with time
					rb.MovePosition(Vector3.MoveTowards(this.transform.position,targetPosition,movementTimer * movementTimer * speedConstant));
				}
				break;
			case EnergyBallState.EXPLODE:
				// Has the explosion already finished?
				if (explosionTimer >= explosionDurance) {
					Debug.Log("BOOM");
					// Hurt the player
					if (playerDistance <= explosionDistance) {
						Debug.Log("That hurt!");
						PlayerUtils.Instance.Kill();
					}
					// Reset the state of the ball
					state = EnergyBallState.RESET;
				} else {
					explosionTimer += Time.deltaTime;
					transform.localScale += Vector3.one * Time.deltaTime * 0.2f;
				}
				break;
			case EnergyBallState.RESET:
				// enable the collider again
				GetComponent<Collider>().enabled =true;
				break;
		}
	}

	// Makes the preparations for the state of attacking
	private void SetupAttack() {
		// Memorize the player's current position
		targetPosition = playerTransform.position;
		targetPosition.y = transform.position.y; // Keep the altitude while moving towards the player
		// Reset the movement timer
		movementTimer = 0;
		state = EnergyBallState.ATTACK;
	}

	// Makes the preparations for the state of exploding
	private void SetupExplosion() {
		// set the timer
		explosionTimer = 0;
		// disable the collider while exploding
		GetComponent<Collider>().enabled = false;
		state = EnergyBallState.EXPLODE;
	}

}

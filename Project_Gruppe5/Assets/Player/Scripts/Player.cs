using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Player : Singleton<Player> {

	[SerializeField] float groundCheckDistance = 0.3f;
	[Range(1f, 4f)][SerializeField] float gravityMultiplier = 2f;
	[SerializeField] float jumpPower = 8.5f;	// 2m Sprunghöhe, 3,5m Doppelsprunghöhe; 7m Sprungweite, 14m Doppelsprungweite
	[SerializeField] float turnSpeed = 1000f;
	[SerializeField] float forwardSpeed = 8f;	// 7m Sprungweite, 14m Doppelsprungweite

	Rigidbody rigidbody;
	Animator anim;

	bool jumped;
	bool walk;
	bool doubleJump;
	bool isGrounded;
	Vector3 groundNormal;
	float origGroundCheckDistance;
	float turnAmount;
	float forwardAmount;

	public Transform startPoint;
	public Transform respawnPoint {
		set;
		get;
	}

	private void Start() {
		rigidbody = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();

		rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		origGroundCheckDistance = groundCheckDistance;

		respawnPoint = startPoint;
		Respawn ();
	}

	public void Move(Vector3 move, bool jump) {
		if (jumped) {
			rigidbody.velocity = new Vector3(rigidbody.velocity.x * 0.5f, rigidbody.velocity.y, rigidbody.velocity.z * 0.5f);
			jumped = false;
		}

		if (move.magnitude > 1f)
			move.Normalize();
		move = transform.InverseTransformDirection(move);

		CheckGroundStatus ();
		move = Vector3.ProjectOnPlane(move, groundNormal);

		turnAmount = Mathf.Atan2 (move.x, move.z);
		if (move != Vector3.zero) {
			TurnRotation ();
		}

		if (isGrounded)
		{
			HandleGroundedMovement(jump);
		}
		else
		{
			HandleAirborneMovement(jump);
		}

		forwardAmount = move.z;
		Forward ();

		Animating ();
	}
	
	void TurnRotation() {
		transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
	}

	void Forward() {
		transform.Translate (0, 0, forwardAmount * forwardSpeed * Time.deltaTime);
	}

	void HandleAirborneMovement(bool jump)
	{
		if (jump && doubleJump) {
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpPower, rigidbody.velocity.z);
			doubleJump = false;
		}
		else {
			// apply extra gravity from multiplier:
			Vector3 extraGravityForce = (Physics.gravity * gravityMultiplier) - Physics.gravity;
			rigidbody.AddForce(extraGravityForce);
			
			groundCheckDistance = rigidbody.velocity.y < 0 ? origGroundCheckDistance : 0.01f;
		}
	}
	
	
	void HandleGroundedMovement(bool jump)
	{
		// check whether conditions are right to allow a jump:
		if (jump && isGrounded)
		{
			// jump!
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y + jumpPower, rigidbody.velocity.z);
			isGrounded = false;
			groundCheckDistance = 0.1f;
			doubleJump = true;
			jumped = true;
		}
	}
	
	void CheckGroundStatus()
	{
		RaycastHit hitInfo;

		if (Physics.Raycast (transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance))
		{
			groundNormal = hitInfo.normal;
			isGrounded = true;
			doubleJump = false;
		}
		else
		{
			isGrounded = false;
			groundNormal = Vector3.up;
		}

		if (Physics.Raycast (transform.position + (Vector3.up * 0.1f), Vector3.down, 1.3f)) {
			walk = true;
		}
		else {
			walk = false;
		}
	}

	void Animating() {
		bool walking = walk && forwardAmount != 0.0f;
		anim.SetBool ("IsWalking", walking);
	}

	public void Kill() {
		Respawn ();
	}

	public void Respawn() {
		transform.position = respawnPoint.position;
		transform.rotation = respawnPoint.rotation;
	}
}

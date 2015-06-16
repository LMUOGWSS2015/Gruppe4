using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Player : Singleton<Player> {

	float groundCheckDistance = 0.3f;
	[SerializeField] float minGravityMultiplier = 2f;
	[SerializeField] float maxGravityMultiplier = 3f;
	float gravityMultiplier = 2f;
	[SerializeField] float jumpPower = 8.5f;
	float turnSpeed = 1000f;
	[SerializeField] float forwardSpeed = 8f;

	Rigidbody rigidbody;
	Animator anim;

	bool jumped;
	bool walk;
	bool doubleJump;
	bool isGrounded;
	bool isJumping;
	bool isDoubleJumping;
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
		Debug.Log ("Velocity: " + rigidbody.velocity.y);
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

		forwardAmount = move.z;
		gravityMultiplier = Mathf.Lerp(minGravityMultiplier, maxGravityMultiplier, forwardAmount);

		if (isGrounded)
		{
			HandleGroundedMovement(jump);
		}
		else
		{
			HandleAirborneMovement(jump);
		}

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
			isDoubleJumping = true;
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
			isJumping = true;
		}
	}
	
	void CheckGroundStatus()
	{
		RaycastHit hitInfo;

		if (Physics.Raycast (transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance))
		{
			groundNormal = hitInfo.normal;
			isGrounded = true;
			isJumping = false;
			doubleJump = false;
			isDoubleJumping = false;
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
		bool jumping = isJumping;
		bool doublejumping = isDoubleJumping;
		bool grounded = walk;
	
		anim.SetBool ("IsJumping", jumping);
		if (!jumping)
			anim.SetBool ("IsWalking", walking);

		anim.SetBool ("IsDoubleJumping", doublejumping);
		anim.SetBool ("IsGrounded", grounded);

	}

	public void TrampolinEnter(float trampolinJumpPower) {
		rigidbody.velocity = new Vector3(0, trampolinJumpPower, 0);
		isJumping = true;
	}

	public void Kill() {
		Respawn ();
	}

	public void Respawn() {
		transform.position = respawnPoint.position;
		transform.rotation = respawnPoint.rotation;
	}
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Player : Singleton<Player> {

	[SerializeField] float groundCheckDistance = 0.3f;
	[Range(1f, 4f)][SerializeField] float gravityMultiplier = 2f;
	[SerializeField] float jumpPower = 8f;

	Rigidbody rigidbody;
	Animator anim;

	bool isGrounded;
	Vector3 groundNormal;
	float origGroundCheckDistance;
	float turnAmount;
	float forwardAmount;

	private void Start() {
		rigidbody = GetComponent<Rigidbody> ();
		//anim = GetComponent<Animator> ();

		rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		origGroundCheckDistance = groundCheckDistance;
	}

	public void Move(Vector3 move, bool jump) {
		if (move.magnitude > 1f)
			move.Normalize();
		move = transform.InverseTransformDirection(move);

		CheckGroundStatus ();

		move = Vector3.ProjectOnPlane(move, groundNormal);

		turnAmount = Mathf.Atan2(move.x, move.z);
		TurnRotation ();

		forwardAmount = move.z;
		Forward ();

		if (isGrounded)
		{
			HandleGroundedMovement(jump);
		}
		else
		{
			HandleAirborneMovement();
		}

		//Animating ();
	}
	
	void TurnRotation() {
		float turnSpeed = 500f;
		transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
	}

	void Forward() {
		float forwardSpeed = 5f;
		transform.Translate (0, 0, forwardAmount * forwardSpeed * Time.deltaTime);
	}

	void HandleAirborneMovement()
	{
		// apply extra gravity from multiplier:
		Vector3 extraGravityForce = (Physics.gravity * gravityMultiplier) - Physics.gravity;
		rigidbody.AddForce(extraGravityForce);
		
		groundCheckDistance = rigidbody.velocity.y < 0 ? origGroundCheckDistance : 0.01f;
	}
	
	
	void HandleGroundedMovement(bool jump)
	{
		//Debug.Log ("Jump: " + jump + "; Crouch: " + crouch + "; isGrounded: " + isGrounded);
		// check whether conditions are right to allow a jump:
		if (jump && isGrounded)
		{
			// jump!
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpPower, rigidbody.velocity.z);
			isGrounded = false;
			groundCheckDistance = 0.1f;
		}
	}
	
	void CheckGroundStatus()
	{
		RaycastHit hitInfo;

		if (Physics.Raycast (transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance))
		{
			groundNormal = hitInfo.normal;
			isGrounded = true;
		}
		else
		{
			isGrounded = false;
			groundNormal = Vector3.up;
		}
	}

	void Animating() {
		bool walking = forwardAmount != 0.0f;
		anim.SetBool ("IsWalking", walking);
	}

	public void Kill() {
		Debug.Log ("Player was killed!");
	}
}

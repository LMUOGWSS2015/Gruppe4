using UnityEngine;
using System.Collections;

/*
 * Der Spieler, den der Nutzer steuert.
 */
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
	[SerializeField] float jumpForwardAmountMultiplier = 4f;

	Rigidbody rigidbody;
	Animator anim;

	bool hit;
	bool isFreezed;
	bool jumped;
	bool isDoubleJumped = false;
	bool walk;
	bool doubleJump;
	bool isGrounded;
	bool isJumping;
	bool isDoubleJumping;
	Vector3 groundNormal;
	float origGroundCheckDistance;
	float turnAmount;
	float forwardAmount;
	float jumpForwardAmount;

	public AudioClip[] audioClip;
	AudioSource audio;
	bool allowplay = true;

	public Transform startPoint;
	public Transform respawnPoint {
		set;
		get;
	}

	public bool finish {
		set;
		get;
	}

	private void Start() {
		rigidbody = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		audio = GetComponent<AudioSource> ();

		rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		origGroundCheckDistance = groundCheckDistance;

		finish = false;

		respawnPoint = startPoint;
		Respawn ();
	}

	public void Move(Vector3 move, bool jump) {
		if (isFreezed) {
			move = new Vector3 (0.0f, 0.0f, 0.0f);
			jump = false;
		}

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

	/*
	 * Dreht den Spieler.
	 */
	void TurnRotation() {
		transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
	}

	/*
	 * Bewegt den Spieler.
	 */
	void Forward() {
		if (isGrounded || forwardAmount == 0.0f) {
			transform.Translate (0, 0, forwardAmount * forwardSpeed * Time.deltaTime);
		} else {
			transform.Translate (0, 0, ((forwardAmount * forwardSpeed) + jumpForwardAmount) * Time.deltaTime);
		}
	}

	/*
	 * Behandelt die Bewegung in der Luft.
	 * Führt u.a. den DoubleJump aus.
	 */
	void HandleAirborneMovement(bool jump)
	{
		if (jump && doubleJump) {
			isDoubleJumped = true;
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpPower, rigidbody.velocity.z);
			doubleJump = false;

			isDoubleJumping = true;
		}
		else {
			// apply extra gravity from multiplier:
			Vector3 extraGravityForce = (Physics.gravity * gravityMultiplier) - Physics.gravity;
			rigidbody.AddForce(extraGravityForce);
			
			groundCheckDistance = rigidbody.velocity.y <= 0 ? origGroundCheckDistance : 0.1f;
		}
	}
	
	/*
	 * Behandelt die Bewegung am Boden.
	 * Führt den Sprung aus.
	 */
	void HandleGroundedMovement(bool jump)
	{
		// check whether conditions are right to allow a jump:
		if (jump && isGrounded)
		{
			// jump!
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y + jumpPower, rigidbody.velocity.z);
			jumpForwardAmount = jumpForwardAmountMultiplier * forwardAmount;
			isGrounded = false;
			groundCheckDistance = 0.1f;
			doubleJump = true;
			jumped = true;
			isJumping = true;
		}
	}

	/*
	 * Überprüfung ob der Spieler auf dem Boden steht oder sich in der Luft befindet.
	 */
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
			jumpForwardAmount = 0.0f;
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

	/*
	 * Animiert die Bewegungen des Spielers (Stehen, Laufen, Springen, DoubleJoump, Siegerpose).
	 * Spielt die Sounds des Spielers ab (Laufen, Springen, DoubleJoump).
	 */
	void Animating() {
		bool walking = walk && forwardAmount != 0.0f;
	
		if (!finish) {
			anim.SetBool ("IsJumping", isJumping);
			if (!isJumping)
				anim.SetBool ("IsWalking", walking);

			anim.SetBool ("IsDoubleJumping", isDoubleJumping);
			anim.SetBool ("IsGrounded", isGrounded);
		}

		anim.SetBool ("HasWon", finish);

		//handle Sounds
		if (walking&&allowplay&&isGrounded) {PlaySound(0,Random.Range(0.5F, 0.9F));}
		if (jumped) {StopWalkingSound(); PlaySound(1,0.7f);}
		if (isDoubleJumped) {isDoubleJumped = false; PlaySound(2,1f);}
		if (hit) {hit = false; PlaySound(3,Random.Range(0.4F, 0.9F));}
	}

	/*
	 * Katapultiert den Spieler in die Luft wenn dieser auf ein Trampolin springt.
	 */
	public void TrampolinEnter(float trampolinJumpPower) {
		rigidbody.velocity = new Vector3(0, trampolinJumpPower, 0);
		isJumping = true;
		PlaySound(4,1f);
	}

	public void Hit(Vector3 pushback) {

		rigidbody.velocity = pushback;

		//Debug.Log ("velocityX: " + rigidbody.velocity.x + ", velocityY: " + rigidbody.velocity.y + ", velosityZ: " + rigidbody.velocity.z);

		hit = true;
	}

	/*
	 * Friert den Spieler ein, so dass dieser sich nicht mehr bewegen kann.
	 */
	public void Freeze(bool freeze) {
		isFreezed = freeze;
	}

	/*
	 * Wird aufgerufen wenn der Spieler stirbt.
	 */
	public void Kill() {
		LevelController.Instance.RestartLevel ();
		Respawn ();
	}


	/*
	 * Wird aufgerufen wenn der Spieler von einem HarmfulObject getroffen wird
	 */
	public void KillByObject ()
	{
		Death death = GetComponent<Death>();
		death.Trigger();
	}

	/*
	 * Setzt den Spieler an den aktuellen CheckPoint zurück.
	 */
	public void Respawn() {
		isFreezed = false;

		rigidbody.velocity = new Vector3 (0f, 0f, 0f);

		transform.position = respawnPoint.position;
		transform.rotation = respawnPoint.rotation;
	}

	/*
	 * Abspielen der Spieler-Sounds.
	 */
	void PlaySound(int Clip, float Volume) {
		allowplay = false;
		float clipLength = audioClip[Clip].length;
		audio.clip = audioClip [Clip];
		audio.volume = Volume;
		audio.Play();

		StartCoroutine(StartMethod(clipLength));
	}

	void StopWalkingSound(){
		audio.clip = audioClip [0];
		audio.Stop ();
	}


	private IEnumerator StartMethod(float clipLength)
	{
		//yield return new WaitForSeconds(clipLength+ Random.Range(0.30F, 0.40F)); //Walk_1

		yield return new WaitForSeconds(clipLength- Random.Range(0.01F, 0.05F)); //Walk_2

		callBack();
		
	}

	void callBack(){
	 allowplay = true;

	}

}

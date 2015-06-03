using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed = 6.0f;
	public float jumpSpeed = 8.0f;

	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
	bool isFalling = false;
	//int floorMask;
	//float camRayLength = 100.0f;

	void Awake() {
		//floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate() {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		Turn (h, v);
		Jump ();
		Move (h, v);
		Animating (h, v);
	}
	
	void Move(float h, float v) {
		movement.Set (h, 0.0f, v);

		movement = movement.normalized * moveSpeed * Time.deltaTime;

		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turn(float h, float v) {
		if (h != 0.0f || v != 0.0f) {
			Vector3 newDirection = new Vector3 (h, 0.0f, v);

			Quaternion newRotation = Quaternion.LookRotation (newDirection);

			playerRigidbody.MoveRotation (newRotation);
		}
	}

	void Jump() {
		if (Input.GetKeyDown (KeyCode.Space) && !isFalling) {
			playerRigidbody.velocity = new Vector3(0.0f, jumpSpeed, 0.0f);
		}
		isFalling = true;
	}

	void OnCollisionStay() {
		isFalling = false;
	}

	void Animating(float h, float v) {
		bool walking = h != 0.0f || v != 0.0f;
		anim.SetBool ("IsWalking", walking);
	}
}

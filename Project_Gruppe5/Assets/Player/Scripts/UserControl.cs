using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class UserControl : MonoBehaviour
{
	private Player player; 			  		// A reference to the ThirdPersonCharacter on the object
	private Transform cam;                  // A reference to the main camera in the scenes transform
	private Vector3 camForward;             // The current forward direction of the camera
	private Vector3 move;
	private bool jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

	private void Start() {
		// get the transform of the main camera
		if (Camera.main != null) {
			cam = Camera.main.transform;
		}
		else {
			Debug.LogWarning(
				"Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
			// we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
		}
		
		// get the third person character ( this should never be null due to require component )
		player = GetComponent<Player>();
	}
	
	
	private void Update() {
		if (!jump) {
			jump = Input.GetButtonDown("Jump");
		}
	}
	
	
	// Fixed update is called in sync with physics
	private void FixedUpdate() {
		// read inputs
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		
		// calculate move direction to pass to character
		if (cam != null) {
			// calculate camera relative direction to move:
			camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
			move = v*camForward + h*cam.right;
		}
		else {
			// we use world-relative directions in the case of no main camera
			move = v*Vector3.forward + h*Vector3.right;
		}
		/*
			#if !MOBILE_INPUT
			// walk speed multiplier
			if (Input.GetKey(KeyCode.LeftShift)) move *= 0.5f;
			#endif
			*/
		//Debug.Log ("Horizontal: " + h + "; " + "X: " + move.x);
		//Debug.Log ("Vertical: " + v + "; " + "Z: " + move.z);
		// pass all parameters to the character control script
		player.Move(move, jump);
		jump = false;
	}
}
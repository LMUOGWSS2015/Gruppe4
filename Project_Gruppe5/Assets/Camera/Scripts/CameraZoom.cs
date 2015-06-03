using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

	public float minZoom = 5.0f;
	public float maxZoom = 20.0f;
	public float sensitivity = 2.0f;

	public Camera camera;

	void Awake() {
		camera = GetComponent<Camera> ();
	}

	void FixedUpdate() {
		float zoom = camera.orthographicSize;
		zoom += -1 * Input.GetAxis ("Zoom") * sensitivity;
		zoom = Mathf.Clamp (zoom, minZoom, maxZoom);

		camera.orthographicSize = zoom; 
	}
}

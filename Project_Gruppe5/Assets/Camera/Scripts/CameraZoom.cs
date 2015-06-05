using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

	public float minZoom = 1.0f;
	public float maxZoom = 50.0f;
	public float sensitivity = 1.0f;

	Camera camera;
	float zoom;

	void Start() {
		camera = GetComponent<Camera> ();
		if (camera.orthographic)
			zoom = camera.orthographicSize;
		else
			zoom = camera.fieldOfView;
	}

	void FixedUpdate() {
		Zoom ();
		if (camera.orthographic)
			camera.orthographicSize = zoom; 
		else
			camera.fieldOfView = zoom;
	}

	void Zoom() {
		zoom += -1 * Input.GetAxis ("Zoom") * sensitivity;
		zoom = Mathf.Clamp (zoom, minZoom, maxZoom);
	}
}

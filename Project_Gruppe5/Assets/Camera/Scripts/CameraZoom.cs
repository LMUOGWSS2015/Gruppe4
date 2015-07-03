using UnityEngine;
using System.Collections;

/*
 * Vergrößert bzw. verkleinert den sichtbaren Bildausschnitt,
 * mit Fokus auf den Spieler.
 */
public class CameraZoom : MonoBehaviour {

	public float minZoom = 5.0f; // Minimale Zoomstärke.
	public float maxZoom = 50.0f; // Maximale Zoomstärke.
	public float zoomSpeed = 1.0f;

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
		zoom += InputManager.Zoom() * InputManager.zoomSensivity * zoomSpeed;
		zoom = Mathf.Clamp (zoom, minZoom, maxZoom);
	}
}

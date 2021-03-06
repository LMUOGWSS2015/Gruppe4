﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AlternativeSelection : MonoBehaviour {

	Camera camera;

	private GameObject[] gazeActivators;
	private List<GameObject> visibleGazeActivators;

	private int selection = -1;
	private GameObject selectedGO = null;

	// Use this for initialization
	void Start () {
		Initialize ();
	}

	// Resets the whole selection script
	public void Initialize() {
		// Get camera and all gaze activator objects
		camera = Camera.main;
		gazeActivators = GameObject.FindGameObjectsWithTag ("SelectionMarker");
		// Deselect all gaze activators
		foreach (GameObject g in gazeActivators) {
			g.GetComponentInChildren<MeshRenderer> ().enabled = false;
		}
		// Reset the selection
		selection = -1;
		selectedGO = null;
	}
	
	// Update is called once per frame
	void Update () {
		// Determine which gaze activators are visible
		int counter = 0;
		visibleGazeActivators = new List<GameObject>();
		foreach (GameObject g in gazeActivators) {
			Vector3 viewPos = camera.WorldToViewportPoint (g.transform.position);
			float distance = (transform.position - g.transform.position).magnitude;
			if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && distance <= 80) {
				visibleGazeActivators.Add(g);
			} else if (counter == selection) { // Selected one is no more visible -> deselect
				Deselect();
			}
			counter++;
		}

		// Determine if the user activated the currently selected object
		if (InputManager.GazeTrigger() && selectedGO != null) {
			ActivateObject(selectedGO);
		} else {
			// Determine if the user selects / deselects
			bool previousSelection = InputManager.Prev ();
			bool removeSelection = Input.GetKeyDown (KeyCode.C);
			bool nextSelection = InputManager.Next ();
			bool selectionChanged = false;

			// Determine how the selection changes because of the user input
			if (removeSelection && selectedGO != null) {
				Deselect ();
			} else if (previousSelection && visibleGazeActivators.Count >= 1) {
				// Deselect the current selection
				if (selectedGO != null) {
					SetMarker (selectedGO, false);
				}
				// Determine the new position of the marker
				if (selectedGO == null || selection == 0) {
					selection = visibleGazeActivators.Count - 1;
				} else {
					selection--;
				}
				selectionChanged = true;
			} else if (nextSelection && visibleGazeActivators.Count >= 1) {
				// Deselect the current selection
				if (selectedGO != null) {
					SetMarker (selectedGO, false);
				}
				// Determine the new position of the marker
				if (selectedGO == null || selection >= visibleGazeActivators.Count - 1) {
					selection = 0;
				} else {
					selection++;
				}
				selectionChanged = true;
			}

			if (selectionChanged) {
				Debug.Log ("Visible gaze activators: " + visibleGazeActivators.Count);
				Debug.Log ("new selection: " + selection);
			}

			// Select the next object
			counter = 0;
			foreach (GameObject g in gazeActivators) {
				Vector3 viewPos = camera.WorldToViewportPoint (g.transform.position);
				float distance = (transform.position - g.transform.position).magnitude;
				if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && distance <= 80) {
					if (selectionChanged) {
						Debug.Log (counter + " - " + distance);
					}
					if (selectionChanged && selection == counter) {
						Select (g, counter);
					}
				}
				counter++;
			}
		}
	}

	private void Select(GameObject g, int counter) {
		Debug.Log ("GazeActivator is selected: Visible activator #" + counter);
		if (g == null) {
			Debug.Log ("!!!!!!!!!!!!!!");
		}
		g.GetComponentInChildren<MeshRenderer> ().enabled = true;
		selection = counter;
		selectedGO = g;
	}

	private void Deselect() {
		Debug.Log ("Selection was removed from visible activator #" + selection);
		if (selectedGO != null) {
			GameObject g = gazeActivators [selection];
			SetMarker (g, false);
		}
		selection = -1;
		selectedGO = null;
	}

	private void SetMarker(GameObject g, bool value) {
		g.GetComponentInChildren<MeshRenderer> ().enabled = value;
	}

	private void ActivateObject(GameObject g) {
		GazeActivator gazeActivator = (GazeActivator) g.transform.GetComponentInParent<GazeActivator> ();
		if (gazeActivator != null) {
			gazeActivator.ExternalActivation();
		}
	}

}

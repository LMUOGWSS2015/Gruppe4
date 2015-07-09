using UnityEngine;
using System.Collections;
using iView;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GazeActivator))]
public class GazeManager : GazeMonobehaviour {

	public GameObject selectionCrystal;

	private Rigidbody rigidbody;
	private GazeActivator gazeActivator;

	private void Start() {
		rigidbody = GetComponent<Rigidbody> ();
		gazeActivator = GetComponent<GazeActivator> ();

		selectionCrystal.SetActive (false);
	}

	private void Update() {
		Vector3 gazePos = iView.SMIGazeController.Instance.GetSample ().averagedEye.gazePosInUnityScreenCoords ();
		if (gazePos.x < 0) {
			gazePos.x =0;
		}
		if (gazePos.y < 0) {
			gazePos.y =0;
		}
	}

	private void OnEnter() {
		gazeActivator.Enter ();
	}
	
	private void OnStay() {
		rigidbody.AddTorque (new Vector3 (0f, -5.0f, 0f));
		selectionCrystal.SetActive (true);
		gazeActivator.Stay (InputManager.GazeTrigger());
	}
	
	private void OnExit() {
		rigidbody.angularVelocity = Vector3.zero;
		selectionCrystal.SetActive (false);
		gazeActivator.Exit ();
	}

	public override void OnGazeEnter(RaycastHit hitInformation){
		base.OnGazeEnter(hitInformation);
		OnEnter ();
	}

	public override void OnGazeStay (RaycastHit hitInformation)
	{
		base.OnGazeStay (hitInformation);
		OnStay ();
	}

	public override void OnGazeExit(){
		base.OnGazeExit ();
		OnExit ();
	}

	public void OnMouseEnter() {
		OnEnter ();
	}

	public void OnMouseOver() {
		OnStay ();
	}

	public void OnMouseExit() {
		OnExit ();
	}	
}

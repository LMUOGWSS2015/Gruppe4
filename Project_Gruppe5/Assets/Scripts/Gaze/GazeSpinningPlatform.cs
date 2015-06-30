using UnityEngine;
using System.Collections;
using iView;

public class GazeSpinningPlatform : GazeMonobehaviour {

	public GameObject ObjToSpin;
	public Vector3 velocity;
	private Rigidbody rig;
	private GameObject player;

	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 gazePos = iView.SMIGazeController.Instance.GetSample ().averagedEye.gazePosInUnityScreenCoords ();
		if (gazePos.x < 0) {
			gazePos.x =0;
		}
		if (gazePos.y < 0) {
			gazePos.y =0;
		}
	}

	public override void OnGazeStay (RaycastHit hitInformation)
	{
		base.OnGazeStay (hitInformation);
		rig.AddTorque (new Vector3 (0f, 5.0f, 0f));
		AttachPlayerToPlatform (ObjToSpin);
		ObjToSpin.transform.Rotate(velocity);
	}
	public override void OnGazeExit(){
		base.OnGazeExit ();
		rig.angularVelocity = Vector3.zero;
		DetachPlayerFromPlatform ();
	}
	

	void OnMouseOver() {
		rig.AddTorque (new Vector3 (0f, 5.0f, 0f));
		ObjToSpin.transform.Rotate (velocity);
		AttachPlayerToPlatform (ObjToSpin);
	}

	void OnMouseExit(){
		rig.angularVelocity = Vector3.zero;
		DetachPlayerFromPlatform ();
	}
	void AttachPlayerToPlatform (GameObject platformToAttachTo) {
		player.transform.parent = platformToAttachTo.transform;
	}
	void DetachPlayerFromPlatform () {
		player.transform.parent = null;
	}
}

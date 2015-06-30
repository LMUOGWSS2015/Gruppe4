using UnityEngine;
using System.Collections;
using iView;

public class GazeStopWheel : GazeMonobehaviour {


	public GameObject ObjToStop;
	private Rigidbody rig;
	private bool gazed;
	public Vector3 velocity;


	// Use this for initialization
	void Start () {
		gazed = false;
		rig = GetComponent<Rigidbody> ();
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
		if (!gazed) {
			ObjToStop.transform.Rotate(velocity);
		}
	}

	public override void OnGazeStay (RaycastHit hitInformation)
	{
		base.OnGazeStay (hitInformation);
		rig.AddTorque (new Vector3 (0f, 5.0f, 0f));
		gazed = true;
	}
	public override void OnGazeExit(){
		base.OnGazeExit ();
		rig.angularVelocity = Vector3.zero;
		gazed = false;
	}

	void OnMouseOver() {
		rig.AddTorque (new Vector3 (0f, 5.0f, 0f));
		gazed = true;
	}
	void OnMouseExit(){
		rig.angularVelocity = Vector3.zero;
		gazed = false;
	}
}

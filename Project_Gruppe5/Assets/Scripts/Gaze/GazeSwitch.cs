using UnityEngine;
using System.Collections;
using iView;

public class GazeSwitch : GazeMonobehaviour {

	private Rigidbody rig;
	private GameObject[] moveablePlatforms;
	public float platformSpeed;
	private Vector3[] targets;
	private Vector3[] startingPoints;
	public int platformAmount;

	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody> ();
		moveablePlatforms = GameObject.FindGameObjectsWithTag("MoveableByGazePlatform");
		targets = new Vector3[platformAmount];
		startingPoints = new Vector3[platformAmount];
		for (int i = 0; i < targets.Length; i++) {
			startingPoints [i] = new Vector3 (moveablePlatforms [i].transform.position.x, moveablePlatforms [i].transform.position.y, moveablePlatforms [i].transform.position.z);
			targets [i] = new Vector3 (moveablePlatforms [i].transform.position.x, moveablePlatforms [i].transform.position.y + 25, moveablePlatforms [i].transform.position.z);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 gazePos = iView.SMIGazeController.Instance.GetSample ().averagedEye.gazePosInUnityScreenCoords ();
	}

	public override void OnGazeEnter(RaycastHit hitInformation){
		base.OnGazeEnter(hitInformation);

	}
	public override void OnGazeStay (RaycastHit hitInformation)
	{
		base.OnGazeStay (hitInformation);
		rig.AddTorque (new Vector3 (0f, 10.0f, 0f));
		float step = platformSpeed * Time.deltaTime;
		for(int i = 0; i < targets.Length; i++){
			moveablePlatforms[i].transform.position = Vector3.MoveTowards (moveablePlatforms[i].transform.position, targets[i], step);
		}
	}
	public override void OnGazeExit(){
		base.OnGazeExit ();
		rig.angularVelocity = Vector3.zero;
	}
}

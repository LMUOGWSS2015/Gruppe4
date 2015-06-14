using UnityEngine;
using System.Collections;

public class TurningWheel : InteractiveObject {

	public float speed;
	public float radius;
	public AXIS axis;
	public bool clockwise;
	public GameObject[] elements;

	private float angle;
	private Vector3 direction;
	private Rigidbody[] rigidbodies;

	public enum AXIS {
		X,
		Y,
		Z
	}

	public override void StartMe()
	{
		angle = speed * 0.1f;
		rigidbodies = new Rigidbody[elements.Length];
		SetDirection();
		PlaceObjects();
	}

	private void SetDirection()
	{
		int factor = 1;

		if(clockwise) factor = -1;

		switch(axis) {
			case AXIS.X:
			direction = Vector3.right * factor;
			break;
			case AXIS.Y:
			direction = Vector3.up * factor;
			break;
			case AXIS.Z:
			direction = Vector3.forward * factor;
			break;
			default:
			direction = Vector3.forward * factor;
			break;
		}
	}

	private void PlaceObjects()
	{
		//Vector3 centrePos = new Vector3(0, 0, 7);
		Vector3 centrePos = Vector3.zero;
		
		for (int c = 0; c < elements.Length; c++) {
			elements[c].transform.SetParent(transform);
			// "i" now represents the progress around the circle from 0-1
			// we multiply by 1.0 to ensure we get a fraction as a result.
			float i = (c * 1.0f) / elements.Length;
			
			// get the angle for this step (in radians, not degrees)
			float angle = i * Mathf.PI * 2;
			
			// the X &amp; Y position for this angle are calculated using Sin &amp; Cos
			float a = Mathf.Sin(angle) * radius;
			float b = Mathf.Cos(angle) * radius;

			Vector3 pos;

			switch(axis) {
			case AXIS.X:
				pos = centrePos - new Vector3(0, a, b);
				break;
			case AXIS.Y:
				pos = centrePos - new Vector3(a, 0, b);
				break;
			case AXIS.Z:
				pos = centrePos - new Vector3(a, b, 0);
				break;
			default:
				pos = centrePos - new Vector3(a, b, 0);
				break;
			}

			elements[c].transform.localPosition = pos;

			rigidbodies[c] = elements[c].GetComponent<Rigidbody>();
		}
	}

	public override void DoActivation ()
	{
		foreach (Rigidbody rb in rigidbodies) {
			Quaternion q = Quaternion.AngleAxis(angle, direction);
			rb.MovePosition(q * (rb.transform.position - transform.position) + transform.position);
		}
	}
}

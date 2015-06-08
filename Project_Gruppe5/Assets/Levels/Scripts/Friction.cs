using UnityEngine;
using System.Collections;

public class Friction : MonoBehaviour {
	
	void Start () {
		Transform parent = transform.parent;

		transform.localScale = new Vector3 (1.0f - (1.0f / (parent.localScale.x / 0.5f)), transform.localScale.y, 1.0f - (1.0f / (parent.localScale.z / 0.5f)));
	}
}

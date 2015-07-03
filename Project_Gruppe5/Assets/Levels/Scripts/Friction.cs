using UnityEngine;
using System.Collections;

/*
 * Setzt die Friction eines Objects so, dass der Spieler an den Seitenwänden nicht hängen bleibt
 * aber auf der Oberfläche, auch bei Bewegung des Objects, stehen bleibt.
 * 
 * Kann nur auf rechteckige Objects angewendet werden.
 */
public class Friction : MonoBehaviour {
	
	void Start () {
		Transform parent = transform.parent;

		transform.localScale = new Vector3 (1.0f - (1.0f / (parent.localScale.x / 0.5f)), transform.localScale.y, 1.0f - (1.0f / (parent.localScale.z / 0.5f)));
	}
}

﻿using UnityEngine;
using System.Collections;

public class ForestRotatingBoxHarmful : MonoBehaviour {

	void OnCollisionEnter(Collision other) 
	{
		if(other.transform.tag == "Player" && !Player.Instance.isDead()) {
			foreach (ContactPoint c in other.contacts) {
				if(c.thisCollider.tag == "SpikeBall") {
					Player.Instance.KillByObject();
				}
			}
		}
	}
}
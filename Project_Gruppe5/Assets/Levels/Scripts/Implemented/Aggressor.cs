using UnityEngine;
using System.Collections;

public class Aggressor : MonoBehaviour {

	public float timeToTarget = 1.0f;

	private bool isAttacking;

	public void Attack (Transform target)
	{
		Vector3 position = target.position;
		position = new Vector3 (position.x, transform.position.y, position.z);

		StartCoroutine (Attacking(position));
	}

	void OnCollisionEnter (Collision other)
	{
		if(other.transform.tag == "Player")
			isAttacking = false;
	}

	private IEnumerator Attacking (Vector3 targetPosition) 
	{
		isAttacking = true;
		float currentLerpTime = 0;
		float lerpTime = timeToTarget;

		Vector3 startPos = transform.position;

		while(isAttacking) {

			currentLerpTime += Time.deltaTime;
			if (currentLerpTime > lerpTime) {
				currentLerpTime = lerpTime;
			}

			float t = currentLerpTime / lerpTime;
			t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
			Vector3 newPos = Vector3.Lerp(startPos, targetPosition, t);
			transform.position = newPos;

			if(transform.position == targetPosition)
				isAttacking = false;

			yield return null;
		}
	}

}

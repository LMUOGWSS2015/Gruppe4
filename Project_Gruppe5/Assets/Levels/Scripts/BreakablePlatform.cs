using UnityEngine;
using System.Collections;

public class BreakablePlatform : MonoBehaviour {

	public Rigidbody[] breakableParts;
	public Transform explosionPosition;

	private bool isShaking;

	private float speed = 1.0f;
	private float amount = 1.0f;

	void Start()
	{
		//StartCoroutine(BreakingApart());
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Player")
			StartCoroutine(BreakingApart());
	}

	void Update()
	{
		if(isShaking) {
			Vector2 shake = Random.insideUnitCircle * 0.05f;
			transform.position = new Vector3(transform.position.x + shake.x, transform.position.y, transform.position.z + shake.y);
		}
	}

	private IEnumerator BreakingApart()
	{
		isShaking = true;
		yield return new WaitForSeconds(1.5f);
		GetComponent<Collider>().enabled = false;
		isShaking = false;
		BreakApart();
	}

	private void BreakApart()
	{
		foreach(Rigidbody rb in breakableParts) {
			float power = Random.Range(2500.0f, 10000.0f);
			rb.AddExplosionForce(power, explosionPosition.position, 50.0f);
			rb.useGravity = true;
		}
	}
	
}

using UnityEngine;
using System.Collections;

//Script for breaking platform
public class BreakablePlatform : MonoBehaviour {

	public Rigidbody[] breakableParts;
	public Transform explosionPosition;

	private bool isShaking;

	private float speed = 1.0f;
	private float amount = 1.0f;
	AudioSource audio;

	void Start()
	{
		//StartCoroutine(BreakingApart());
		audio = GetComponent<AudioSource> ();
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player") {
			StartCoroutine (BreakingApart ());
			PlaySound ();
		}
	}

	void Update()
	{
		if(isShaking) {
			Vector2 shake = Random.insideUnitCircle * 0.05f;
			transform.position = new Vector3(transform.position.x + shake.x, transform.position.y, transform.position.z + shake.y);
		}
	}

	//Coroutine that initiates the breaking of the platform
	private IEnumerator BreakingApart()
	{
		isShaking = true;
		yield return new WaitForSeconds(1.0f);
		GetComponent<Collider>().enabled = false;
		isShaking = false;
		BreakApart();
	}

	//Causes explosion in the middle of the platform for every part of it
	private void BreakApart()
	{
		foreach(Rigidbody rb in breakableParts) {
			float power = Random.Range(2500.0f, 10000.0f);
			rb.AddExplosionForce(power, explosionPosition.position, 50.0f);
			rb.useGravity = true;
		}
	}

	void PlaySound() {
		audio.Play();
	}
	
}

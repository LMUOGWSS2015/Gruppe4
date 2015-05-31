using UnityEngine;
using System.Collections;

public class SpawningPlatform : InteractiveObject {

	public GameObject spawnPrefab;
	public float timeToSpawn;
	private GameObject spawn;
	private bool spawning;
	
	// Update is called once per frame
	public override void UpdateMe () 
	{

		base.UpdateMe ();

		if(Input.GetKeyDown(KeyCode.P)) {
			Spawn ();
		}
	}

	private IEnumerator SpawnAnimation(GameObject spawn)
	{
		spawn.GetComponent<Renderer>().enabled = true;
		spawn.transform.position = new Vector3(transform.position.x, spawn.transform.localScale.y + transform.position.y, transform.position.z);
		spawn.transform.localScale = Vector3.zero;

		float elapsedTime = 0f;

		while(elapsedTime < timeToSpawn) {
			spawn.transform.localScale = Vector3.Lerp (Vector3.zero, spawnPrefab.transform.localScale, elapsedTime / timeToSpawn);
			elapsedTime += Time.deltaTime;

			yield return null;
		}
	}

	private void Spawn()
	{
		GameObject spawn = GameObject.Instantiate(spawnPrefab);
		StartCoroutine(SpawnAnimation(spawn));

	}

	private void Respawn()
	{

	}

	public override void DoActivation ()
	{
		Spawn ();
	}
}

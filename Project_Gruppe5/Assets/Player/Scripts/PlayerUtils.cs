using UnityEngine;
using System.Collections;

public class PlayerUtils : Singleton<PlayerUtils> {

	public Transform player;

	public Transform respawnPoint {
		set;
		get;
	}

	public void Respawn()
	{
		player.position = respawnPoint.position;
		player.rotation = respawnPoint.rotation;
	}

	public void Kill() 
	{
		Respawn();
	}

}

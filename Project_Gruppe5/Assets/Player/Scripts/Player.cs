using UnityEngine;
using System.Collections;

public class Player : Singleton<Player> {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Kill() 
	{
		Debug.Log ("Player was killed!");
	}
}

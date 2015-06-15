using UnityEngine;
using System.Collections;

public class InteractiveObject : MyMonoBehaviour {

	public bool noTrigger;
	public float delay;
	public bool singleUse;
	
	[HideInInspector]
	public bool isActivated;
	[HideInInspector]
	public bool isDeactivated;

	private bool isUsed;

	public void Start()
	{
		isActivated = false;
		isDeactivated = true;
		isUsed = false;
		StartMe ();
		if(noTrigger) {
			Activate();
		}
	}
	
	void Update() 
	{
		UpdateMe ();
	}

	public override void UpdateMe ()
	{
		if(isActivated) {
			DoActivation ();
			if(singleUse) isActivated = false;
		}
		
		if(isDeactivated) {
			DoDeactivation ();
		}
	}
	
	public virtual void Activate()
	{
		if(!isUsed) {
			if(singleUse) isUsed = true;
			isDeactivated = false;
			StartCoroutine(CoActivate());
		}
	}
	
	public IEnumerator CoActivate()
	{
		yield return new WaitForSeconds(delay);
		isActivated = true;
	}
	
	public virtual void Deactivate()
	{
		isActivated = false;
		StartCoroutine(CoDeactivate());
	}
	
	public IEnumerator CoDeactivate()
	{
		yield return new WaitForSeconds(delay);
		isDeactivated = true;
	}

	public void EndDeactivation()
	{
		isDeactivated = false;
	}
	
	public virtual void DoActivation() 
	{

	}
	
	public virtual void DoDeactivation() 
	{
		
	}
}
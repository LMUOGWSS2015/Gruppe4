using UnityEngine;
using System.Collections;

//"Interface" for objects that can be triggered and are interactive
public class InteractiveObject : MyMonoBehaviour {

	//is a trigger needed
	public bool noTrigger;
	//delay until the object is triggered after activation
	public float delay;
	//can be used more than once
	public bool singleUse;
	
	[HideInInspector]
	public bool isActivated;
	[HideInInspector]
	public bool isDeactivated;

	//is already used
	public bool isUsed;

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

	//execute object
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

	//deactivate object
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

	//needs to be implemented by the certain object
	public virtual void DoActivation() 
	{

	}

	//needs to be implemented by the certain object
	public virtual void DoDeactivation() 
	{
		
	}
}
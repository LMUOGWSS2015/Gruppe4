using UnityEngine;
using System.Collections;

public class Shadow_Follow : MonoBehaviour {

	GameObject shadow;
	RaycastHit hit;
	float length = 10000f;
	float distance;
	Vector3 targetLocation;
	Vector3 normalscale;
	float minscale = 0.4f;
	float maxscale = 1.8f;
	void Start () {
		shadow = GameObject.Find("Player_Shadow");
		normalscale = shadow.transform.localScale;
	}
	
	// Update is called once per frame
	
	void Update () {
		Vector3 newscale = normalscale;

		if (Physics.Raycast (transform.position, Vector3.down, out hit, length)) {

			targetLocation = hit.point;
			shadow.transform.position = new Vector3(targetLocation.x, targetLocation.y+0.1f, transform.position.z);
			distance = hit.distance;

		
			 
			if (distance > 1) {
			
				distance =distance/2.5f;	
			  
				newscale.x = normalscale.x / distance;
				newscale.y = normalscale.y / distance;
				if(newscale.x<minscale){newscale.x=minscale;newscale.y=minscale;}
				if(newscale.x>maxscale){newscale.x=maxscale;newscale.y=maxscale;}
		

			}


				shadow.transform.localScale = newscale;


		} 
	    
	}
}

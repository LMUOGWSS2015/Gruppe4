using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * Controller für das Hauptmenü.
 */
public class MainMenuController : Singleton<MainMenuController> {

	public float radiusX = 25.0f;
	public float radiusZ = 25.0f;
	public float rotationSpeed = 10.0f;
	public Text islandName;
	public Text islandText;
	public RectTransform titleLogo;
	public Camera backgroundCamera;
	public Camera starsCamera;
	public Transform fog;
	public Transform[] islands;
	public string[] islandNames;
	public string[] islandTexts;
	public Color[] backgroundColors;

	public AudioClip switchSound;
	public AudioClip selectSound;

	private int currentIsland;
	private Transform islesTransform;
	private float islesAngle;

	private bool nameChanging = false;
	private bool textChanging = false;
	private bool islandChanging = false;
	private bool initializing = true;

	void Start ()
	{
		RenderSettings.skybox.SetColor("_GroundColor", backgroundColors[0]);
		RenderSettings.skybox.SetFloat("_Exposure", 0.2f);

		for (int i=0; i<islandTexts.Length; i++) {
			if (i == islandTexts.Length-1) {
				islandTexts[i] = "Go To Settings";
			}
			else {
				bool[] activeLevels = LoadingController.Instance.ActiveLevels();
				if (activeLevels[i])
					islandTexts[i] = "Start Your Journey";
				else
					islandTexts[i] = "Level Deactivated";
			}
		}

		currentIsland = 0;
		islesTransform = islands[0].transform.parent;
		islesAngle = 360.0f / islands.Length;
		InitIslands ();

		StartCoroutine(StartMenuAnimation());
	}

	void Update()
	{
		if(!initializing) {
			if(!nameChanging && !textChanging && !islandChanging) {
				if(InputManager.Next() || Input.GetKeyDown(KeyCode.RightArrow)) {
					NextIsland();
				}
				if(InputManager.Prev() || Input.GetKeyDown(KeyCode.LeftArrow)) {
					PreviousIsland();
				}
				if(InputManager.Jump() || Input.GetKeyDown(KeyCode.Return)) {
					LoadLevel();
				}
				if(Input.GetKeyDown(KeyCode.Escape)) {
					Application.Quit();
				}
			}
		}
	}

	public void InitIslands ()
	{
		//Vector3 centrePos = new Vector3(0, 0, 7);
		Vector3 centrePos = Vector3.zero;

		for (int islandNum = 0; islandNum < islands.Length; islandNum++) {
			// "i" now represents the progress around the circle from 0-1
			// we multiply by 1.0 to ensure we get a fraction as a result.
			float i = (islandNum * 1.0f) / islands.Length;
			
			// get the angle for this step (in radians, not degrees)
			float angle = i * Mathf.PI * 2;
			
			// the X &amp; Y position for this angle are calculated using Sin &amp; Cos
			float x = Mathf.Sin(angle) * radiusX * -1.0f;
			float z = Mathf.Cos(angle) * radiusZ;
			float y = 100.0f;
			
			Vector3 pos = centrePos - new Vector3(x, y, z);

			islands[islandNum].localPosition = pos;
		}
	}

	private void NextIsland () 
	{
		currentIsland = (currentIsland + 1) % islands.Length;

		StartCoroutine(TurnIsles());
		StartCoroutine(ChangeIslandName());
		StartCoroutine(ChangeIslandText());

		MainController.Instance.PlaySound (switchSound);
	}

	private void PreviousIsland ()
	{
		currentIsland = (currentIsland - 1) % islands.Length;

		if(currentIsland < 0)
			currentIsland = islands.Length - 1;

		StartCoroutine(TurnIsles());
		StartCoroutine(ChangeIslandName());
		StartCoroutine(ChangeIslandText());

		MainController.Instance.PlaySound (switchSound);
	}

	public void LoadLevel ()
	{
		MainController.Instance.PlaySound (selectSound);

		if (!(currentIsland < LoadingController.Instance.ActiveLevels ().Length
				&& LoadingController.Instance.ActiveLevels () [currentIsland] == false)) {

			if (currentIsland != 4) {
				islandText.text = "Level Loading ...";
			}

			LoadingController.Instance.LoadScene (currentIsland);
		}
	}

	private IEnumerator TurnIsles ()
	{
		islandChanging = true;
		float lerpTime = 1.0f;
		Quaternion targetRotation = Quaternion.Euler(new Vector3(0, (islesAngle * currentIsland), 0));
		Color startColor = RenderSettings.skybox.GetColor("_GroundColor");
		Color endColor = backgroundColors[currentIsland];

		bool rotating = true;
		float currentLerpTime = 0.0f;
		
		while(rotating) {
			currentLerpTime += Time.deltaTime;

			if (currentLerpTime > lerpTime) {
				currentLerpTime = lerpTime;
			}

			float t = currentLerpTime / lerpTime;
			t = t * t * t * (t * (6f * t - 15f) + 10f);
			t = t * rotationSpeed;

			Quaternion newRot = Quaternion.RotateTowards(islesTransform.rotation, targetRotation, t);
			islesTransform.rotation = newRot;

			Color newColor = Color.Lerp(startColor, endColor, t);
			RenderSettings.skybox.SetColor("_GroundColor", newColor);

			if(islesTransform.rotation == targetRotation && RenderSettings.skybox.GetColor("_GroundColor") == endColor)
				rotating = false;

			IslandsLookAtCamera();

			yield return null;
		}
		islandChanging = false;
	}

	private void IslandsLookAtCamera()
	{
		foreach(Transform island in islands) {
			Vector3 t = new Vector3(island.position.x, island.position.y, 10000);
			island.LookAt(t);
		}
	}

	private IEnumerator ChangeIslandName() 
	{
		nameChanging = true;

		float lerpTime = 1.0f;
		bool fading = true;
		float currentLerpTime = 0.0f;
		Color startColor = islandName.color;
		Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
		Color firstColor;
		Color secondColor;

		firstColor = startColor;
		secondColor = endColor;

		bool fadingBack = false;
		
		while(fading) {
			currentLerpTime += Time.deltaTime;
			
			if (currentLerpTime > lerpTime) {
				currentLerpTime = lerpTime;
			}
			
			float t = currentLerpTime / lerpTime;
			t = t * t * t * (t * (6f * t - 15f) + 10f);
			t = t * rotationSpeed/2f;

			Color c = Color.Lerp(firstColor, secondColor, t);
			islandName.color = c;
			
			if(islandName.color == secondColor) {
				if(fadingBack)
					fading = false;

				fadingBack = true;

				islandName.text = islandNames[currentIsland];

				firstColor = endColor;
				secondColor = startColor;
				currentLerpTime = 0.0f;
			}
			yield return null;
		}
		nameChanging = false;
	}

	private IEnumerator ChangeIslandText() 
	{
		textChanging = true;
		
		float lerpTime = 1.0f;
		bool fading = true;
		float currentLerpTime = 0.0f;
		Color startColor = islandText.color;
		Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
		Color firstColor;
		Color secondColor;
		
		firstColor = startColor;
		secondColor = endColor;
		
		bool fadingBack = false;
		
		while(fading) {
			currentLerpTime += Time.deltaTime;
			
			if (currentLerpTime > lerpTime) {
				currentLerpTime = lerpTime;
			}
			
			float t = currentLerpTime / lerpTime;
			t = t * t * t * (t * (6f * t - 15f) + 10f);
			t = t * rotationSpeed/2f;
			
			Color c = Color.Lerp(firstColor, secondColor, t);
			islandText.color = c;
			
			if(islandText.color == secondColor) {
				if(fadingBack)
					fading = false;
				
				fadingBack = true;
				
				islandText.text = islandTexts[currentIsland];
				
				firstColor = endColor;
				secondColor = startColor;
				currentLerpTime = 0.0f;
			}
			yield return null;
		}
		textChanging = false;
	}

	private IEnumerator StartMenuAnimation ()
	{
		yield return new WaitForSeconds(1.0f);

		StartCoroutine(MoveCamerasDown());
		StartCoroutine(MoveLogo());
		yield return new WaitForSeconds(1.5f);
		StartCoroutine(ShowFog());
		yield return new WaitForSeconds(0.4f);
		StartCoroutine(ShowIslands());
		yield return new WaitForSeconds(0.5f);
	}

	private IEnumerator MoveLogo()
	{
		float lerpTime = 2.0f;
		
		Vector3 startSize = titleLogo.localScale;
		Vector3 endSize = new Vector3 (1, 1, 0);
		Vector2 startPos = titleLogo.anchoredPosition;
		Vector2 endPos = new Vector2(0, 330);
		
		bool animating = true;
		float currentLerpTime = 0.0f;
		
		while(animating) {
			currentLerpTime += Time.deltaTime;
			
			if (currentLerpTime > lerpTime) {
				currentLerpTime = lerpTime;
			}
			
			float t = currentLerpTime / lerpTime;
			t = t * t * t * (t * (6f * t - 15f) + 10f);
			
			Vector2 size = Vector3.Lerp(startSize, endSize, t);
			titleLogo.localScale = size;
			
			Vector2 pos = Vector2.Lerp(startPos, endPos, t);
			titleLogo.anchoredPosition = pos;
			
			if(titleLogo.anchoredPosition == endPos && titleLogo.localScale == endSize) {
				animating = false;
			}
			
			yield return null;
		}
	}

	private IEnumerator ShowIslands()
	{
		int num = 0;
		/*
		foreach(Transform island in islands) {
			StartCoroutine(MoveIslandUp(island, num));
			num++;
			yield return new WaitForSeconds(0.1f);
			//yield return null;
		}
		*/
		//yield return new WaitForSeconds(0.2f);
		StartCoroutine(MoveIslandUp(islands[0], num));
		num++;
		yield return new WaitForSeconds(0.1f);
		StartCoroutine(MoveIslandUp(islands[1], num));
		num++;
		yield return new WaitForSeconds(0.7f);
		StartCoroutine(MoveIslandUp(islands[4], num));
		num++;
		yield return new WaitForSeconds(0.2f);
		StartCoroutine(MoveIslandUp(islands[2], num));
		num++;
		yield return new WaitForSeconds(0.4f);
		StartCoroutine(MoveIslandUp(islands[3], num));
		num++;
		//yield return null;
	}

	private IEnumerator MoveIslandUp(Transform island, int num)
	{
		float lerpTime = 1.2f;
		lerpTime -= (num * 0.1f) * 2;
		
		Vector3 startPos = island.localPosition;
		Vector3 endPos = new Vector3(island.localPosition.x, 0, island.localPosition.z);
		
		bool animating = true;
		float currentLerpTime = 0.0f;

		while(animating) {
			currentLerpTime += Time.deltaTime;
			
			if (currentLerpTime > lerpTime) {
				currentLerpTime = lerpTime;
			}
			
			float t = currentLerpTime / lerpTime;
			//t = t * t * t * (t * (6f * t - 15f) + 10f);
			t = t;
			
			Vector3 pos = Vector3.Lerp(startPos, endPos, t);
			island.localPosition = pos;
			
			if(island.localPosition == endPos)
				animating = false;
			
			yield return null;
		}

		if(num == islands.Length - 1) {
			StartCoroutine(FadeInTitle());
		}
		yield return null;
	}

	private IEnumerator FadeInTitle()
	{
		float lerpTime = 1.3f;
		
		Color startColor = islandName.color;
		Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f);
		
		bool fadingIn = true;
		float currentLerpTime = 0.0f;

		while(fadingIn) {
			currentLerpTime += Time.deltaTime;
			
			if (currentLerpTime > lerpTime) {
				currentLerpTime = lerpTime;
			}
			
			float t = currentLerpTime / lerpTime;
			t = t * t * t * (t * (6f * t - 15f) + 10f);
			t = t * 5f;
			
			Color color = Color.Lerp(startColor, endColor, t);
			islandName.color = color;
			
			if(islandName.color == endColor)
				fadingIn = false;
			
			yield return null;
		}

		lerpTime = 0.3f;
		currentLerpTime = 0.0f;
		fadingIn = true;

		while(fadingIn) {
			currentLerpTime += Time.deltaTime;
			
			if (currentLerpTime > lerpTime) {
				currentLerpTime = lerpTime;
			}
			
			float t = currentLerpTime / lerpTime;
			t = t * t * t * (t * (6f * t - 15f) + 10f);
			t = t * 5f;
			
			Color color = Color.Lerp(startColor, endColor, t);
			islandText.color = color;
			
			if(islandText.color == endColor)
				fadingIn = false;
			
			yield return null;
		}

		initializing = false;
	}

	private IEnumerator MoveCamerasDown () {
		float lerpTime = 2.3f;

		Quaternion targetRotation = Quaternion.Euler(new Vector3(13, 0, 0));
		bool rotating = true;
		float currentLerpTime = 0.0f;

		float startExp = 0.2f;
		float endExp = 1.0f;
		
		while(rotating) {
			currentLerpTime += Time.deltaTime;
			
			if (currentLerpTime > lerpTime) {
				currentLerpTime = lerpTime;
			}
			
			float t = currentLerpTime / lerpTime;
			t = t * t * t * (t * (6f * t - 15f) + 10f);
			
			Quaternion newRot = Quaternion.RotateTowards(backgroundCamera.transform.rotation, targetRotation, t);
			backgroundCamera.transform.rotation = newRot;
			starsCamera.transform.rotation = newRot;

			float exposure = Mathf.Lerp (startExp, endExp, t);
			RenderSettings.skybox.SetFloat("_Exposure", exposure);
			
			if(backgroundCamera.transform.rotation == targetRotation && startExp == endExp)
				rotating = false;
			
			yield return null;
		}
	}

	private IEnumerator ShowFog ()
	{
		float lerpTime = 0.9f;
		
		Vector3 startPos = fog.localPosition;
		Vector3 endPos = new Vector3(0.0f, -50.0f, 0.0f);
		
		bool animating = true;
		float currentLerpTime = 0.0f;
		
		while(animating) {
			currentLerpTime += Time.deltaTime;
			
			if (currentLerpTime > lerpTime) {
				currentLerpTime = lerpTime;
			}
			
			float t = currentLerpTime / lerpTime;
			//t = t * t * t * (t * (6f * t - 15f) + 10f);
			t = t;
			
			Vector3 pos = Vector3.Lerp(startPos, endPos, t);
			fog.localPosition = pos;
			
			if(fog.localPosition == endPos)
				animating = false;
			
			yield return null;
		}
	}

}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * Controller für alle Menüs (außer Hauptmenü).
 */
public class SettingMenuController : Singleton<SettingMenuController> {

	public AbstractMenu menu;

	public float radiusX = 25.0f;
	public float radiusZ = 25.0f;
	public float rotationSpeed = 10.0f;
	public Text settingName;
	public Text settingText;
	public RectTransform title;
	public Camera backgroundCamera;
	public Camera starsCamera;
	public Transform fog;
	public Transform[] settings;
	public string[] settingNames;
	public string[] settingTexts;
	public Color[] backgroundColors;

	public AudioClip switchSound;
	public AudioClip selectSound;

	private int currentSetting;
	private Transform settingTransform;
	private float settingsAngle;

	private bool textChanging = false;
	private bool nameChanging = false;
	private bool settingChanging = false;
	private bool initializing = true;

	public int getCurrentSetting() {
		return currentSetting;
	}

	void Start() {
		RenderSettings.skybox.SetColor("_GroundColor", backgroundColors[0]);
		RenderSettings.skybox.SetFloat("_Exposure", 0.2f);
		
		currentSetting = 0;
		settingTransform = settings[0].transform.parent;
		settingsAngle = 360.0f / settings.Length;
		InitSettings ();
		
		StartCoroutine(SettingsMenuAnimation());
	}

	void Update()
	{
		if(!initializing) {
			if(!nameChanging && !textChanging && !settingChanging) {
				if(InputManager.Next() || Input.GetKeyDown(KeyCode.RightArrow)) {
					NextSetting();
				}
				if(InputManager.Prev() || Input.GetKeyDown(KeyCode.LeftArrow)) {
					PreviousSetting();
				}
				if(InputManager.Jump() || Input.GetKeyDown(KeyCode.Return)) {
					LoadSetting();
				}
			}
		}
	}

	private void NextSetting () 
	{
		currentSetting = (currentSetting + 1) % settings.Length;
		
		StartCoroutine(TurnSettings());
		StartCoroutine(ChangeSettingName());
		StartCoroutine(ChangeSettingText());

		MainController.Instance.PlaySound (switchSound);
	}
	
	private void PreviousSetting ()
	{
		currentSetting = (currentSetting - 1) % settings.Length;
		
		if(currentSetting < 0)
			currentSetting = settings.Length - 1;
		
		StartCoroutine(TurnSettings());
		StartCoroutine(ChangeSettingName());
		StartCoroutine(ChangeSettingText());

		MainController.Instance.PlaySound (switchSound);
	}

	public void LoadSetting ()
	{
		MainController.Instance.PlaySound (selectSound);

		menu.DoSetting ();
	}

	private IEnumerator TurnSettings ()
	{
		settingChanging = true;
		float lerpTime = 1.0f;
		Quaternion targetRotation = Quaternion.Euler(new Vector3(0, (settingsAngle * currentSetting), 0));
		Color startColor = RenderSettings.skybox.GetColor("_GroundColor");
		Color endColor = backgroundColors[currentSetting];
		
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
			
			Quaternion newRot = Quaternion.RotateTowards(settingTransform.rotation, targetRotation, t);
			settingTransform.rotation = newRot;
			
			Color newColor = Color.Lerp(startColor, endColor, t);
			RenderSettings.skybox.SetColor("_GroundColor", newColor);
			
			if(settingTransform.rotation == targetRotation && RenderSettings.skybox.GetColor("_GroundColor") == endColor)
				rotating = false;
			
			SettingsLookAtCamera();
			
			yield return null;
		}
		settingChanging = false;
	}

	private void SettingsLookAtCamera()
	{
		foreach(Transform setting in settings) {
			Vector3 t = new Vector3(setting.position.x, setting.position.y, 10000);
			setting.LookAt(t);
		}
	}

	private IEnumerator ChangeSettingName() 
	{
		nameChanging = true;
		
		float lerpTime = 1.0f;
		bool fading = true;
		float currentLerpTime = 0.0f;
		Color startColor = settingName.color;
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
			settingName.color = c;
			
			if(settingName.color == secondColor) {
				if(fadingBack)
					fading = false;
				
				fadingBack = true;
				
				settingName.text = settingNames[currentSetting];
				
				firstColor = endColor;
				secondColor = startColor;
				currentLerpTime = 0.0f;
			}
			yield return null;
		}
		nameChanging = false;
	}

	private IEnumerator ChangeSettingText() 
	{
		textChanging = true;
		
		float lerpTime = 1.0f;
		bool fading = true;
		float currentLerpTime = 0.0f;
		Color startColor = settingText.color;
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
			settingText.color = c;
			
			if(settingText.color == secondColor) {
				if(fadingBack)
					fading = false;
				
				fadingBack = true;
				
				settingText.text = settingTexts[currentSetting];
				
				firstColor = endColor;
				secondColor = startColor;
				currentLerpTime = 0.0f;
			}
			yield return null;
		}
		textChanging = false;
	}

	public void InitSettings ()
	{
		//Vector3 centrePos = new Vector3(0, 0, 7);
		Vector3 centrePos = Vector3.zero;
		
		for (int settingNum = 0; settingNum < settings.Length; settingNum++) {
			// "i" now represents the progress around the circle from 0-1
			// we multiply by 1.0 to ensure we get a fraction as a result.
			float i = (settingNum * 1.0f) / settings.Length;
			
			// get the angle for this step (in radians, not degrees)
			float angle = i * Mathf.PI * 2;
			
			// the X and Z position for this angle are calculated using Sin &amp; Cos
			float x = Mathf.Sin(angle) * radiusX * -1.0f;
			float z = Mathf.Cos(angle) * radiusZ;
			float y = 100.0f;
			
			Vector3 pos = centrePos - new Vector3(x, y, z);

			settings[settingNum].localPosition = pos;
		}
	}

	private IEnumerator SettingsMenuAnimation ()
	{
		yield return new WaitForSeconds(0.5f);
		
		StartCoroutine(MoveCamerasDown());
		StartCoroutine(MoveLogo());
		yield return new WaitForSeconds(1.2f);
		StartCoroutine(ShowFog());
		yield return new WaitForSeconds(0.3f);
		StartCoroutine(ShowSettings());
		yield return new WaitForSeconds(0.5f);
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

	private IEnumerator MoveLogo()
	{
		float lerpTime = 2.0f;
		
		Vector3 startSize = title.localScale;
		Vector3 endSize = new Vector3 (1, 1, 0);
		Vector2 startPos = title.anchoredPosition;
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
			title.localScale = size;
			
			Vector2 pos = Vector2.Lerp(startPos, endPos, t);
			title.anchoredPosition = pos;
			
			if(title.anchoredPosition == endPos && title.localScale == endSize) {
				animating = false;
			}
			
			yield return null;
		}
	}

	private IEnumerator ShowFog ()
	{
		float lerpTime = 0.9f;
		
		Vector3 startPos = fog.localPosition;
		Vector3 endPos = Vector3.zero;
		
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

	private IEnumerator ShowSettings()
	{
		int num = 0;

		foreach(Transform setting in settings) {
			StartCoroutine(MoveIslandUp(setting, num));
			num++;
			yield return new WaitForSeconds(0.2f);
			//yield return null;
		}
	}

	private IEnumerator MoveIslandUp(Transform setting, int num)
	{
		float lerpTime = 1.2f;
		lerpTime -= (num * 0.1f) * 2;
		
		Vector3 startPos = setting.localPosition;
		Vector3 endPos = new Vector3(setting.localPosition.x, 0, setting.localPosition.z);

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
			setting.localPosition = pos;
			
			if(setting.localPosition == endPos)
				animating = false;
			
			yield return null;
		}
		
		if(num == settings.Length - 1) {
			StartCoroutine(FadeInTitle());
		}
		yield return null;
	}

	private IEnumerator FadeInTitle()
	{
		float lerpTime = 1.3f;
		
		Color startColor = settingName.color;
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
			settingName.color = color;
			
			if(settingName.color == endColor)
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
			settingText.color = color;
			
			if(settingText.color == endColor)
				fadingIn = false;
			
			yield return null;
		}
		
		initializing = false;
	}
}

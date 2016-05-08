
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelectionController : MonoBehaviour
{

	// this highscore is subject to change later on
	public int HighScore = 10000;
	public GameObject challenge;
	public GameObject freeRun;
	private RectTransform challengeRectTransform;
	private RectTransform freeRunRectTransform;
	private GameObject levelsPanel;
	private int levelsCount;
	public string lastPanelName = "";
	private bool isTabActive = false;
	private bool isFreeRun = false;
	float calculatedHeightDifference;
	float height;
	private ScrollRect scrollRect;
	private Scrollbar levelsScrollBar;

	// For now 7 levels, these names sequence should be same the one written in scene
	string[] levelNames = new string[] {"BOARDWALK", "BIO HAZARDOUS",
		"SKYLINE", "BOARDWALK","BIO HAZARDOUS",
		"SKYLINE","TRAIN YARD"};
		
	void Awake ()
	{
		challengeRectTransform = challenge.transform as RectTransform;
		freeRunRectTransform = freeRun.transform as RectTransform;
		levelsPanel = GameObject.Find ("FreeRunPanel").transform.FindChild ("SelectionAreaPanel").transform.FindChild ("LevelsScroll").transform.FindChild ("Panel").gameObject;
		levelsCount = levelsPanel.transform.childCount;

		scrollRect = levelsPanel.transform.GetComponentInParent<ScrollRect> ();
		levelsScrollBar = levelsPanel.transform.parent.transform.FindChild ("Scrollbar").GetComponent <Scrollbar> ();

	}

/*	void Update() {
		//Disable scroll on level buttons in case when panel animation is active
		if(isTabActive == true) {
			scrollRect.enabled = false;
		} else {
			scrollRect.enabled = true;
		}
	}*/


	//Add a level icon to panel with image and name
	public void AddLevelIconToPanel ()
	{

	}

	public void ChallengeClicked ()
	{

		if(isTabActive==true){ //already the tab has been selected, then move out the content 
			LevelPanelBack();
			isTabActive = false;
			//Enable scrolling when challenge/freerun animation tab is active
			scrollRect.enabled = true;
		}

		isFreeRun = false;
		// Position will be changed by localPosition of Rect Transorm if middle anchor point
		// default size and position of challenge button
		challengeRectTransform.localPosition = new Vector2 (-40f, 72.7f);
		challengeRectTransform.sizeDelta = new Vector2 (126, 40);
		freeRunRectTransform.localPosition = new Vector2 (47.5f, 70);
		freeRunRectTransform.sizeDelta = new Vector2 (90, 35);

		//Chaneg Image of button and text color
		challenge.GetComponent <Image>().sprite = Resources.Load <Sprite>(AssetsPath._ChallengeModePath + "LevelSelect-Modeselect-selectedbutton");
		challenge.transform.FindChild ("Text").GetComponent <Text> ().color = new Color32(9,9,9,255);

		freeRun.GetComponent <Image>().sprite = Resources.Load <Sprite>(AssetsPath._FreeModePath + "FreeRun-Unselected");
		freeRun.transform.FindChild ("Text").GetComponent <Text> ().color = new Color32(255,255,255,255);
	}

	public void FreeRunClicked ()
	{
		if(isTabActive==true){ //already the tab has been selected, then move out the content 
			LevelPanelBack();
		}
			isFreeRun = true;
			// Position will be changed by localPosition of Rect Transorm if middle anchor point
			// default size and position of free run button
			freeRunRectTransform.localPosition = new Vector2 (40f, 72.7f);
			freeRunRectTransform.sizeDelta = new Vector2 (126, 40);
			challengeRectTransform.localPosition = new Vector2 (-47.5f, 70);
			challengeRectTransform.sizeDelta = new Vector2 (90, 35);

			//Change Image and color of text
			freeRun.GetComponent <Image>().sprite = Resources.Load <Sprite>(AssetsPath._FreeModePath + "LevelSelect-FreeRun-selectedbutton");
			freeRun.transform.FindChild ("Text").GetComponent <Text> ().color = new Color32(9,9,9,255);
			//
			challenge.GetComponent <Image>().sprite = Resources.Load <Sprite>(AssetsPath._ChallengeModePath + "LevelSelect-FreeRun-ButtonUnselected");
			challenge.transform.FindChild ("Text").GetComponent <Text> ().color = new Color32(255,255,255,255);
	}

	public void LevelPanelBack(){

		if (lastPanelName.Length <= 0)
			return;
		if (!isTabActive)
			return;

		GameObject panelObj = GameObject.Find (lastPanelName);
		//Enable animation
		Animator animCtrl = panelObj.GetComponent<Animator>();
		if(animCtrl.enabled==true && animCtrl.GetBool ("isHidden")==false)
			animCtrl.SetBool ("isHidden",true);
		for (int i = 1; i < levelsCount; i++) {
			levelsPanel.transform.GetChild (i).localPosition = new Vector3 (levelsPanel.transform.GetChild (i).localPosition.x, levelsPanel.transform.GetChild (i).localPosition.y - (-calculatedHeightDifference), levelsPanel.transform.GetChild (i).localPosition.z);
			isTabActive = false;
			//Ensable scrolling when challenge/freerun animation tab is active
			scrollRect.enabled = true;
			}
		}

	public void ChangeContentOfPanel (int levelNumber)
	{
		//Scoll the levels to top first and then do other stuff
		levelsScrollBar.value = 1;

		if (isFreeRun == true) {
			FreeRunPanelContent (levelNumber);
		} else
			ChallengePanelContent (levelNumber);
	}

	void ChallengePanelContent (int levelNumber)
	{
		GameObject panelObj = GameObject.Find ("ChallengeLevelPanel");
		//Change name
		GameObject levelObjText = panelObj.transform.FindChild ("LevelName")
			.transform.FindChild ("Text").gameObject;
		string levelNameText = levelNames [levelNumber - 1];
		levelObjText.GetComponent<Text> ().text = levelNameText;

		//Change level screenshot
		panelObj.transform.FindChild ("LevelScreen").GetComponent<Image> ().sprite =
			Resources.Load<Sprite> (AssetsPath._LevelSSPath + levelNameText + "_IMG");

		//Change Highscore
		panelObj.transform.FindChild ("LevelScreen").FindChild ("HighScore").transform.
		FindChild ("Score").GetComponent<Text> ().text = HighScore.ToString ();

		//Level Details
		GameObject LODObj = panelObj.transform.FindChild ("LevelDetails").gameObject;
		//Stunt1
		LODObj.transform.FindChild ("Stunt1Img").FindChild ("Score").GetComponent<Text> ().text = 5000.ToString ();
		//Stunt2
		LODObj.transform.FindChild ("Stunt2Img").FindChild ("Score").GetComponent<Text> ().text = 10000.ToString ();
		//Stunt3
		LODObj.transform.FindChild ("Stunt3Img").FindChild ("Score").GetComponent<Text> ().text = 15000.ToString ();

		//Level fixed timer
		LODObj.transform.FindChild ("LevelTimeImg").FindChild ("Timer").GetComponent<Text> ().text = "1:30";

		// EnergyBars, enable of disable according to level details from PLIST (future)
		LODObj.transform.FindChild ("Energy").FindChild ("EnergyBar1").gameObject.SetActive (true);
		LODObj.transform.FindChild ("Energy").FindChild ("EnergyBar2").gameObject.SetActive (true);
		LODObj.transform.FindChild ("Energy").FindChild ("EnergyBar3").gameObject.SetActive (true);

		//open panel animation
		ChangePanelAnimationStatus (panelObj,false); // slide in

		//Lerping ChallengePanel to UserView
		//panelObj.transform.localPosition = new Vector3(panelObj.transform.localPosition.x,Mathf.Lerp (panelObj.transform.position.y,26,1f),panelObj.transform.localPosition.z);
	}
		
	void FreeRunPanelContent (int levelNumber)
	{
		GameObject panelObj = GameObject.Find ("FreeRunLevelPanel");
		//Change name
		GameObject levelObjText = panelObj.transform.FindChild ("LevelName")
			.transform.FindChild ("Text").gameObject;
		string levelNameText = levelNames [levelNumber - 1];
		levelObjText.GetComponent<Text> ().text = levelNameText;

		//Change level screenshot
		panelObj.transform.FindChild ("LevelScreen").GetComponent<Image> ().sprite =
			Resources.Load<Sprite> (AssetsPath._LevelSSPath + levelNameText + "_IMG");

		ChangePanelAnimationStatus (panelObj,false); // slide in

	}

	void ChangePanelAnimationStatus(GameObject panelObj, bool status){
		//save temp name
		lastPanelName = panelObj.name;

		//Enable animation
		Animator animCtrl = panelObj.GetComponent<Animator>();
		if (animCtrl.enabled == true) {
			animCtrl.SetBool ("isHidden", status);

		}
	}


	public void ShuffleChild (string levelNumber) {

		if(isFreeRun) {
			height = 2.5f;
		} else {
			height = 4;
		}
		//string levelName = (string)levelNames [int.Parse(levelNumber) - 1];
		Transform selectedLevelObject = levelsPanel.transform.FindChild (levelNumber);

		GameObject[] tempObj = new GameObject[selectedLevelObject.GetSiblingIndex () + 1];
		Debug.Log (selectedLevelObject.GetSiblingIndex ());
		float SelectedLevelsPosY = selectedLevelObject.localPosition.y;
		selectedLevelObject.localPosition = new Vector3 (selectedLevelObject.localPosition.x, levelsPanel.transform.GetChild (0).transform.localPosition.y, selectedLevelObject.localPosition.z);
		tempObj [0] = selectedLevelObject.gameObject;
		//selectedLevelObject.SetAsFirstSibling ();
		for ( int i=0; i<selectedLevelObject.GetSiblingIndex (); i++) {
			if (i < selectedLevelObject.GetSiblingIndex () - 1) {
				levelsPanel.transform.GetChild (i).localPosition = new Vector3 (levelsPanel.transform.GetChild (i).localPosition.x, levelsPanel.transform.GetChild (i + 1).localPosition.y, levelsPanel.transform.GetChild (i).localPosition.z);
				tempObj [i + 1] = levelsPanel.transform.GetChild (i).gameObject;
			} else {
				levelsPanel.transform.GetChild (i).localPosition = new Vector3 (levelsPanel.transform.GetChild (i).localPosition.x, SelectedLevelsPosY, levelsPanel.transform.GetChild (i).localPosition.z);
				tempObj [i + 1] = levelsPanel.transform.GetChild (i).gameObject;
			}
		}

		for(int i=0; i<tempObj.Length; i++) {
			tempObj [i].transform.SetSiblingIndex (i);
		}

		RectTransform buttonsRT = selectedLevelObject.transform as RectTransform;
		calculatedHeightDifference = (buttonsRT.sizeDelta.y + 14) * height;

		if (!isTabActive) {
			for (int i = 1; i < levelsCount; i++) {
				levelsPanel.transform.GetChild (i).localPosition = new Vector3 (levelsPanel.transform.GetChild (i).localPosition.x, levelsPanel.transform.GetChild (i).localPosition.y - calculatedHeightDifference, levelsPanel.transform.GetChild (i).localPosition.z);
			}
		}
		isTabActive = true;
		//Disable scrolling when challenge/freerun animation tab is active
		scrollRect.enabled = false;
	}
}

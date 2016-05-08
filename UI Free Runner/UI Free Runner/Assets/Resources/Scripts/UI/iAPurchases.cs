/*
 AppMonster-FreeRun


Developer:
Asema Hassan
December 2015
 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace UI{

public class iAPurchases : MonoBehaviour {

		GameObject guassianBlur = null;
		GameObject inAppBacksPanel = null;
		RectTransform rectTransform = null;
		// Use this for initialization
		void Start () {
			inAppBacksPanel = GameObject.Find ("BuyGreenBacks");
			rectTransform = inAppBacksPanel.transform as RectTransform;
			guassianBlur = GameObject.Find ("MenuBackground").transform.FindChild ("GuassianBlurr").gameObject;
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public void OpenInAppPurchasesPanel(){
			//enable guassian blurr panel
			guassianBlur.GetComponent<Image> ().enabled = true;

			//Fixed position for the greenbacks panel into the screen
			rectTransform.localPosition = new Vector2 (0.0f, 0);
			rectTransform.sizeDelta = new Vector2 (440.0f, 410);
		}

		public void CloseInAppPurchasesPanel(){
			//disable guassian blurr panel
			guassianBlur.GetComponent<Image> ().enabled = false;

			//reset position for the greenbacks panel outside the screen
			rectTransform.localPosition = new Vector2 (440.0f, 820.0f);
			rectTransform.sizeDelta = new Vector2 (440.0f, 410.0f);
		}

	}
}

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

public class StoreUI : MonoBehaviour {

		GameObject lastActivePanel;
		Image shirtSelected, trouserSelected, shoesSelected, tattooSelected;
		Image currentSelectedButton = null;

		// Use this for initialization
		void Start () {
			shirtSelected = this.transform.FindChild ("Panel").transform.FindChild ("StoreItems").FindChild ("Content").FindChild ("SelectedItemShirts").GetComponent <Image>();
			trouserSelected = this.transform.FindChild ("Panel").transform.FindChild ("StoreItems").FindChild ("Content").FindChild ("SelectedItemTrousers").GetComponent <Image>();
			shoesSelected = this.transform.FindChild ("Panel").transform.FindChild ("StoreItems").FindChild ("Content").FindChild ("SelectedItemShoes").GetComponent <Image>();
			tattooSelected = this.transform.FindChild ("Panel").transform.FindChild ("StoreItems").FindChild ("Content").FindChild ("SelectedItemTattoo").GetComponent <Image>();
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		//Reset the previous selected button state when any other button is selected
		void ButtonDefault (Image imgRefer) {
			imgRefer.color = new Color32 (255,255,255,0);
		}

		//Make the button highlighted when selected
		void ButtonSelected (Image imgRefer) {
			imgRefer.color = new Color32 (255,255,255,10);
		}

		void ChangePanelAnimationStatus(GameObject panelObj, bool status){
			//save temp name
			lastActivePanel = panelObj;

			//Enable animation
			Animator animCtrl = panelObj.GetComponent<Animator>();
			if (animCtrl.enabled == true) {
				animCtrl.SetBool ("isHidden", status);

			}
		}

		public void ShirtPanel(){

			//if any button is selected already, then set its default state first
			if(currentSelectedButton != null) {
				ButtonDefault (currentSelectedButton);
			} else
				//store the reference to current selected button and set its selected state
				currentSelectedButton = shirtSelected;
				ButtonSelected (currentSelectedButton);
			

			if (lastActivePanel != null)
				ChangePanelAnimationStatus (lastActivePanel, true);
			
			GameObject panelObj = this.transform.FindChild ("Panel").transform.FindChild ("ShirtScrollView").gameObject;
			if (panelObj != null)
				ChangePanelAnimationStatus (panelObj, false);

		}

		public void TrouserPanel(){
			
			//if any button is selected already, then set its default state first
			if(currentSelectedButton != null) {
				ButtonDefault (currentSelectedButton);
			} 
				//store the reference to current selected button and set its selected state
				currentSelectedButton = trouserSelected;
				ButtonSelected (currentSelectedButton);
			

			if (lastActivePanel != null)
				ChangePanelAnimationStatus (lastActivePanel, true);
			
			GameObject panelObj = this.transform.FindChild("Panel").transform.FindChild ("TrouserScrollView").gameObject;
			Debug.Log ("TrouserPanel");
			if (panelObj != null)
				ChangePanelAnimationStatus (panelObj, false);
		}

		public void ShoesPanel(){

			//if any button is selected already, then set its default state first
			if(currentSelectedButton != null) {
				ButtonDefault (currentSelectedButton);
			} 
				//store the reference to current selected button and set its selected state
				currentSelectedButton = shoesSelected;
				ButtonSelected (currentSelectedButton);


			if (lastActivePanel != null)
				ChangePanelAnimationStatus (lastActivePanel, true);
			
			GameObject panelObj = this.transform.FindChild("Panel").transform.FindChild ("ShoesScrollView").gameObject;
			if(panelObj!=null)
				ChangePanelAnimationStatus (panelObj, false);
		}

		public void TattooPanel(){

			//if any button is selected already, then set its default state first
			if(currentSelectedButton != null) {
				ButtonDefault (currentSelectedButton);
			} 
				//store the reference to current selected button and set its selected state
				currentSelectedButton = tattooSelected;
				ButtonSelected (currentSelectedButton);
			

			if (lastActivePanel != null)
				ChangePanelAnimationStatus (lastActivePanel, true);
			
			GameObject panelObj = this.transform.FindChild("Panel").transform.FindChild ("TattooScrollView").gameObject;
			if(panelObj!=null)
				ChangePanelAnimationStatus (panelObj, false);
		}

	}
}
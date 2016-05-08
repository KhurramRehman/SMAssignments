/*
 AppMonster-FreeRun

Main Menu class

Developer:
Asema Hassan
December 2015
 */

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace UI{

public class MainMenu : MonoBehaviour {

		public MenuPanelStates panelStates = MenuPanelStates.None;
		// Use this for initialization
		void Start () {
			panelStates = MenuPanelStates.None;	
		}

		void Update(){
			// Check when to Quit game; 
			if (Input.GetKeyDown (KeyCode.Escape)) { 
				#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
				#else
				Application.Quit(); 
				#endif
			}
		}

		#region Button Panel Methods
		public void Challenge () {
			panelStates = MenuPanelStates.ChallengePanel;
			OpenPanelAccordingToState ();
		}

		public void FreeRun () {
			panelStates = MenuPanelStates.FreeRunPanel;
			OpenPanelAccordingToState ();
		}
		
		public void Store () {
			panelStates = MenuPanelStates.StorePanel;
			OpenPanelAccordingToState ();
		}
		public void Settings () {
			panelStates = MenuPanelStates.SettingsPanel;
			OpenPanelAccordingToState ();
		}

		public void Play () {
			SceneManager.LoadScene ("Level1");	
		}
		
		public void Back () {
			CloseCurrentPanelAccordingToState ();
		}
		
		public void Menu () {
			SceneManager.LoadScene ("Menu");
		}

		void OpenPanelAccordingToState(){

			if (panelStates == MenuPanelStates.None)
				return;

			GameObject panel = GameObject.Find (panelStates.ToString());
			Animator animCtrl = panel.GetComponent<Animator>();
			if(animCtrl.enabled==true)
				animCtrl.SetBool ("isHidden",false);

			MoveOUTMenuButtonsPanel ();
			Debug.Log("PanelState:"+ panelStates.ToString());
		}

		void CloseCurrentPanelAccordingToState(){
			GameObject panel = GameObject.Find (panelStates.ToString());
			Animator animCtrl = panel.GetComponent<Animator>();
			if(animCtrl.enabled==true)
				animCtrl.SetBool ("isHidden",true);

			// Enable the Buttons Controller for entry animation
			MoveINMenuButtonsPanel ();

			// Set panel state back to none			
			panelStates = MenuPanelStates.None;

		}
		#endregion

		#region PanelController Methods
		void MoveINMenuButtonsPanel(){
			GameObject buttonsPanel = GameObject.Find ("Buttons");
			Animator animCtrl = buttonsPanel.GetComponent<Animator>();
			animCtrl.enabled = true;

			animCtrl.SetBool ("isHidden", false);
			animCtrl.SetBool ("MoveOut", false);
		}

		void MoveOUTMenuButtonsPanel(){
			GameObject buttonsPanel = GameObject.Find ("Buttons");
			Animator animCtrl = buttonsPanel.GetComponent<Animator>();
			if (animCtrl.enabled == true) {
				animCtrl.SetBool ("isHidden", false);
				animCtrl.SetBool ("MoveOut", true);
			}
		}
		
		public void SetIsPanelHiddenTrue(){
			GameObject buttonsPanel = GameObject.Find ("Buttons");
			Animator animCtrl = buttonsPanel.GetComponent<Animator>();
			if (animCtrl.enabled == true) {
				animCtrl.SetBool ("isHidden", true);
				animCtrl.SetBool ("MoveOut", false);
				animCtrl.enabled = false;
			}
		}
		#endregion
	

	}
}

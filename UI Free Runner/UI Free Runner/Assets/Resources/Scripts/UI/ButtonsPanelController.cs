using UnityEngine;
using System.Collections;

namespace UI{
public class ButtonsPanelController : MonoBehaviour {

	public void SetButtonsPanelStatusTrue(){

			GameObject mainCanvasObj = GameObject.Find ("Canvas");
			mainCanvasObj.GetComponent<MainMenu>().SetIsPanelHiddenTrue();
		}

	}

}

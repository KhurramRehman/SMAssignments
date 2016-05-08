using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatesStoreItem : MonoBehaviour {

	Image  prevSelectedItem = null;
	GameObject purchasePopWindow = null;
	string currentItemPrice = "";
	void Start () {
		purchasePopWindow = GameObject.Find ("MenuBackground").transform.FindChild ("StorePanel").
			FindChild ("PurchaseConfirmPopup").gameObject;
	}

	public void MyState() {
		if (prevSelectedItem != null) {
			DefaultState (prevSelectedItem);
		}

		prevSelectedItem = EventSystem.current.currentSelectedGameObject.GetComponent <Image> ();
		currentItemPrice = EventSystem.current.currentSelectedGameObject.transform.FindChild ("Price").
			GetComponent<Text> ().text;
		ButtonSelectedState (prevSelectedItem);
	}

	void ButtonSelectedState(Image imgRefer) {
		imgRefer.sprite = Resources.Load <Sprite> (AssetsPath._StoreItemWindow +"Store-ItemPopUp-Button-Selected");
	}

	void DefaultState(Image imgRefer) {
		imgRefer.sprite = Resources.Load <Sprite> (AssetsPath._StoreItemWindow + "Store-ItemPopUp-Button-Default");
	}

	public void Purchase () {
		purchasePopWindow.transform.FindChild ("Price").GetComponent<Text>().text = currentItemPrice;
		Animator anim = purchasePopWindow.GetComponent <Animator> ();
		if(anim.GetBool ("Purchase") == false)
			anim.SetBool ("Purchase",true);

		/*else
			anim.SetBool ("Purchase",false);*/
	}
	public void PurchaseBack () {
		Animator anim = purchasePopWindow.GetComponent <Animator> ();
		if(anim.GetBool ("Purchase") == true)
			anim.SetBool ("Purchase",false);
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScrollableList : MonoBehaviour
{
    GameObject itemPrefab;
    public int itemCount = 10, columnCount = 1;
	bool shirtAlreadySet, trouserAlreadySet, shoesAlreadySet, tattoAlreadyset = false;
	 //substring to item name of camo items in resources and whiteitempath to change sprite for shorts/hoodie
	string item1SubString, item2SubString, whiteItemPath;

	Scrollbar shirtsScrollBar, tattoScrollbar, shoesScrollBar, trouserScrollbar;

	List<Color32> itemColorsList = new List<Color32> (){
		new Color32(25,24,25,255),
		new Color32(66,103,219,255),
		new Color32 (0,217,133,255),
		new Color32 (109,109,109,255),
		new Color32 (255,85,47,255),
		new Color32 (242,62,63,255),
		new Color32 (255,255,255,255),
		new Color32 (224,222,41,255)
	};

	void Start () {
		shirtsScrollBar = GameObject.Find ("MenuBackground").transform.FindChild ("StorePanel").FindChild ("Panel").FindChild ("ShirtScrollView").GetComponentInChildren <Scrollbar> ();
		trouserScrollbar = GameObject.Find ("MenuBackground").transform.FindChild ("StorePanel").FindChild ("Panel").FindChild ("TrouserScrollView").GetComponentInChildren <Scrollbar> ();
		shoesScrollBar = GameObject.Find ("MenuBackground").transform.FindChild ("StorePanel").FindChild ("Panel").FindChild ("ShoesScrollView").GetComponentInChildren <Scrollbar> ();
		tattoScrollbar = GameObject.Find ("MenuBackground").transform.FindChild ("StorePanel").FindChild ("Panel").FindChild ("TattooScrollView").GetComponentInChildren <Scrollbar> ();
	}
		
	public void ShirtContent(GameObject contentPanel) {
		if (shirtAlreadySet == true)
			return;

		//setting the loop count
		itemCount = 23;
		item1SubString = "Vest";
		item2SubString = "SH_Hoodie";
		whiteItemPath = AssetsPath._StoreItemsWhiteHoody;
		myStoreContent (contentPanel, "IndividualItemShirts");
		shirtAlreadySet = true;
	}
	public void TrouserContent(GameObject contentPanel) {
		if (trouserAlreadySet == true)
			return;
		
		//setting the loop count
		itemCount = 23;
		//Setting substring used by loop to set color and sprites for pajamas 
		item1SubString = "Pajama";
		//Setting substring used by loop to set color and sprites for shorts 
		item2SubString = "Parkour_Short";

		whiteItemPath = AssetsPath._StoreItemsWhiteShorts;
		myStoreContent (contentPanel, "IndividualItemTrouser");
		trouserAlreadySet = true;
	}
	public void ShoesContent(GameObject contentPanel) {
		if (shoesAlreadySet == true)
			return;

		item1SubString = "Shoes";
		MyStoreContentShoes (contentPanel, "IndividualItemShoes");
		shoesAlreadySet = true;
	}
	public void TattoContent(GameObject contentPanel) {
		if (tattoAlreadyset == true)
			return;
		itemCount = 9;
		myStoreContent (contentPanel, "IndividualItemTattoo");
		tattoAlreadyset = true;
	}

	//Individual method for shoes
	void MyStoreContentShoes(GameObject contentPanel, string name) {

		itemPrefab = GameObject.Find (name);
		RectTransform rowRectTransform = itemPrefab.GetComponent<RectTransform>();
		RectTransform containerRectTransform = contentPanel.GetComponent<RectTransform>();

		//calculate the width and height of each child item.

		//HACK TO SET THE SIZE OF CONTENT ACCORDING TO PANEL - ASEMA
		float width = /*113.5F;*/ containerRectTransform.rect.width / columnCount;

		float ratio = width / rowRectTransform.rect.width;

		float height = /*125.0F;*/  rowRectTransform.rect.height * ratio;

		int rowCount = itemCount / columnCount;
		if (itemCount % rowCount > 0)
			rowCount++;

		//adjust the height of the container so that it will just barely fit all its children
		float scrollHeight = height * rowCount;
		containerRectTransform.offsetMin = new Vector2(containerRectTransform.offsetMin.x, -scrollHeight / 2);
		containerRectTransform.offsetMax = new Vector2(containerRectTransform.offsetMax.x, scrollHeight / 2);

		int j = 0;
		for (int i = 0; i < 6; i++) // adding exactly 12 items for now 
		{
			//this is used instead of a double for loop because itemCount may not fit perfectly into the rows/columns
			if (i % columnCount == 0)
				j++;

			//create a new item, name it, and set the parent
			GameObject newItem = Instantiate(itemPrefab) as GameObject;
			newItem.name = gameObject.name + " item at (" + i + "," + j + ")";
			newItem.transform.SetParent (contentPanel.transform);
			//newItem.transform.parent = contentPanel.transform;
			newItem.transform.localScale = new Vector3 (1, 1, 1);

			//Change the price later according to the data
			newItem.transform.FindChild ("Price").GetComponent <Text>().text = i + "0";

			newItem.transform.FindChild ("ItemPreviewImage").GetComponent <Image> ().sprite = Resources.Load <Sprite> (AssetsPath._StoreItemShoePath + item1SubString + i);

			//move and size the new item
			RectTransform rectTransform = newItem.GetComponent<RectTransform>();

			float x = -containerRectTransform.rect.width / 2 + width * (i % columnCount);
			float y = containerRectTransform.rect.height / 2 - height * j;
			rectTransform.offsetMin = new Vector2(x, y);

			x = rectTransform.offsetMin.x + width;
			y = rectTransform.offsetMin.y + height;
			rectTransform.offsetMax = new Vector2(x, y);
		}
		shoesScrollBar.value = 0.9999f;
	}

	void myStoreContent(GameObject contentPanel, string name)
    {
		itemPrefab = GameObject.Find (name);
        RectTransform rowRectTransform = itemPrefab.GetComponent<RectTransform>();
		RectTransform containerRectTransform = contentPanel.GetComponent<RectTransform>();

        //calculate the width and height of each child item.

		//HACK TO SET THE SIZE OF CONTENT ACCORDING TO PANEL - ASEMA
		float width = /*113.5F;*/ containerRectTransform.rect.width / columnCount;

        float ratio = width / rowRectTransform.rect.width;

		float height = /*125.0F;*/  rowRectTransform.rect.height * ratio;

        int rowCount = itemCount / columnCount;
        if (itemCount % rowCount > 0)
            rowCount++;

        //adjust the height of the container so that it will just barely fit all its children
		float scrollHeight = height * rowCount;
        containerRectTransform.offsetMin = new Vector2(containerRectTransform.offsetMin.x, -scrollHeight / 2);
        containerRectTransform.offsetMax = new Vector2(containerRectTransform.offsetMax.x, scrollHeight / 2);

        int j = 0;
        for (int i = 0; i < itemCount-1; i++) // adding exactly 12 items for now 
        {
            //this is used instead of a double for loop because itemCount may not fit perfectly into the rows/columns
            if (i % columnCount == 0)
                j++;

            //create a new item, name it, and set the parent
            GameObject newItem = Instantiate(itemPrefab) as GameObject;
            newItem.name = gameObject.name + " item at (" + i + "," + j + ")";
			newItem.transform.SetParent (contentPanel.transform);
			//newItem.transform.parent = contentPanel.transform;
			                        newItem.transform.localScale = new Vector3 (1, 1, 1);

			//Change the price later according to the data
			newItem.transform.FindChild ("Price").GetComponent <Text>().text = i + "0";

			if(i < itemColorsList.Count) {
				print (i);
				newItem.transform.FindChild ("ItemPreviewImage").GetComponent <Image>().color = itemColorsList[i];
			} else if(i < (itemColorsList.Count + 3)) {
				newItem.transform.FindChild ("ItemPreviewImage").GetComponent <Image> ().sprite = Resources.Load <Sprite> (AssetsPath._CamoItemPath + item1SubString + i);
			} else if(i >= (itemColorsList.Count + 3) ) {
				if (i - (itemColorsList.Count + 3) < itemColorsList.Count) {
					newItem.transform.FindChild ("ItemPreviewImage").GetComponent <Image> ().sprite = Resources.Load <Sprite> (whiteItemPath);
					newItem.transform.FindChild ("ItemPreviewImage").GetComponent <Image> ().color = itemColorsList [i - (itemColorsList.Count + 3)];
				} else {
					/*itemSubString = "Pajama";*/
					int index = i - (itemColorsList.Count + 3);
						newItem.transform.FindChild ("ItemPreviewImage").GetComponent <Image> ().sprite = Resources.Load <Sprite> (AssetsPath._CamoItemPath + item2SubString + index);
				}
			}

            //move and size the new item
            RectTransform rectTransform = newItem.GetComponent<RectTransform>();

            float x = -containerRectTransform.rect.width / 2 + width * (i % columnCount);
            float y = containerRectTransform.rect.height / 2 - height * j;
            rectTransform.offsetMin = new Vector2(x, y);

            x = rectTransform.offsetMin.x + width;
            y = rectTransform.offsetMin.y + height;
            rectTransform.offsetMax = new Vector2(x, y);
        }
		if(name == "IndividualItemShirts") {
			shirtsScrollBar.value = 0.9999f;
		} else if (name == "IndividualItemTrouser") {
			trouserScrollbar.value = 0.9999f;
		} else if (name == "IndividualItemShoes") {
			shoesScrollBar.value = 0.9999f;
		} else if (name == "IndividualItemTattoo") {
			tattoScrollbar.value = 0.9999f;
		}


    }

}

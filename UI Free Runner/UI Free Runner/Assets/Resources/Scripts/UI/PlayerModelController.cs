using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerModelController : MonoBehaviour {

	//Refeerences to Mesh rendere of each individual cloth item 
	SkinnedMeshRenderer shoesMeshRef;
	SkinnedMeshRenderer shortMeshRef;
	SkinnedMeshRenderer trouserMeshRef;
	SkinnedMeshRenderer vestMeshRef;
	SkinnedMeshRenderer hoodieMeshRef;
	SkinnedMeshRenderer hoodieDownMeshRef;

	// Reference to indicidual cloth object
	GameObject shoesRef, shortRef, trouserRef, vestRef, hoodieRef,  hoodieDownRef;

	Texture itemTexture;
	Image itemImageComponenetRef;
	Sprite itemSpriteRef;
	string itemSpriteName;
	Color32 itemSpriteColor;

	Texture default_SH_White;
	Texture default_Vest_White;
	Texture default_Pajama_White;
	Texture default_Parkour_White;

	GameObject playerRef;

	// Use this for initialization
	void Start () {
		//getting reference to parent object of cloth items
		playerRef = GameObject.Find ("MenuBackground").transform.FindChild ("StorePanel").FindChild ("Panel").FindChild ("Character_Tpose").FindChild ("Man:Character").gameObject;

		//getting Mesh component of each individual item
		shoesMeshRef = playerRef.transform.FindChild ("shoes").GetComponent <SkinnedMeshRenderer>();
		shortMeshRef = playerRef.transform.FindChild ("Short").GetComponent <SkinnedMeshRenderer>();
		trouserMeshRef = playerRef.transform.FindChild ("trouser").GetComponent <SkinnedMeshRenderer>();
		vestMeshRef = playerRef.transform.FindChild ("Vest").GetComponent <SkinnedMeshRenderer>();
		hoodieMeshRef = playerRef.transform.FindChild ("Hoodie").GetComponent <SkinnedMeshRenderer>();
		hoodieDownMeshRef = playerRef.transform.FindChild ("hood_down").GetComponent <SkinnedMeshRenderer> ();


		//getting reference to each cloth object
		shoesRef = playerRef.transform.FindChild ("shoes").gameObject;
		shortRef = playerRef.transform.FindChild ("Short").gameObject;
		trouserRef = playerRef.transform.FindChild ("trouser").gameObject;
		vestRef = playerRef.transform.FindChild ("Vest").gameObject;
		hoodieRef = playerRef.transform.FindChild ("Hoodie").gameObject;
		hoodieDownRef = playerRef.transform.FindChild ("hood_down").gameObject;

		//Debug.Log ("Shoes name =>" + shoesRef.name + " short name => " + shortRef.name + " trouser name =>" + trouserRef.name + " vest name => " + vestRef.name + " hoodie name => " + hoodieRef.name);

		//loading default white items texture
		default_SH_White = Resources.Load <Texture> (AssetsPath._StoreItemsWhiteHoody);
		default_Vest_White = Resources.Load <Texture> (AssetsPath._StoreItemWhiteVest);
		default_Pajama_White = Resources.Load <Texture> (AssetsPath._StoreItemWhitePajama);
		default_Parkour_White = Resources.Load <Texture> (AssetsPath._StoreItemsWhiteShorts);

	}

	// Method call for changing vest/hoodie item
	public void ChangeItemVest ()
	{
		//getting reference to image component of the selected store item
		itemImageComponenetRef = EventSystem.current.currentSelectedGameObject.transform.FindChild ("ItemPreviewImage").GetComponent <Image> ();

		//reference to sprite of image componenet and name of sprite
		itemSpriteRef = itemImageComponenetRef.sprite;
		itemSpriteName = itemSpriteRef.name;


		Debug.Log (itemSpriteName);
		// to check whether the selected item is just a colored shirt or a texture 
		if(itemSpriteName == "SH_White" || itemSpriteName == "Vest_White")
		{
			//getting color from image component
			itemSpriteColor = itemImageComponenetRef.color;

			//calling method to apply just color of vest/hoodie 
			ChangeVest (null, itemSpriteColor);

		}	else {
			// laod texture from resources that needs to be applied to the model
			itemTexture = Resources.Load <Texture>(AssetsPath._CamoItemPath + itemSpriteName + "_Texture");

			//calling the method to apply just texture of vest/hoodie
			ChangeVest (itemTexture, itemSpriteColor);
		}

	}

	//Method call for changning pajama/short ****Same functionality as vest/hoodie (ChangeItemVest) method
	public void ChangeItemTrouser ()
	{
		itemImageComponenetRef = EventSystem.current.currentSelectedGameObject.transform.FindChild ("ItemPreviewImage").GetComponent <Image> ();
		itemSpriteRef = itemImageComponenetRef.sprite;
		itemSpriteName = itemSpriteRef.name;


		Debug.Log (itemSpriteName);
		if(itemSpriteName == "Pajama_White" || itemSpriteName == "Parkour_Short_White")
		{
			itemSpriteColor = itemImageComponenetRef.color;
			ChangeShort (null, itemSpriteColor);
		}
		// Vest_White	SH_White
		else {
			itemTexture = Resources.Load <Texture>(AssetsPath._CamoItemPath + itemSpriteName + "_Texture");
			ChangeShort (itemTexture, itemSpriteColor);
		}
	}

	//Method call for changing shoes
	public void ChangeItemShoes ()
	{
		itemImageComponenetRef = EventSystem.current.currentSelectedGameObject.transform.FindChild ("ItemPreviewImage").GetComponent <Image> ();
		itemSpriteRef = itemImageComponenetRef.sprite;
		itemSpriteName = itemSpriteRef.name;

		//Loading the texture to be applied to the model's shoes
		itemTexture = Resources.Load <Texture> (AssetsPath._StoreItemShoeTexturePath + itemSpriteName + "_Texture");

		//Method that will apply the above loaded texture 
		ChangeShoes (itemTexture);
	}

	void ChangeShoes (Texture texture)
	{
		if (texture != null)
			shoesMeshRef.material.mainTexture = texture;
			
	}

	void ChangeShort (Texture texture, Color32 color)
	{
		// check if method is called either for color changes or texture changes
		if (texture != null) {
			
			// checking if the selected store items name represents a pajama or short and activating / deactivating objects accordingly
			if(itemSpriteName.Contains ("Pajama")) {
				trouserRef.SetActive (true);
				shortRef.SetActive (false);

				//Set the color to default white in case user has selected a colored shirt before a textured shirt   
				trouserMeshRef.material.color = new Color32 (255, 255, 255, 255);
				trouserMeshRef.material.mainTexture = texture;

			} else if (itemSpriteName.Contains ("Parkour")) {
				shortRef.SetActive (true);
				trouserRef.SetActive (false);

				shortMeshRef.material.color = new Color32 (255, 255, 255, 255);
				shortMeshRef.material.mainTexture = texture;
			}
		}

		else if (color.ToString () != null) {
			// checking if the selected store items name represents a pajama or short and activating / deactivating objects accordingly
			if(itemSpriteName.Contains ("Pajama")) {
				trouserRef.SetActive (true);
				shortRef.SetActive (false);

				//if colored item is a white pajama then apply the white pajama texture first
				if (itemSpriteName == "Pajama_White")
					trouserMeshRef.material.mainTexture = default_Pajama_White;

				trouserMeshRef.material.color = new Color32 (color.r, color.g, color.b, color.a);

			} else if (itemSpriteName.Contains ("Parkour")) {
				shortRef.SetActive (true);
				trouserRef.SetActive (false);

				//if colored item is a short then apply the white pajama texture first
				if (itemSpriteName == "Parkour_Short_White")
					shortMeshRef.material.mainTexture = default_Parkour_White;

				shortMeshRef.material.color = new Color32 (color.r, color.g, color.b, color.a);
			}



			

		}
	}

	void ChangeTrouser (Texture texture, Color32 color)
	{
		if (texture != null)
			trouserMeshRef.material.mainTexture = texture;
	}

	void ChangeVest (Texture texture, Color32 color)
	{
		
		if (texture != null) {
			if (itemSpriteName.Contains ("SH")) {
				//hoodieRef.SetActive (true);
				hoodieDownRef.SetActive (true);
				vestRef.SetActive (false);

				hoodieDownMeshRef.material.color = new Color32 (255, 255, 255, 255);
				//vestMeshRef.material.color = new Color32 (255, 255, 255, 255);

				hoodieDownMeshRef.material.mainTexture = texture;
				//vestMeshRef.material.mainTexture = texture;

			} else if (itemSpriteName.Contains ("Vest")) {
				vestRef.SetActive (true);
				//hoodieRef.SetActive (false);
				hoodieDownRef.SetActive (false);

				vestMeshRef.material.color = new Color32 (255, 255, 255, 255);
				vestMeshRef.material.mainTexture = texture;

			}
		}

		else if (color.ToString () != null) {
			if (itemSpriteName.Contains ("SH")) {
				hoodieDownRef.SetActive (true);
				vestRef.SetActive (false);
				if(itemSpriteName == "SH_White")
					hoodieDownMeshRef.material.mainTexture = default_SH_White;
				
				hoodieDownMeshRef.material.color = new Color32 (color.r, color.g, color.b, color.a);
				
			}

			else if (itemSpriteName.Contains ("Vest")) {
				vestRef.SetActive (true);
				hoodieDownRef.SetActive (false);
				if(itemSpriteName == "Vest_White")
					vestMeshRef.material.mainTexture = default_Vest_White;
				
				vestMeshRef.material.color = new Color32 (color.r, color.g, color.b, color.a);

			}
				
			

		}
	}

	void ChangeHoodie (Texture texture, Color32 color)
	{
		if (texture != null)
			hoodieMeshRef.material.mainTexture = texture;
	}

}

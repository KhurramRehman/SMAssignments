using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerSpeedSlider : MonoBehaviour {

	public const float DefaultSpeedValue = 125.0f;
	private float _playerSpeed = 0.0f; 
	Slider playerSpeedSlider;
	Text speedText;

	// Use this for initialization
	void Start() 
	{
		// Try to find the desired GameObject.
		// This will only find active GameObjects in the scene.
		GameObject SliderObj = GameObject.Find("SpeedSlider");
		GameObject TextObj = GameObject.Find ("CurrentSpeed");


		if (TextObj != null) {
			speedText = TextObj.GetComponent<Text>();
		}

		if (SliderObj != null){
			// Get the Slider Component
			playerSpeedSlider = SliderObj.GetComponent<Slider>();
			// If a Slider Component was found on the GameObject.
			if (playerSpeedSlider != null){
				playerSpeedSlider.value = DefaultSpeedValue;
			}
			else{
				Debug.LogError("[" + SliderObj.name + "] - Does not contain a Slider Component!");
			}	
		}
		else{
			_playerSpeed = 150.0f;
			Debug.LogError("Could not find an active GameObject named Speed Slider!");
		}
	}

	public void SetPlayerRunSpeed(){
		_playerSpeed = playerSpeedSlider.value;
		speedText.text = _playerSpeed.ToString ();
		//Debug.Log (_playerSpeed);
	}
	
	public float GetPlayerRunSpeed(){
		return _playerSpeed;
	}
}


using UnityEngine;
using System.Collections;

	public enum Swipe {
		None,
		Up,
		Down,
		Left,
		Right,
		UpLeft,
		UpRight,
		DownLeft,
		DownRight
	};

	public enum PlayerActions {
		None=0,
		TooSoon=1,
		LazyVault=2,
		TooLate=3,
		NoKey=4,
		WallClimb=5,
		WallJump=6
	};

	public enum PlayerIdleDirections{
		None=0,
		Left= -1,
		Right= 1
	};


	public enum MenuPanelStates{
		None =0,
		ChallengePanel =1,
		FreeRunPanel = 2,
		StorePanel =3,
		SettingsPanel =4
	};


using UnityEngine;
using System.Collections;

public class LevelMenuController : MonoBehaviour {

	public void PlayGame(){
		Application.LoadLevel("GamePlay");
	}

	public void BackToMenu(){
		Application.LoadLevel("MainMenu");
	}

}

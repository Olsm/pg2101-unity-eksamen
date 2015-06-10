using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour {

	public Text highScore;

	// Set high score in menu
	void Start () {
		highScore.text = "High Score: " + PlayerPrefs.GetFloat ("High Score", 0);
	}

	// Load first game level
	public void LoadGame () {
		Application.LoadLevel("Level1");
	}
}

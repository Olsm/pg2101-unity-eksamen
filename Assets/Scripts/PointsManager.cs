using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour {

	static readonly float timer = 300;
	static float timeSurvived = 0;	// A global float for how long player has survived
	static int lives = 3;	// A global int for how many lives are left
	static float timeLeft = timer;	// A global float for how much time is left
	static bool gameOver = false;
	private string timeString;
	private Text timeSurvivedText;
	private Text timeLeftText;
	private Text livesText;

	void Awake () {
		timeSurvivedText = GameObject.Find ("Text_TimeSurvived").GetComponent<Text> ();
		timeLeftText = GameObject.Find ("Text_TimeLeft").GetComponent<Text> ();
		livesText = GameObject.Find ("Text_Lives").GetComponent<Text> ();
	}

	void Update () {
		// Update time survived
		if (timeSurvived < timer) {
			timeSurvived += Time.deltaTime;	// Add a second to time survived
			timeSurvived = Mathf.Clamp(timeSurvived, 0, timer);	// prevent from going higher than 0
			timeString = timeToString (timeSurvived);	// turn time into string
			timeSurvivedText.text = "Time survived: " + timeString;	// Update time survived text
		}

		// Update time left
		if (timeLeft > 0) {
			timeLeft -= Time.deltaTime;	// remove a secnd from time left
			timeLeft = Mathf.Clamp (timeLeft, 0, timer);	// prevent from going lower than 0
			timeString = timeToString (timeLeft);	// tun time into string
			timeLeftText.text = "Time left: " + timeString;
		}	// game is over if no time left 
		else if (!gameOver && timeLeft == 0) {
			gameOver = true;
		}
	}

	// Return a string with the time in minutes:secons
	string timeToString (float time) {
		// Calculate minutes and convert to string
		string minutes = Mathf.Floor(time / 60).ToString("00");
		// Calculate seconds and convert to string
		string seconds = (time % 60).ToString("00");
		return minutes + ":" + seconds;
	}

	// Lose a life or game over when player is hit
	void playerHit () {
		if (lives > 0)
			lives -= 1;
		if (lives == 0)
			gameOver = true;
	}
}

using UnityEngine;
using System.Collections;

public class BasketYellow : MonoBehaviour {

	private static float timer = 10;
	private float timeLeft = timer;
	private string timeString;
	private bool move = false;
	private float timePoints = 10;
	private PointsManager gameInfo;
	
	void Awake () {
		gameInfo = GameObject.Find ("Text_GameInfo").GetComponent<PointsManager> ();
	}
	
	// Update is called once per frame
	void Update () {

		// Update time left
		if (timeLeft > 0) {
			timeLeft -= Time.deltaTime;	// remove a secnd from time left
			timeLeft = Mathf.Clamp (timeLeft, 0, timer);	// prevent from going lower than 0
			timeString = PointsManager.timeToString (timeLeft);	// tun time into string
			//timeLeftText.text = "Time left: " + timeString;
		}	// game is over if no time left 
		else if (!move && timeLeft == 0) {
			move = true;
		}

		if (move) {
			transform.position = transform.position - Vector3.up * Time.deltaTime;
			if (transform.position.y < -5.4) {
				Destroy(gameObject);
			}
		}
	}

	void OnCollisionEnter2D (Collision2D collider) {
		if (collider.gameObject.name == "Gun_Laser(Clone)") {
			gameInfo.AddTime(timePoints);
			Destroy (gameObject);
		} 
	}

	void OnTriggerEnter2D (Collider2D other) {
		move = true;
		other.gameObject.transform.parent = transform;
	}
}

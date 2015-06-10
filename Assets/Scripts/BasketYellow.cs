using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BasketYellow : MonoBehaviour {

	private static float timer = 10;
	private float timeLeft = timer;
	private string timeString;
	private bool move = false;
	private float timePoints = 1;
	private GameManager gameInfo;
	public GameObject timerText;
	public GUIText timeLeftText;
	private Camera camera;
	
	void Awake () {
		gameInfo = GameObject.Find ("Text_GameInfo").GetComponent<GameManager> ();
		camera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Update time left
		if (timeLeft > 0) {
			timeLeft -= Time.deltaTime;	// remove a secnd from time left
			timeLeft = Mathf.Clamp (timeLeft, 0, timer);	// prevent from going lower than 0
			timeString = GameManager.timeToString (timeLeft);	// tun time into string
			timeLeftText.text = timeString;
			timerText.transform.position = camera.WorldToViewportPoint(transform.position + new Vector3(0, 0.7f, 0));
		}	// game is over if no time left 
		else if (!move && timeLeft == 0) {
			move = true;
			timeLeftText.enabled = false;
			GetComponent<Rigidbody2D>().isKinematic = false;
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
			timePoints = Random.Range(1, 5);
			gameInfo.AddTime(timePoints);
			Destroy (gameObject);
		} 
	}
}

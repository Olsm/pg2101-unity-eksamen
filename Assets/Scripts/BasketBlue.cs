using UnityEngine;
using System.Collections;

public class BasketBlue : MonoBehaviour {

	private bool move = false;
	private float timePoints = 10;
	private GameManager gameInfo;
	private AudioSource audio;
	
	void Awake () {
		gameInfo = GameObject.Find ("Text_GameInfo").GetComponent<GameManager> ();
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (move) {
			transform.position = transform.position - Vector3.up * Time.deltaTime;
			if (transform.position.y < -5.4) {
				gameInfo.LifeLost();
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
		audio.Play ();
	}
}

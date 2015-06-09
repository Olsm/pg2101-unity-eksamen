using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

	Rigidbody2D rb;
	private PointsManager gameInfo;

	void Awake() {
		rb = GetComponent<Rigidbody2D> ();
		gameInfo = GameObject.Find ("Text_GameInfo").GetComponent<PointsManager> ();
	}

	void OnCollisionEnter2D (Collision2D collider) {
		// Prevent player from being moved on collision by enabling isKinematic and setting localposition
		rb.isKinematic = true;
		transform.localPosition = new Vector2 (0, 0);

		// Lose a life if its not the ball that has collided
		if (collider.gameObject.name != "Ball_Red") {
			gameInfo.LifeLost();
		}

		// destroy the collided object
		Destroy(collider.gameObject);
	}

	// Turn off kinematic after collision to enable collision detection
	void OnCollisionExit2D (Collision2D collider) {
		rb.isKinematic = false;
	}
}

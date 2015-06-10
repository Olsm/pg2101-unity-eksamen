using UnityEngine;
using System.Collections;

public class LaserShoot : MonoBehaviour {

	public static bool laserShot = false;
	private string direction = "right";
	private float speed = 3f;
	private Vector2 targetPosition; // = new Vector2(20f, 0f);
	
	// Update is called once per frame
	void Update () {
		if (laserShot) {
			// Update position with Lerp for a smoother movement
			transform.position = Vector2.Lerp (transform.position, targetPosition, Time.deltaTime * speed);

			// Destroy the gameobject when its done moving
			if ((direction == "right" && transform.position.x > targetPosition.x -1)
			    || (direction == "left" && transform.position.x < targetPosition.x + 1)){
				Destroy (gameObject);
				laserShot = false;
			}
		}
	}

	// Shoot the laser to the position
	public void Shoot(Vector2 target, string direction) {
		this.direction = direction;
		targetPosition = target;
		laserShot = true;
	}
}

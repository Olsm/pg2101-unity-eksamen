using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Vector3 axis = new Vector3 (0f, 0f, 1f);
	private Vector3 pos;	// Player position
	private float orbitSpeed = 100f;
	private float playerSpeed = 1f;
	private float shootPower = 10f;	// How long a shot should travel
	private string direction = "right";	// in what direction should gun shoot
	private Vector2 targetPosition;

	public GameObject ball;
	public GameObject laser;
	public Transform gun;

	void Awake () {
		ball = newBall ();
	}

	void Update () {
		// Update player position
		pos = transform.position;

		// Move player left/right if laser has been shot
		if (Laser.laserShot)
			transform.position = Vector2.Lerp (pos, targetPosition, Time.deltaTime * playerSpeed);

		// Orbit ball around player to the left
		if (Input.GetKey ("left")) {
			if (direction == "right")
				direction = "left";
			if (ball.transform.position.y > -4.2 || ball.transform.localPosition.x > 0)
				ball.transform.RotateAround (pos, axis, orbitSpeed * Time.deltaTime);
		}
		// Orbit ball around player to the right
		else if (Input.GetKey ("right")) {
			if (direction == "left")
				direction = "right";
			if (ball.transform.position.y > -4.2 || ball.transform.localPosition.x < 0)
				ball.transform.RotateAround (pos, -axis, orbitSpeed * Time.deltaTime);
		} 
		// Shoot laser from gun and move player opposite direction
		if (Input.GetKey ("left ctrl") || Input.GetKey ("right ctrl")) {
			if (!Laser.laserShot) {
				movePlayer();
				GameObject laserInstance = Instantiate(laser, gun.position, gun.rotation) as GameObject;
				if (direction == "right")
					laserInstance.GetComponent <Laser>().Shoot(new Vector2(gun.transform.position.x + shootPower, gun.transform.position.y), direction);
				else
					laserInstance.GetComponent <Laser>().Shoot(new Vector2(gun.transform.position.x - shootPower, gun.transform.position.y), direction);
			} 
		}
	}

	void movePlayer () {
		if (direction == "right")
			targetPosition = new Vector2 (pos.x - shootPower/2, pos.y);
		else
			targetPosition = new Vector2 (pos.x + shootPower/2, pos.y);
		// Stop player from going further than 7f left and right
		targetPosition.x = Mathf.Clamp (targetPosition.x, -7, 7);
	}

	GameObject newBall () {
		// Instantiate a new ball, set player as parent and initial local position
		ball = Instantiate (ball, new Vector3 (0f, 0f, 0f), new Quaternion (0f, 0f, 0f, 0f)) as GameObject;
		ball.transform.parent = transform;
		ball.transform.localPosition = new Vector2 (0f, 1.4f);
		return ball;
	}
}

using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	private bool run = false;
	private float speed = 2f;
	private Vector2 targetPosition; // = new Vector2(20f, 0f);
	
	// Update is called once per frame
	void Update () {
		if (run) {
			// Oppdater posisjonen med lerp for mykere bevegelse
			transform.position = Vector2.Lerp (transform.position, targetPosition, Time.deltaTime * speed);

			// slett spillobjektet når posisjon x er større enn 10
			if (transform.position.x > targetPosition.x - 0.1)
				Destroy (gameObject);
		}
	}

	// Skyt laser til posisjonen
	void Shoot(Vector2 target) {
		targetPosition = target;
		run = true;
	}
}

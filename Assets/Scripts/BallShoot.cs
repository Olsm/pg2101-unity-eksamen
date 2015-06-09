﻿using UnityEngine;
using System.Collections;

public class BallShoot : MonoBehaviour {

	private float minimumPower = 200f;
	private float power = 0f;
	private float powerRate = 1f;
	public float powerMultiplier = 5f;
	private int shift = 1;	// used to shift power counter between positive and negative
	private PointsManager gameInfo;
	private bool shot = false;
	private float shootWait = 2;

	void Awake () {
		GetComponent<Rigidbody2D> ().isKinematic = true;
		gameInfo = GameObject.Find ("Text_GameInfo").GetComponent <PointsManager> ();
	}

	void Update () {
		if (!shot) {
			// Increase/decrease power rate when space is clicked
			if (Input.GetKey ("space")) { 
				power += (powerRate * shift);
				gameInfo.SetPowerText ("" + power);
				if (power >= 100 || power <= 0)
					shift = shift * -1;
			}
			// Shoot ball
			if (Input.GetKeyUp ("space")) {
				power = power * powerMultiplier + minimumPower;
				GetComponent<Rigidbody2D> ().isKinematic = false;
				GetComponent<Rigidbody2D> ().AddForce (new Vector2 (transform.localPosition.x * power, transform.localPosition.y + power));
				transform.parent = null;
				shot = true;
				GameObject.Find ("Player").GetComponent<PlayerController> ().Invoke ("newBall", shootWait);
			}
		} 
		// Destroy the ball if its out of space
		else if (transform.position.y < -5.3f)
			Destroy (gameObject);
	}
}
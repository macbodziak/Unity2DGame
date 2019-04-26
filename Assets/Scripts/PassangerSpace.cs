using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PassangerSpace : MonoBehaviour {

	[SerializeField] Passanger passanger;

	private void Awake () {
		Assert.IsNotNull (passanger);
	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player") {
			if (passanger != null) { passanger.moving = true; }
		}
	}

	private void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.tag == "Player") {
			if (passanger != null) { passanger.moving = false; }
		}
	}
}
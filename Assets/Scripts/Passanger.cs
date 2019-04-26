using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Passanger : MonoBehaviour {

	public bool moving = false;
	[SerializeField] float speed = 1f;
	Rigidbody2D rb;

	private void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		Assert.IsNotNull (rb);
	}

	private void Start() {
		GameController.Instance.RegisterPassanger(this);
	}
	private void FixedUpdate () {
		if (GameController.Instance.player == null) {
			return;
		}

		if (moving) {
			if (transform.position.x < GameController.Instance.player.transform.position.x) {
				//move right
				rb.MovePosition (rb.position + new Vector2 (speed * Time.fixedDeltaTime, 0f));
			} else {
				//move left
				rb.MovePosition (rb.position - new Vector2 (speed * Time.fixedDeltaTime, 0f));
			}
		}
	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log("payer collided with passanger");
			Player player = other.gameObject.GetComponent<Player>();
			if (player.Occupied) {
				moving = false;
			}
			else {
				Debug.Log("passanger pick up");
				player.Occupied = true;
				GameController.Instance.UnregisterPassanger(this);
				Destroy(gameObject);
			}
		}
	}
}
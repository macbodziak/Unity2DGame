using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassangerDroppedOff : MonoBehaviour {

	Vector2 movement = new Vector3(0f, 0f);
	Transform target;
	[SerializeField] float speed = 1f;
	Rigidbody2D rb;
	
	private void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	public void MoveTo(Transform _target) {
		target = _target;
		if(transform.position.x > target.position.x) {
			movement = new Vector3(-1f, 0f, 0f);
		}
		else {
			movement = new Vector3(1f, 0f, 0f);
		}
	}

	private void FixedUpdate() {
		Vector2 translation = movement * Time.deltaTime * speed; 
		rb.MovePosition(rb.position + translation);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Mothership")) {
			Debug.Log("score point");
			GameController.Instance.Score++;
			Destroy(gameObject);
		}
	}
}

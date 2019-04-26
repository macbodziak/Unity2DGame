using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	Vector2 startPoint;
	Vector2 endPoint;
	Vector2 vector;
	Player player;
	[SerializeField] float sensitivity = 0.01f;
	// Use this for initialization
	void Start () {
		player = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			startPoint = Input.mousePosition;
		}
		if(Input.GetMouseButton(0)) {
			endPoint = Input.mousePosition;
			vector = endPoint - startPoint;
			player.MovementVector = vector * sensitivity;
		}

		if(Input.GetMouseButtonUp(0)) {
			player.MovementVector = Vector2.zero;
		}
	}
}

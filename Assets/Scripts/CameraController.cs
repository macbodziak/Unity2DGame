using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float sizeRatio = 16f;

	private void Start() {
		float aspectRatio = (float)Screen.height / (float)Screen.width;
		if(aspectRatio > 1f) {
			Camera.main.orthographicSize = sizeRatio * aspectRatio;
		}
		else {
		Camera.main.orthographicSize = sizeRatio;
		}
	}

	void Update () {
		if(GameController.instance.player != null) {
		transform.position = new Vector3(GameController.instance.player.transform.position.x, GameController.instance.player.transform.position.y, -50f);
		}
	}
}

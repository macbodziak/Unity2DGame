using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MothershipLandingZone : MonoBehaviour {

	[SerializeField] Transform gatePosition;
	[SerializeField] PassangerDroppedOff passangerPrefab;

	private void Awake() {
		Assert.IsNotNull(passangerPrefab);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player") {
			Player player = other.gameObject.GetComponent<Player>();
			if (player.Occupied) {
				Debug.Log("drop off passanger");
				player.Occupied = false;
				Vector2 pos = new Vector2(player.transform.position.x, transform.position.y);
				PassangerDroppedOff pass = Instantiate(passangerPrefab, pos, Quaternion.identity); 
				pass.MoveTo(gatePosition);

			}
		}

	}
}

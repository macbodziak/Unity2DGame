using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	static public UIController instance = null;
	[SerializeField] ScorePanel scorePanel;
	[SerializeField] ScorePanel lifePanel;
	[SerializeField] OccupiedIcon occIcon;
	static public UIController Instance {
		get {
			return instance;
		}
	}

	private void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			if (instance != this) {
				Destroy (gameObject);
			}
		}
	}

	public void SetScore(int scr) {
		scorePanel.Score = scr;
	}

	public void SetPlayerOccupied(bool val) {
		occIcon.Occupied = val;
	}

	public void SetLifes(int val) {
		lifePanel.Score = val;
		Debug.Log("lifes = " + val);
	}


}
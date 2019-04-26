using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	static public GameController instance = null;
	int score = 0;
	[SerializeField] Player _player;
	int scoreToWin = 5;
	public AudioSource audioSource;
	[SerializeField] SpriteRenderer passangerIndicatorPrefab;
	[SerializeField] SpriteRenderer mothershipIndicatorPrefab;
	[SerializeField] GameObject mothershipGate;
	[SerializeField] ScreenShower gameOverScreen;
	[SerializeField] ScreenShower LevelCompletedScreen;
	[SerializeField] LevelChanger levelChanger;

	List<IndicatorMapper> passangerIndicatorList;
	IndicatorMapper mothershipIndicator;

	public Player player {
		get {
			return _player;
		}
	}
	static public GameController Instance {
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

		passangerIndicatorList = new List<IndicatorMapper> ();

		//register mothership indicator

		audioSource = GetComponent<AudioSource> ();
		Assert.IsNotNull (audioSource);
		Assert.IsNotNull (_player);
		Assert.IsNotNull (passangerIndicatorPrefab);
		Assert.IsNotNull (mothershipIndicatorPrefab);
		Assert.IsNotNull (mothershipGate);
		Assert.IsNotNull (gameOverScreen);
		Assert.IsNotNull (levelChanger);

		SpriteRenderer indicator = Instantiate (mothershipIndicatorPrefab, new Vector3 (0f, 0f, 5f), Quaternion.identity);
		indicator.gameObject.SetActive (false);
		mothershipIndicator = new IndicatorMapper (mothershipGate, indicator);
	}

	public int Score {
		get {
			return score;
		}
		set {
			score = value;
			UIController.Instance.SetScore (score);
			if (score >= scoreToWin) {
				OnGameWin ();
			}
		}
	}

	void OnGameWin () {
		LevelCompletedScreen.ShowScreen();
	}

	public void OnGameOver() {
		gameOverScreen.ShowScreen();
	}

	public void RestartLevel() {
		levelChanger.FadoutToLevel(SceneManager.GetActiveScene().buildIndex);
		// SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void LoadMainMenu() {
		levelChanger.FadoutToLevel(0);
	}

	public void LoadNextLevel() {
		int index = SceneManager.GetActiveScene().buildIndex;
		levelChanger.FadoutToLevel(index + 1);
	}
	public void QuitGame() {
		Application.Quit();
	}

	public void RegisterPassanger (Passanger passanger) {
		SpriteRenderer indicator = Instantiate (passangerIndicatorPrefab, new Vector3 (0f, 0f, 5f), Quaternion.identity);
		passangerIndicatorList.Add (new IndicatorMapper (passanger.gameObject, indicator));
	}

	public void UnregisterPassanger (Passanger Argpassanger) {
		for (int i = 0; i < passangerIndicatorList.Count; i++) {
			if (passangerIndicatorList[i].go == Argpassanger.gameObject) {
				Destroy (passangerIndicatorList[i].indicator);
				passangerIndicatorList.RemoveAt (i);
			}
		}
	}
	void DrawAllIndicators () {
		if (player == null) {
			return;
		}
		if (player.Occupied) {
			HidePassangerIndicators ();
			DrawIndicator (mothershipIndicator);

		} else {
			//draw pointers to passangers
			mothershipIndicator.indicator.gameObject.SetActive(false);
			foreach (IndicatorMapper im in passangerIndicatorList) {
				DrawIndicator (im);
			}
		}
	}

	private void Update () {
		DrawAllIndicators ();

	}

	private void DrawIndicator (IndicatorMapper im) {
		Vector2 vPassanger = Camera.main.WorldToViewportPoint (im.go.transform.position);
		if (vPassanger.x > 1.02f || vPassanger.x < -0.02f || vPassanger.y > 1.02f || vPassanger.y < -0.02f) {
			im.indicator.gameObject.SetActive (true);
			Vector2 WorldPos = new Vector2 (vPassanger.x - 0.5f, vPassanger.y - 0.5f) * 2f; // mapping to range [-1, 1]
			float max = Mathf.Max (Mathf.Abs (WorldPos.x), Mathf.Abs (WorldPos.y)); //get largest offset
			WorldPos = (WorldPos / (max * 2f)) + new Vector2 (0.5f, 0.5f); //undo mapping
			WorldPos = Camera.main.ViewportToWorldPoint (WorldPos);
			im.indicator.transform.position = WorldPos;

			Vector2 v = new Vector2 (im.go.transform.position.x - player.transform.position.x, im.go.transform.position.y - player.transform.position.y);
			float angle = Mathf.Atan2 (v.y, v.x);
			im.indicator.transform.rotation = Quaternion.Euler (0f, 0f, angle * Mathf.Rad2Deg);
		} else {
			im.indicator.gameObject.SetActive (false);
		}
	}

	void HidePassangerIndicators () {
		foreach (IndicatorMapper im in passangerIndicatorList) {
			im.indicator.gameObject.SetActive (false);
		}
	}
}
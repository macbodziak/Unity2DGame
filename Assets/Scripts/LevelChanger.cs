using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {
	int levelToLoad = 0;
	[SerializeField] Animator fadeOutAnimator;


	public void FadoutToLevel(int index) {
		levelToLoad = index;
		fadeOutAnimator.SetTrigger("FadeOut");
	}

	public void LoadLevel() {
		SceneManager.LoadScene(levelToLoad);
	}
}

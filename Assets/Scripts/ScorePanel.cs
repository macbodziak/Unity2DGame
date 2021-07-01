using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour {

	[SerializeField] Sprite pointSprite;
	[SerializeField] Sprite noPointSprite;
	[SerializeField] bool startFull = false;
	Image[] images;
	int score = 0;

	public int Score {
		get {
			return score;
		}

		set {
			if (value <= images.Length) {
				score = value;
				for(int i = 0; i < score; i++)
				{
					images[i].sprite = pointSprite;
				}
				for(int i = score; i < images.Length; i++)
				{
					images[i].sprite = noPointSprite;
				}
			}
		}
	}
	// Use this for initialization
	void Start () {
		images = GetComponentsInChildren<Image> ();
		if(Screen.width > 900) {
			RectTransform rt = GetComponent<RectTransform>();
			rt.sizeDelta = new Vector2(images.Length * 64 + (images.Length - 1) * 2 ,64);
			for(int i = 0; i < images.Length; i++) {
				rt = images[i].GetComponent<RectTransform>();
				rt.sizeDelta = new Vector2(64,64);
			}
		}
		if(startFull == true)
		{
			foreach(Image image in images)
			{
				image.sprite = pointSprite;
			}
		}
	}
}
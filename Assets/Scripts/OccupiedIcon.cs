using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
public class OccupiedIcon : MonoBehaviour
{

    [SerializeField] Sprite emptySprite;
    [SerializeField] Sprite occupiedSprite;
	[SerializeField] Color emptyColor;
	[SerializeField] Color occupiedColor;
    Image actualImage;
    bool occupied = false;
    public bool Occupied
    {
        set
        {
            occupied = value;
            if (value == true)
            {
                actualImage.sprite = occupiedSprite;
                actualImage.color = occupiedColor;
            }
            else
            {
                actualImage.sprite = emptySprite;
                actualImage.color = emptyColor;
            }
        }
        get
        {
            return occupied;
        }
    }
    private void Awake()
    {
        actualImage = gameObject.GetComponent<Image>();
        Assert.IsNotNull(emptySprite);
        Assert.IsNotNull(occupiedSprite);
        Assert.IsNotNull(actualImage);
		actualImage.color = emptyColor;
		
    }

    private void Start()
    {
        if (Screen.width > 900)
        {
            RectTransform rt = GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(64, 64);
        }
    }
}

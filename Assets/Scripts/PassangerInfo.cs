using UnityEngine;

public class IndicatorMapper {
	public GameObject go;
	public SpriteRenderer indicator;

	public IndicatorMapper (GameObject argGo, SpriteRenderer i) {
		go = argGo;
		indicator = i;
	}
}
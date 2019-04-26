using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	[SerializeField] ParticleSystem explosionPrefab;
	[SerializeField] AudioSource audioSource;

	private void Awake() {
		audioSource = GetComponent<AudioSource>();
	}
	private void OnCollisionEnter2D(Collision2D other) {
		ParticleSystem exp = Instantiate(explosionPrefab,transform.position, Quaternion.identity);
		Destroy(exp.gameObject, 1f);
		audioSource.Play();
		Destroy(gameObject);
	}
}

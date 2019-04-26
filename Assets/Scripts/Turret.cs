using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Turret : MonoBehaviour {

	[SerializeField] Transform gun;
	[SerializeField] Transform projectileSpawningPoint;
	[SerializeField] GameObject projectilePrefab;
	[SerializeField] AudioClip shootingSound;
	[SerializeField] float projectileSpeed = 5f;
	[SerializeField] float range = 5f;
	[SerializeField] ParticleSystem loadingPSPrefab;

	bool readyToShoot = true;
	float rotZ;
	Vector2 playerVector;
	AudioSource audioSource;

	[SerializeField] float reloadTime = 2.5f;
	// Use this for initialization
	private void Awake () {

		audioSource = GetComponent<AudioSource> ();
		Assert.IsNotNull (gun);
		Assert.IsNotNull (projectileSpawningPoint);
		Assert.IsNotNull (projectilePrefab);
		Assert.IsNotNull (shootingSound);
		Assert.IsNotNull (audioSource);
	}

	private void Start () {
		StartCoroutine (Shoot ());
	}
	// Update is called once per frame
	void Update () {
		if (GameController.instance.player != null) {
			playerVector = GameController.instance.player.transform.position - transform.position;
			rotZ = Mathf.Atan2 (playerVector.y, playerVector.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler (0f, 0f, rotZ);
		}
	}

	IEnumerator Shoot () {
		while (true) {
			RaycastHit2D hit = Physics2D.Raycast (projectileSpawningPoint.position, playerVector);
			if (hit.collider != null  && hit.collider.tag == "Player" && hit.distance <= range) {
				if (readyToShoot) {
					readyToShoot = false; 
					ParticleSystem laodingPS = Instantiate (loadingPSPrefab, projectileSpawningPoint.position, Quaternion.Euler (0f, 0f, rotZ), projectileSpawningPoint);
					yield return new WaitForSeconds (0.5f);
					Destroy(laodingPS.gameObject, 1.0f);
					GameObject projectile = Instantiate (projectilePrefab, projectileSpawningPoint.position, Quaternion.Euler (0f, 0f, rotZ));
					Rigidbody2D rb = projectile.GetComponent<Rigidbody2D> ();
					if (rb != null) {
						rb.velocity = playerVector.normalized * projectileSpeed;
					}
					audioSource.Play ();
					yield return new WaitForSeconds (reloadTime);
					readyToShoot = true;
				} else {
					yield return null;
				}
			}
			yield return null;
		}
	}
}
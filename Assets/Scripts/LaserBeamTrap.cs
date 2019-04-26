using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LaserBeamTrap : MonoBehaviour {

	[SerializeField] float TInactive;
	[SerializeField] float TActive;
	[SerializeField] float length;
	[SerializeField] ParticleSystem leftParticleSys;
	[SerializeField] ParticleSystem rightParticleSys;
	[SerializeField] ParticleSystem laserParticleSys;
	Animator anim;

	private void Awake() {
		anim = GetComponent<Animator>();
		Assert.IsNotNull(anim);
	}

	private void Start() {
		StartCoroutine(OnLaserOffCoroutine());
	}

	public void ActivateLoadingSequence() {
		// leftParticleSys.Play();
		// rightParticleSys.Play();
		laserParticleSys.Play();
	}

	public void DectivateLoadingSequence() {
		// leftParticleSys.Stop();
		// rightParticleSys.Stop();
		laserParticleSys.Stop();
		StartCoroutine(OnLaserOnCoroutine());
	}
	
	IEnumerator OnLaserOnCoroutine() {
		yield return new WaitForSeconds(TActive);
		anim.SetBool("Active", false);
		StartCoroutine(OnLaserOffCoroutine());
	}

	IEnumerator OnLaserOffCoroutine() {
		yield return new WaitForSeconds(TInactive);
		anim.SetBool("Active", true);
	}
}

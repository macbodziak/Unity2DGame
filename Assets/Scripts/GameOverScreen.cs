using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour {

    Animator animator;
    
    public void ShowScreen() {
        animator = GetComponent<Animator>();
        animator.SetTrigger("OnGameOver");
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Animator barrierAnimator;
    public bool isActive;

    void Start() {
        barrierAnimator.SetBool("isActive", isActive);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Switch Flipped");
        if (collision.CompareTag("Player")) {  
            isActive = !isActive;
            barrierAnimator.SetBool("isActive", isActive);
        }
    }
}

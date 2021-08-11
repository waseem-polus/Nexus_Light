using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierManagement : MonoBehaviour
{
    public bool isActive;

    [Space(10)]
    public Animator barrierAnimator;
    public Animator[] switchAnimators;

    void Start() {
        barrierAnimator.SetBool("isActive", isActive);
        for (int i = 0; i < switchAnimators.Length; i++) {
            switchAnimators[i].SetBool("isActive", isActive);
        }
    }

    public void FlipSwitch() {
        isActive = !isActive;
            
        barrierAnimator.SetBool("isActive", isActive);

        for (int i = 0; i < switchAnimators.Length; i++) {
            switchAnimators[i].SetBool("isActive", isActive);

            //TODO: Delete the debug statement
            Debug.Log(i);
        }
    }
}

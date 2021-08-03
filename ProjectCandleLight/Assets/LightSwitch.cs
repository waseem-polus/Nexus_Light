using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public BarrierManagement barrierManager;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {  
            barrierManager.FlipSwitch();
        }
    }
}

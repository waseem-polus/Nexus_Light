using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private int activeCheckpoint = 0;
    private Vector3 activeCheckpointPos;
        
    void Start()
    {    
        activeCheckpointPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
    }

    public int GetActiveCheckpoint() {
        return activeCheckpoint;
    }

    public void SetActiveCheckpoint(int index) {
        if (index > activeCheckpoint) {
            activeCheckpoint = index;
        }
    }

    public Vector3 GetActiveCheckpointPosition () {
        return activeCheckpointPos;
    }

    public void SetActiveCheckpointPosition(Vector3 position) {
        activeCheckpointPos = position;
    } 
}

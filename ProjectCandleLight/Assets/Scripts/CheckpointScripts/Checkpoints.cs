using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Checkpoints : MonoBehaviour
{
    private Animator animator;
    private Transform checkpointTransform;
    private CheckpointManager checkpointManager;
    
    private bool isActive = false;
    private int activeCheckpoint;
    
    public int checkpointIndex;

    void Start() {
        animator = GetComponent<Animator>();
        checkpointTransform = GetComponent<Transform>();
        checkpointManager = GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<CheckpointManager>();

        activeCheckpoint = checkpointManager.GetActiveCheckpoint();
    }

    void Update() {
        CheckIfActive();
        UpdateAnimations();
    }

    private void CheckIfActive() {
        activeCheckpoint = checkpointManager.GetActiveCheckpoint();
        
        if (activeCheckpoint == checkpointIndex) {
            isActive = true;
        } else {
            isActive = false;
        }
    }

    private void UpdateAnimations() {
        animator.SetBool("isActive", isActive);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && activeCheckpoint < checkpointIndex) {
            checkpointManager.SetActiveCheckpoint(checkpointIndex);
            checkpointManager.SetActiveCheckpointPosition(checkpointTransform.position);
        }
    }
}

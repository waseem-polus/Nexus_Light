using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagement : MonoBehaviour
{
    private static LevelManagement instance;

    private GameObject player;
    private CheckpointManager checkpointManager;

    public bool isLevel = true;

    public float freezeRespawnedPlayerFor = 0.5f;

    void Start() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (isLevel) {
            player = GameObject.FindGameObjectWithTag("Player");
            checkpointManager = GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<CheckpointManager>();
        }
    }

    void Update() {
        if (isLevel) {
            if (player == null) {
                player = GameObject.FindGameObjectWithTag("Player");
                Debug.Log("Player Renewed");
            }

            if (checkpointManager == null) {
                checkpointManager = GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<CheckpointManager>();
                Debug.Log("checkpointManager Renewed");
            }

            if (Input.GetButtonDown("Restart")) {
                ResetScene();
            }
        }
    }

    public void GoToScene(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }

    public void NextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResetScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RespawnPlayer() {
        player.transform.position = checkpointManager.GetActiveCheckpointPosition();

        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        DisablePlayerAnimations();

        StartCoroutine(FreezePlayer());
    }

    private IEnumerator FreezePlayer() {
        player.GetComponent<PlayerController>().enabled = false;

        yield return new WaitForSeconds(freezeRespawnedPlayerFor);
        
        player.GetComponent<PlayerController>().enabled = true;
    }

    private void DisablePlayerAnimations() {
        player.GetComponent<Animator>().SetBool("isRunning",false);
        player.GetComponent<Animator>().SetBool("isJumpingUp",false);
        player.GetComponent<Animator>().SetBool("isJumpingDown",false);
        player.GetComponent<Animator>().SetBool("isWallSliding",false);
    }

    public void StopTime() {
        Time.timeScale = 0.0f;
        player.GetComponent<PlayerController>().enabled = false;
    }

    public void ResumeTime() {
        Time.timeScale = 1.0f;
        player.GetComponent<PlayerController>().enabled = true;
        if (player.GetComponent<PlayerController>() == null) {
            Debug.Log("Controlle not found");
        }
    }

    public void EndOfLevel() {
        //take away control from player
        player.GetComponent<PlayerController>().enabled = false;

        //set animation to idle
        player.GetComponent<Animator>().SetBool("isRunning", false);
        player.GetComponent<Animator>().SetBool("isJumpingUp", false);
        player.GetComponent<Animator>().SetBool("isJumpingDown", false);
        player.GetComponent<Animator>().SetBool("isWallSliding", false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagement : MonoBehaviour
{
    private static LevelManagement instance;

    private CheckpointManager checkpointManager;
    private GameObject player;
    private PlayerController playerController;
    private bool timeIsFrozen = false;

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
            playerController = player.GetComponent<PlayerController>();
            checkpointManager = GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<CheckpointManager>();
        }
    }

    void Update() {
        if (isLevel) {
            if (player == null) {
                player = GameObject.FindGameObjectWithTag("Player");
                Debug.Log("Player Renewed");
            }

            if (playerController == null) {
                playerController = player.GetComponent<PlayerController>();
                Debug.Log("Player Controller Renewed");
            }

            if (checkpointManager == null) {
                checkpointManager = GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<CheckpointManager>();
                Debug.Log("checkpointManager Renewed");
            }
        }
    }

    public void GoToScene(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);

        if (timeIsFrozen) {
            ResumeTime();
        }
    }

    public void NextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        if (timeIsFrozen) {
            ResumeTime();
        }
    }

    public void ResetScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (timeIsFrozen) {
            ResumeTime();
        }
    }

    public void RespawnPlayer() {
        player.transform.position = checkpointManager.GetActiveCheckpointPosition();

        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        DisablePlayerAnimations();

        StartCoroutine(WaitFor(freezeRespawnedPlayerFor));
    }

    public IEnumerator WaitFor(float time) {
        playerController.enabled = false;

        yield return new WaitForSeconds(time);
        
        playerController.enabled = true;
    }

    public void StopTime() {
        Time.timeScale = 0.0f;
        playerController.enabled = false;
        timeIsFrozen = true;
    }

    public void ResumeTime() {
        Time.timeScale = 1.0f;
        playerController.enabled = true;
        timeIsFrozen = false;
    }

    public void EndOfLevel() {
        //take away control from player
        playerController.enabled = false;

        //set animation to idle
        DisablePlayerAnimations();
    }

    private void DisablePlayerAnimations() {
        player.GetComponent<Animator>().SetBool("isRunning",false);
        player.GetComponent<Animator>().SetBool("isJumpingUp",false);
        player.GetComponent<Animator>().SetBool("isJumpingDown",false);
        player.GetComponent<Animator>().SetBool("isWallSliding",false);
    }

    public bool GetIfTimeIsFrozen() {
        return timeIsFrozen;
    }
}

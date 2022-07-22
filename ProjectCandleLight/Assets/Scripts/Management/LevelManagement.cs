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
    private Animator levelTransition;
    private bool timeIsFrozen = false;
    private bool isLevel;
    private int previousLevelIndex;
    private int currentLevelIndex;

    public float freezeRespawnedPlayerFor = 0.5f;

    void Start() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(this);
        }

        instance.currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Previous: " + instance.previousLevelIndex + "\nCurrent: " + instance.currentLevelIndex);

        if (instance.currentLevelIndex > 0) {
            instance.isLevel = true;
            Debug.Log("This is a level");

            player = GameObject.FindGameObjectWithTag("Player");
            playerController = player.GetComponent<PlayerController>();
            checkpointManager = GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<CheckpointManager>();
            
            levelTransition = GameObject.Find("LevelTransition").GetComponent<Animator>();
            if (instance.currentLevelIndex != instance.previousLevelIndex) {
                levelTransition.SetTrigger("Start Of New Scene");
                instance.previousLevelIndex = currentLevelIndex;
            }

        } else {
            instance.isLevel = false;
            Debug.Log("This is not a level");
        }
    }

    void Update() {
        if (instance.isLevel) {
            if (player == null) {
                player = GameObject.FindGameObjectWithTag("Player");
            }

            if (playerController == null) {
                playerController = player.GetComponent<PlayerController>();
            }

            if (checkpointManager == null) {
                checkpointManager = GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<CheckpointManager>();
            }
        
            if (levelTransition == null) {
                levelTransition = GameObject.Find("LevelTransition").GetComponent<Animator>();
            }
        }
    }

    public void GoToScene(int sceneIndex) {
        previousLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);

        if (sceneIndex > 0) {
            int timesPlayed = PlayerPrefs.GetInt("Level" + sceneIndex + "TimesPlayed");
            PlayerPrefs.SetInt("Level" + sceneIndex + "TimesPlayed", timesPlayed + 1);
        }

        if (timeIsFrozen) {
            ResumeTime();
        }
    }

    public void NextScene() {
        previousLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(previousLevelIndex + 1);

        if ((previousLevelIndex + 1) > 0) {
            int timesPlayed = PlayerPrefs.GetInt("Level" + (previousLevelIndex + 1 ) + "TimesPlayed");
            PlayerPrefs.SetInt("Level" + (previousLevelIndex + 1 ) + "TimesPlayed", timesPlayed + 1);
        }

        if (timeIsFrozen) {
            ResumeTime();
        }
    }

    public void ResetScene() {
        previousLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(previousLevelIndex);

        if (previousLevelIndex > 0) {
            int timesPlayed = PlayerPrefs.GetInt("Level" + previousLevelIndex + "TimesPlayed");
            PlayerPrefs.SetInt("Level" + previousLevelIndex + "TimesPlayed", timesPlayed + 1);
        }

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

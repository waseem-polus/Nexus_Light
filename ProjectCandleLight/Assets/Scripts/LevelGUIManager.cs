using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGUIManager : MonoBehaviour
{
    private Timer timer;
    private Animator timerAnimator;
    private LevelManagement levelManager;

    public GameObject inLevelPanel;
    public GameObject pauseMenuPanel;
    public GameObject endOfLevelPanel;
    public GameObject optionsPanel;

    private bool isPaused = false;
    private bool isEOL = false;

    void Start() {
        timer = GameObject.Find("Canvas").GetComponent<Timer>();
        if (timer == null) {
            Debug.Log("timer not found");
        } else {
            timerAnimator = GameObject.Find("TimerText").GetComponent<Animator>();
        }

        levelManager = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();
        if (levelManager == null) {
            Debug.Log("levelManager not found");
        }

        inLevelPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
        endOfLevelPanel.SetActive(false);
        optionsPanel.SetActive(false);

        if (isPaused) {
            De_ActivatePausePanel();
        }

        if (isEOL) {
            isEOL = false;
        }
    }

    void Update() {
        if (levelManager == null) {
            levelManager = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();
        }

        if (inLevelPanel == null) {
            inLevelPanel = GameObject.Find("LevelHUD");
            Debug.Log("inLevelPanel renewed");
        }

        if (pauseMenuPanel == null) {
            pauseMenuPanel = GameObject.Find("PauseMenuPanel");
            Debug.Log("pauseMenuPanel renewed");
        }

        if (endOfLevelPanel == null) {
            endOfLevelPanel = GameObject.Find("EndOfLevelPanel");
            Debug.Log("endOfLevelPanel renewed");
        }

        if (optionsPanel == null) {
            optionsPanel = GameObject.Find("Options");
            Debug.Log("optionsPanel renewed");
        }

        if (Input.GetButtonDown("Cancel") && !isEOL) {
            if (isPaused) {
                De_ActivatePausePanel();
            } else {
                ActivatePausePanel();
            }
        }

        if (Input.GetButtonDown("Restart")) {
            levelManager.ResetScene();
            if (isPaused) {
                De_ActivatePausePanel();
            }
        }
    }

    public void De_ActivatePausePanel () {
        //update isPaused
        isPaused = false;

        //resume timer
        timer.StartTimer();
        levelManager.ResumeTime();

        //enable inlevelPanel
        inLevelPanel.SetActive(true);

        //disable pauseMenuPanel
        pauseMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void ActivatePausePanel() {
        //update isPaused
        isPaused = true;

        //pause timer        
        timer.StopTimer();
        levelManager.StopTime();
        
        //Disable inLevelPanel
        inLevelPanel.SetActive(false);
        optionsPanel.SetActive(false);
        
        //Enable PauseMenuPanel
        pauseMenuPanel.SetActive(true);
    }

    public void ActivateEndOfLevelPanel() {
        //set isEOL (is End of Level) to true
        isEOL = true;

        //deactivate pause and inlevel panels
        inLevelPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);

        //activate endoflevel panel
        endOfLevelPanel.SetActive(true);
    }

    public void ActivateOptionsPanel() {
        //Disable inLevelPanel
        inLevelPanel.SetActive(false);
        optionsPanel.SetActive(true);
        
        //Enable PauseMenuPanel
        pauseMenuPanel.SetActive(false);
    }
}

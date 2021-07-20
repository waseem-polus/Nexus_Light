using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGUIManager : MonoBehaviour
{
    private Timer timer;
    private Animator timerAnimator;
    private LevelManagement levelManager;

    private GameObject inLevelPanel;
    private GameObject pauseMenuPanel;
    private GameObject endOfLevelPanel;
    private GameObject optionsPanel;

    private bool isPaused = false;
    private bool isEOL = false;

    void Start() {
        timer = GameObject.Find("Canvas").GetComponent<Timer>();
        timerAnimator = GameObject.Find("TimerText").GetComponent<Animator>();
    
        levelManager = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();

        inLevelPanel = GameObject.Find("LevelHUD");
        inLevelPanel.SetActive(true);

        pauseMenuPanel = GameObject.Find("PauseMenuPanel");
        pauseMenuPanel.SetActive(false);

        endOfLevelPanel = GameObject.Find("EndOfLevelPanel");
        endOfLevelPanel.SetActive(false);

        optionsPanel = GameObject.Find("Options");
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

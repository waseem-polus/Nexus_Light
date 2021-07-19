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

        De_ActivatePausePanel();
    }

    void Update() {
        if (levelManager == null) {
            levelManager = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();
        }

        if (Input.GetButtonDown("Cancel")) {
            if (isPaused) {
                De_ActivatePausePanel();
            } else {
                ActivatePausePanel();
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

        //disable options panel
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
        
        //Enable PauseMenuPanel
        pauseMenuPanel.SetActive(true);

        //disable options panel
        optionsPanel.SetActive(false);
    }

    public void ActivateEndOfLevelPanel() {
        //stop timer
        timer.StopTimer();
        timerAnimator.SetTrigger("EndOfLevel");

        //deactivate pause and inlevel panels
        inLevelPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);

        //activate endoflevel panel
        endOfLevelPanel.SetActive(true);

        //disable options panel
        optionsPanel.SetActive(false);
    }

    public void ActivateOptionsPanel() {
        //Disable inLevelPanel
        inLevelPanel.SetActive(false);
        
        //Enable PauseMenuPanel
        pauseMenuPanel.SetActive(false);

        //disable options panel
        optionsPanel.SetActive(true);
    }
}

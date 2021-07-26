using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HomeManagement : MonoBehaviour
{
    public GameObject homePanel, levelsPanel, optionsPanel, statsPanel;

    [Space(10)] 
    public Animator sceneTransition;   

    [Space(10)]
    public LeaderboardEntry currentPlayerInfo;
    public LeaderboardEntry[] leaderboardEntries;

    private LevelManagement levelManager;

    void Start()
    {
        levelManager = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();

        homePanel.SetActive(true);
        levelsPanel.SetActive(false);
        optionsPanel.SetActive(false);
        statsPanel.SetActive(false);
    }

    
    void Update()
    {
        if (levelManager == null) {
            levelManager = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();
        }
    }

    public void ActivateHomePanel() {
        homePanel.SetActive(true);
        levelsPanel.SetActive(false);
        optionsPanel.SetActive(false);
        statsPanel.SetActive(false);  
    }

    public void ActivateLevelsPanel() {
        homePanel.SetActive(false);
        levelsPanel.SetActive(true);
        optionsPanel.SetActive(false);
        statsPanel.SetActive(false);
    }

    public void ActivateOptionsPanel() {
        homePanel.SetActive(false);
        levelsPanel.SetActive(false);
        optionsPanel.SetActive(true); 
        statsPanel.SetActive(false);
    }

    public void ActivateStatsPanel() {
        homePanel.SetActive(false);
        levelsPanel.SetActive(false);
        optionsPanel.SetActive(false); 
        statsPanel.SetActive(true);
    }

    public void GoToLevel(int levelIndex) {
        StartCoroutine(WaitFor(0.5f, levelIndex));
    }

    private IEnumerator WaitFor(float time, int levelIndex) {
        sceneTransition.SetTrigger("End Of Scene");

        yield return new WaitForSeconds(time);
        
        levelManager.GoToScene(levelIndex);
    }
}

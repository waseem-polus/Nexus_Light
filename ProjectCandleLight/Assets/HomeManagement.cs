using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HomeManagement : MonoBehaviour
{
    public GameObject homePanel, levelsPanel, optionsPanel, statsPanel;
    
    [Space(10)]
    public TMP_Text[] currentPlayerInfo, LeaderboardNames, leaderboardTimes;

    void Start()
    {
        homePanel.SetActive(true);
        levelsPanel.SetActive(false);
        optionsPanel.SetActive(false);
        statsPanel.SetActive(false);
    }

    
    void Update()
    {
        
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
}

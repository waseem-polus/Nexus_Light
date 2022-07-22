using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

public class Timer : MonoBehaviour
{
    private bool isActive = true;

    [HideInInspector]
    public float currentTime = 0;
    public TMP_Text currentTimeText;
    public TMP_Text bestTimerText;

    private void Update()
    {
        if (isActive) {
            currentTime += Time.deltaTime;
        }
        
        currentTimeText.text = FloatTimeToString(currentTime);
    }

    public void StartTimer() {
        isActive = true;
    }

    public void StopTimer() {
        isActive = false;
    }

    public void SaveTime() {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        float bestTime = PlayerPrefs.GetFloat("Level" + sceneIndex + "Time");

        if (bestTime > currentTime || bestTime <= 0) {
            PlayerPrefs.SetFloat("Level" + sceneIndex + "Time", currentTime);
            bestTimerText.text = "-" + FloatTimeToString(bestTime - currentTime) + " (New Best!)";
        } else {
            bestTimerText.text = "+" + FloatTimeToString(currentTime - bestTime);
        }
    }

    private string FloatTimeToString(float timeFloat) {
        TimeSpan time = TimeSpan.FromSeconds(timeFloat);
        return time.ToString(@"mm\:ss\.fff");
    }
}

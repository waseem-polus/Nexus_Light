using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Timer : MonoBehaviour
{
    public int intTime = 0;
    private float currentTime = 0;
    private bool isActive = true;

    public TMP_Text currentTimeText;

    void Update()
    {
        if (isActive) {
            currentTime += Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.ToString(@"mm\:ss\.fff");

        string stringTime = time.ToString(@"ssfff");
        intTime = int.Parse(stringTime);
    }


    public void StartTimer() {
        isActive = true;
    }

    public void StopTimer() {
        isActive = false;
    }
}

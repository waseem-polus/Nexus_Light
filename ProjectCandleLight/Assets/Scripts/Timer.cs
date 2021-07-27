using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Timer : MonoBehaviour
{
    private bool isActive = true;

    [HideInInspector]
    public float currentTime = 0;
    public TMP_Text currentTimeText;

    void Update()
    {
        if (isActive) {
            currentTime += Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.ToString(@"mm\:ss\.fff");
    }


    public void StartTimer() {
        isActive = true;
    }

    public void StopTimer() {
        isActive = false;
        Debug.Log(Math.Round(1000 * currentTime, 0));
    }
}

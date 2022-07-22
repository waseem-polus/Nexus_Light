using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class BestTimeStat : MonoBehaviour
{
    [Header("Level Info")]
    public int sceneIndex;
    public int levelNumber;

    [Header("Text Elements")]
    public TMP_Text levelNumberText;
    public TMP_Text bestTimeText;
    public TMP_Text timesPlayedText;

    private void Start() {
        levelNumberText.text = levelNumber + ".";
        bestTimeText.text = FloatTimeToString(PlayerPrefs.GetFloat("Level"+sceneIndex+"Time"));
        timesPlayedText.text = PlayerPrefs.GetInt("Level"+sceneIndex+"TimesPlayed").ToString();
    }

    private string FloatTimeToString(float timeFloat) {
        TimeSpan time = TimeSpan.FromSeconds(timeFloat);
        return time.ToString(@"mm\:ss\.fff");
    }
}

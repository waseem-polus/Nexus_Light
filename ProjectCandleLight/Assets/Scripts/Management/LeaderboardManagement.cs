using UnityEngine;
using System;

public class LeaderboardManagement : MonoBehaviour
{
    private static LeaderboardManagement instance;

    public Timer timer;
    public int leaderboardID;

    [Space(10)]
    public string memberID;
    public string playerName;
    
    private int msScore;
    private string formattedTimeScore;

    public void Start () {

        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SubmitTime() {
        msScore = (int) Math.Round(1000 * timer.currentTime, 0);
        Debug.Log(msScore);
    }

    public void DecodeScore() {
        TimeSpan time = TimeSpan.FromMilliseconds((double) msScore);
        formattedTimeScore = time.ToString(@"mm\:ss\.fff");

        Debug.Log(formattedTimeScore);
    }
}

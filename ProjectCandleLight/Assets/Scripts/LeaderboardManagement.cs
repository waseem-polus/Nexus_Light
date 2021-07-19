using LootLocker.Requests;
using UnityEngine;

public class LeaderboardManagement : MonoBehaviour
{
    private static LeaderboardManagement instance;

    public Timer timer;
    public int leaderboardID;

    [Space(10)]
    public string memberID;
    public string playerName;
    
    private int score;

    public void Start () {

        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        LootLockerSDKManager.StartSession("WassomiTest", (response) => {
            if (response.success) {
                Debug.Log("Session Started Seccessfully");
            } else {
                Debug.Log("Session Failed to Start");
            }
        });
    }

    public void SubmitTime() {
        score = timer.intTime;
        Debug.Log(score);

        LootLockerSDKManager.SubmitScore(memberID, score, leaderboardID, (response) => {
            if (response.success) {
                Debug.Log("Score Submitted Successfulyy");
            } else {
                Debug.Log("Score Not Submitted");
            }
        });
    }
}

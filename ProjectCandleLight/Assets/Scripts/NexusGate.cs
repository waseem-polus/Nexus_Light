using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusGate : MonoBehaviour
{
    private Animator cubeAnimator;
    private Animator timerAnimator;
    private LevelManagement levelManager;
    private Timer timer;
    
    public GameObject endOfLevelPanel;
    public GameObject inLevelHUD;

    void Start()
    {
        cubeAnimator = GetComponent<Animator>();
        timerAnimator = GameObject.Find("TimerText").GetComponent<Animator>();
        levelManager = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();

        timer = GameObject.Find("Canvas").GetComponent<Timer>();

        endOfLevelPanel.SetActive(false);
        inLevelHUD.SetActive(true);
    }

    void Update()
    {
        if (levelManager == null) {
            levelManager = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            cubeAnimator.SetTrigger("PlayerCollision");
            
            timer.StopTimer();
            levelManager.EndOfLevel();

            StartCoroutine(WaitFor(0.5f));
        }

    }

    IEnumerator WaitFor(float time) {
        yield return new WaitForSeconds(time);

        timerAnimator.SetTrigger("EndOfLevel");

        endOfLevelPanel.SetActive(true);
        inLevelHUD.SetActive(false);
    }
}

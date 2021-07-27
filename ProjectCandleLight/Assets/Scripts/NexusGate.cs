using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusGate : MonoBehaviour
{
    public ParticleSystem activationParticles;
    public ParticleSystem idleParticle;
    public Animator cubeAnimator;

    private Animator levelStatsAnimator;
    private LevelManagement levelManager;
    private Timer timer;
    private LevelGUIManager GUIManager;

    void Start()
    {
        levelStatsAnimator = GameObject.Find("LevelStats").GetComponent<Animator>();
        levelManager = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();
        GUIManager = GameObject.Find("LevelGUIManager").GetComponent<LevelGUIManager>();

        timer = GameObject.Find("Canvas").GetComponent<Timer>();
    }

    void Update()
    {
        if (levelManager == null) {
            levelManager = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();
        }

        if (GUIManager == null) {
            GUIManager = GameObject.Find("LevelGUIManager").GetComponent<LevelGUIManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            cubeAnimator.SetTrigger("PlayerCollision");
            
            timer.StopTimer();
            levelManager.EndOfLevel();

            StartCoroutine(WaitFor(0.5f));
            StartCoroutine(WaitForParticle(0.2f));
        }

    }

    IEnumerator WaitFor(float time) {
        yield return new WaitForSeconds(time);

        levelStatsAnimator.SetTrigger("EndOfLevel");
        idleParticle.Stop();

        GUIManager.ActivateEndOfLevelPanel();
    }

    IEnumerator WaitForParticle(float time) {
        yield return new WaitForSeconds(time);

        activationParticles.Play();
    }
}

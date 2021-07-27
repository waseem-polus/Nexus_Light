using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lasers : MonoBehaviour
{
    private LevelManagement levelManager;
    private Animator playerAnimator;

    private int deaths = 0;

    public float cameraShakeIntensity = 10f;
    public float cameraShakeDurtion = 0.15f;

    public float respawnDelay = 0.15f;
    public TMP_Text deathCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManagement").GetComponent<LevelManagement>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    
        deathCounter.text = deaths.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager == null) {
            levelManager = GameObject.FindGameObjectWithTag("LevelManagement").GetComponent<LevelManagement>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraShake.Instance.ShakeCamera(cameraShakeIntensity, cameraShakeDurtion);
            StartCoroutine(WaitFor());
        }
    }

    private IEnumerator WaitFor() {
        yield return new WaitForSeconds(respawnDelay); 
        
        levelManager.RespawnPlayer();

        deaths++;
        deathCounter.text = deaths.ToString();
    } 
}

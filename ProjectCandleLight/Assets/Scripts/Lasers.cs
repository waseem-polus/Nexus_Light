using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    private LevelManagement levelManager;
    private Animator playerAnimator;

    public float respawnDelay = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManagement").GetComponent<LevelManagement>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
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
            //playerAnimator.SetTrigger("playerDeath");
            
            StartCoroutine(WaitFor());
        }
    }

    private IEnumerator WaitFor() {
        yield return new WaitForSeconds(respawnDelay); 
        
        levelManager.RespawnPlayer();
    } 
}

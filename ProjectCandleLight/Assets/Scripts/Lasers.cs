using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    private LevelManagement levelManager;
    
    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManagement").GetComponent<LevelManagement>();
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
            levelManager.RespawnPlayer();
        }
    }
}

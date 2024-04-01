using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //assign waypoints here
    public GameObject[] waypoints1;
    public GameObject[] waypoints2;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    public GameObject warningScreen;
    public int enemiesAlive; //counts how many enemies are present in the scene so you can progress when enemies = 0
    public int towerHealth = 200;
    public int currentHealth;
    public bool stopGame;

    EnemySpawner enemySpawner;


    private void Awake()
    {
        enemySpawner = GetComponent<EnemySpawner>();
        stopGame = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = towerHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth < 1)
        {
            gameOverScreen.SetActive(true); return;
        }
    }

    private void GameStop()
    {
   
    }
}

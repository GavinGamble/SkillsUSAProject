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
    public int coinCount;
    public int towerCost = -10;
    public int waveNumber = 1;
    public bool stopGame;


    EnemySpawner enemySpawner;


    private void Awake()
    {
        enemySpawner = GetComponent<EnemySpawner>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = towerHealth;
        waveNumber = 1;
        coinCount = 15;
        UIManager.Instance.UpdateWaveNumber(waveNumber);
        
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(coinCount);
        if (currentHealth < 1)
        {
            gameOverScreen.SetActive(true);
            Destroy(GameObject.Find("EnemySpawner"));
            return;
        }

        if (waveNumber == 60)
        {
            if (enemiesAlive >= 0)
            {
                Destroy(GameObject.Find("EnemySpawner"));
                winScreen.SetActive(true);
            }
        }
    }
}

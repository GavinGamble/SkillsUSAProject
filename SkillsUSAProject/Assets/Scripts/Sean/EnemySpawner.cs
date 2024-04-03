using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Assign your enemy prefabs in the inspector
    public GameObject warningScreen;
    private float spawnInterval = 2.0f;
    private int enemiesToSpawn = 5;
    private int coinsForWaveCompletion = 10; // Coins awarded for completing a wave
    public int coinsForKill = 2; // Coins awarded for each enemy kill

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.enemiesAlive = enemiesToSpawn;
    }
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        
        while (true) // Infinite loop to keep spawning waves
        {
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnInterval);
            }
            

                // Increase for next wave
                

                gameManager.waveNumber++;
                enemiesToSpawn += 5; // Increase the number of enemies per wave
                gameManager.enemiesAlive = enemiesToSpawn; // JASON : makes the enemy count match the amount spawned
                spawnInterval *= 0.9f; // Optional: decrease spawn interval to make it harder
                                       // Update wave number in UI
            gameManager.coinCount = gameManager.coinCount + coinsForWaveCompletion;

            
                UIManager.Instance.UpdateWaveNumber(gameManager.waveNumber);

            // Reward player for surviving a wave
            UIManager.Instance.AddCoins(coinsForWaveCompletion);

            

                yield return new WaitForSeconds(30); // Wait before next wave
            
        }
    }

    void SpawnEnemy()
    {
        int enemyIndex = ChooseEnemyIndex();
        Instantiate(enemyPrefabs[enemyIndex], transform.position, Quaternion.identity);
    }

    public int ChooseEnemyIndex()
    {
        // Simple logic to increase the chance of harder enemies over waves
        float progress = (float)gameManager.waveNumber / 10; // Adjust this based on your game's difficulty curve
        float randomNumber = Random.Range(0f, 1f); 

        if (randomNumber < progress && gameManager.waveNumber > 5) // Adjust conditions based on your needs
        {
            return Random.Range(1, enemyPrefabs.Length); // Spawn harder enemies
        }
        else
        {
            return 0; // Spawn the easiest enemy
        }
    }

    private void Update()
    {

    }
}

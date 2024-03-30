using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerJs : MonoBehaviour
{


    public GameObject[] enemyPrefabs; // Assign your enemy prefabs in the inspector
    public int waveNumber = 1;
    private float spawnInterval = 2.0f;
    private int enemiesToSpawn = 5;
    private int coinsForWaveCompletion = 10; // Coins awarded for completing a wave
    private int coinsForKill = 2; // Coins awarded for each enemy kill

    public GameObject[] waypoints;
    public int currentWP = 0;

    public float speed = 30.0f;


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
            waveNumber++;
            enemiesToSpawn += 2; // Increase the number of enemies per wave
            spawnInterval *= 0.9f; // Optional: decrease spawn interval to make it harder
            // Update wave number in UI
            UIManager.Instance.UpdateWaveNumber(waveNumber);

            // Reward player for surviving a wave
            UIManager.Instance.AddCoins(coinsForWaveCompletion);

            yield return new WaitForSeconds(10); // Wait before next wave
        }
    }

     public void SpawnEnemy()
    {
        int enemyIndex = ChooseEnemyIndex();
        Instantiate(enemyPrefabs[enemyIndex], transform.position, Quaternion.identity);

        if (Vector3.Distance(enemyPrefabs[enemyIndex].transform.position, waypoints[currentWP].transform.position) < 3.0f)
        {
            currentWP++;
        }

        if (currentWP >= waypoints.Length)
        {

            currentWP = 0;
        }

        transform.LookAt(waypoints[currentWP].transform.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public int ChooseEnemyIndex()
    {
        // Simple logic to increase the chance of harder enemies over waves
        float progress = (float)waveNumber / 10; // Adjust this based on your game's difficulty curve
        float randomNumber = Random.Range(0f, 1f);

        if (randomNumber < progress && waveNumber > 5) // Adjust conditions based on your needs
        {
            return Random.Range(1, enemyPrefabs.Length); // Spawn harder enemies
        }
        else
        {
            return 0; // Spawn the easiest enemy
        }
    }
}

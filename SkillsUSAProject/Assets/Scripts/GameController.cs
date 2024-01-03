using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool GameShouldStop;
    private static Queue<int> EnemyIDToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        EnemyIDToSpawn = new Queue<int>();
        EnemySpawner.Init();

        //StartCoroutine(GameLoop());
        //InvokeRepeating("TestSpawn", 0, 10);
        //InvokeRepeating("TestRemove", 0, 1.5f);
    }

    void TestSpawn()
    {
        EnqueueEnemyIDToSPawn(1);
    }

    void TestRemove()
    {
        if(EnemySpawner.enemiesAlive.Count > 0)
        {
            EnemySpawner.RemoveEnemy(EnemySpawner.enemiesAlive[Random.Range(0, EnemySpawner.enemiesAlive.Count)]);
        }
    }

    IEnumerator GameLoop()
    {
        while(GameShouldStop == false)
        {
            //Spawn Enemies
            if(EnemyIDToSpawn.Count > 0)
            {
                for(int i = 0; i < EnemyIDToSpawn.Count; i++)
                {
                    EnemySpawner.SpawnEnemy(EnemyIDToSpawn.Dequeue());
                }
            }




            yield return null;
        }
    }

    public static void EnqueueEnemyIDToSPawn(int ID)
    {
        EnemyIDToSpawn.Enqueue(ID);
    }
}

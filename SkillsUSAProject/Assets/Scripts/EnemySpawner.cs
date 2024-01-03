using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static List<Enemies> enemiesAlive;
    public static Dictionary<int, GameObject> enemyPrefab;
    public static Dictionary<int, Queue<Enemies>> enemiesQueue;
    private static bool IsInitilized;

    // Start is called before the first frame update
    public static void Init()
    {

        if (!IsInitilized)
        {
            enemyPrefab = new Dictionary<int, GameObject>();
            enemiesQueue = new Dictionary<int, Queue<Enemies>>();
            enemiesAlive = new List<Enemies>();

            EnemySummonData[] Enemies = Resources.LoadAll<EnemySummonData>("Enemies");
            foreach (EnemySummonData enemy in Enemies)
            {
                enemyPrefab.Add(enemy.enemyID, enemy.enemyPrefab);
                enemiesQueue.Add(enemy.enemyID, new Queue<Enemies>());
            }

            IsInitilized = true;
        }
        else
        {
            Debug.Log("This class is already initilazed");
        }


    }

    public static Enemies SpawnEnemy(int enemyID)
    {
        Enemies SpawnedEnemy = null;
        if(enemyPrefab.ContainsKey(enemyID))
        {
            Queue<Enemies> RefrencedQueue = enemiesQueue[enemyID];

            if (RefrencedQueue.Count > 0)
            {
                //dequeue and initilize
                SpawnedEnemy = RefrencedQueue.Dequeue();
                SpawnedEnemy.Init();
                SpawnedEnemy.gameObject.SetActive(true);
            }
            else
            {
                //Initilize new instance and initilize
                GameObject newEnemy = Instantiate(enemyPrefab[enemyID],Vector3.zero, Quaternion.identity);
                SpawnedEnemy = newEnemy.GetComponent<Enemies>();
                SpawnedEnemy.Init();
            }
        }
        else
        {
            return null;
        }
        enemiesAlive.Add(SpawnedEnemy);
        SpawnedEnemy.ID = enemyID;
        return SpawnedEnemy;
    }
    public static void RemoveEnemy(Enemies EnemyToRemove)
    {
        enemiesQueue[EnemyToRemove.ID].Enqueue(EnemyToRemove);
        EnemyToRemove.gameObject.SetActive(false);
        enemiesAlive.Remove(EnemyToRemove);
    }
}

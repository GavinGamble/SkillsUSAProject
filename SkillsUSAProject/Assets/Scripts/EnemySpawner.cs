using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static List<Enemies> EnemiesAlive;
    public static List<Transform> EnemiesAliveTransform;
    public static Dictionary<int, GameObject> EnemyPrefab;
    public static Dictionary<int, Queue<Enemies>> EnemiesQueue;
    private static bool IsInitilized;


    public static void Init()
    {

        if (!IsInitilized)
        {
            EnemyPrefab = new Dictionary<int, GameObject>();
            EnemiesQueue = new Dictionary<int, Queue<Enemies>>();
            EnemiesAlive = new List<Enemies>();
            EnemiesAliveTransform = new List<Transform>();

            EnemySummonData[] Enemies = Resources.LoadAll<EnemySummonData>("Enemies");
            foreach (EnemySummonData enemy in Enemies)
            {
                EnemyPrefab.Add(enemy.EnemyID, enemy.EnemyPrefab);
                EnemiesQueue.Add(enemy.EnemyID, new Queue<Enemies>());
            }

            IsInitilized = true;
        }
        else
        {
            Debug.Log("This class is already initilazed");
        }


    }

    public static Enemies SpawnEnemy(int EnemyID)
    {
        Enemies SpawnedEnemy = null;
        if(EnemyPrefab.ContainsKey(EnemyID))
        {
            Queue<Enemies> RefrencedQueue = EnemiesQueue[EnemyID];

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
                GameObject newEnemy = Instantiate(EnemyPrefab[EnemyID], GameController.NodePos[0], Quaternion.identity);
                SpawnedEnemy = newEnemy.GetComponent<Enemies>();
                SpawnedEnemy.Init();
            }
        }
        else
        {
            return null;
        }
        EnemiesAliveTransform.Add(SpawnedEnemy.transform);
        EnemiesAlive.Add(SpawnedEnemy);
        SpawnedEnemy.ID = EnemyID;
        return SpawnedEnemy;
    }
    public static void RemoveEnemy(Enemies EnemyToRemove)
    {
        EnemiesQueue[EnemyToRemove.ID].Enqueue(EnemyToRemove);
        EnemyToRemove.gameObject.SetActive(false);
        EnemiesAliveTransform.Remove(EnemyToRemove.transform);
        EnemiesAlive.Remove(EnemyToRemove);
    }
}

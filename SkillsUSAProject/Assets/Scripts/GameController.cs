using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class GameController : MonoBehaviour
{
    public static Vector3[] NodePos;
    public Transform NodeParent;
   
    public bool GameShouldStop;

    private static Queue<Enemies> EnemiesToRemove;
    private static Queue<int> EnemyIDToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        EnemyIDToSpawn = new Queue<int>();
        EnemySpawner.Init();

        NodePos = new Vector3[NodeParent.childCount];

        for(int i = 0; i < NodePos.Length; i++)
        {
            NodePos[i] = NodeParent.GetChild(i).position;
        }

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
        if(EnemySpawner.EnemiesAlive.Count > 0)
        {
            EnemySpawner.RemoveEnemy(EnemySpawner.EnemiesAlive[Random.Range(0, EnemySpawner.EnemiesAlive.Count)]);
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

            //remove enemies
            if (EnemiesToRemove.Count > 0)
            {
                for (int i = 0; i < EnemiesToRemove.Count; i++)
                {
                    EnemySpawner.RemoveEnemy(EnemiesToRemove.Dequeue());
                }
            }

            //move enemies
            NativeArray<Vector3> NodesToUse = new NativeArray<Vector3>(NodePos, Allocator.TempJob);
            NativeArray<int> NodeIndexes = new NativeArray<int>(EnemySpawner.EnemiesAlive.Count , Allocator.TempJob);
            NativeArray<float> EnemySpeed = new NativeArray<float>(EnemySpawner.EnemiesAlive.Count, Allocator.TempJob);
            TransformAccessArray EnemyAccess = new TransformAccessArray(EnemySpawner.EnemiesAliveTransform.ToArray(),2);

            for (int i = 0; i < EnemySpawner.EnemiesAlive.Count; i++)
            {
                EnemySpeed[0] = EnemySpawner.EnemiesAlive[i].Speed;
                NodeIndexes[0] = EnemySpawner.EnemiesAlive[i].NodePoint;
            }

            MoveEnemiesJob MoveEnemies = new MoveEnemiesJob
            {
                NodePos = NodesToUse,
                EnemySpeed = EnemySpeed,
                NodeIndex = NodeIndexes,
                deltaTime = Time.deltaTime

            };

            JobHandle MoveHandle = MoveEnemies.Schedule(EnemyAccess);
            MoveHandle.Complete();

            for(int i = 0; i < EnemySpawner.EnemiesAlive.Count; i++)
            {
                EnemySpawner.EnemiesAlive[i].NodePoint = NodeIndexes[i];
            }

            NodeIndexes.Dispose();
            NodesToUse.Dispose();
            EnemySpeed.Dispose();
            EnemyAccess.Dispose();

            yield return null;
        }
    }

    public static void EnqueueEnemyIDToSPawn(int ID)
    {
        EnemyIDToSpawn.Enqueue(ID);
    }

    public static void EnqueueEnemyToRemove(Enemies EnemyToRemove)
    {
        EnemiesToRemove.Enqueue(EnemyToRemove);
    }

    public struct MoveEnemiesJob : IJobParallelForTransform
    {
        public NativeArray<float> EnemySpeed;
        public NativeArray<Vector3> NodePos;
        public NativeArray<int> NodeIndex;
        public float deltaTime;
        public void Execute(int index, TransformAccess transform)
        {
            Vector3 PosToMoveTo = NodePos[NodeIndex[index]];

            transform.position = Vector3.MoveTowards(transform.position, PosToMoveTo, EnemySpeed[index] * deltaTime);

            if(transform.position == PosToMoveTo)
            {
                NodeIndex[index]++;
            }
        }
    }
}

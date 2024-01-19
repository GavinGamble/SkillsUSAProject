using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;


public class GameController : MonoBehaviour
{
    public static float[] NodeDistance;
    public static List<TowerBehavior> TowersInGame;

    public static Vector3[] NodePos;
    public Transform NodeParent;

    public bool GameShouldStop;

    private static Queue<Enemies> EnemiesToRemove;
    private static Queue<int> EnemyIDToSpawn;
    // Start is called before the first frame update
    void Start()
    {

        TowersInGame = new List<TowerBehavior>();
        EnemiesToRemove = new Queue<Enemies>();
        EnemyIDToSpawn = new Queue<int>();
        EnemySpawner.Init();

        NodePos = new Vector3[NodeParent.childCount];

        for (int i = 0; i < NodePos.Length; i++)
        {
            NodePos[i] = NodeParent.GetChild(i).position;
        }

        NodeDistance = new float[NodePos.Length - 1];

        for (int i = 0; i < NodeDistance.Length; i++)
        {
            NodeDistance[i] = Vector3.Distance(NodePos[i], NodePos[i + 1]);
        }
        StartCoroutine(GameLoop());
        InvokeRepeating("TestSpawn", 0, 10);
    }

    void TestSpawn()
    {
        EnqueueEnemyIDToSPawn(1);
    }


    IEnumerator GameLoop()
    {
        while (GameShouldStop == false)
        {
            //Spawn Enemies
            if (EnemyIDToSpawn.Count > 0)
            {
                for (int i = 0; i < EnemyIDToSpawn.Count; i++)
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
            NativeArray<int> NodeIndexes = new NativeArray<int>(EnemySpawner.EnemiesAlive.Count, Allocator.TempJob);
            NativeArray<float> EnemySpeed = new NativeArray<float>(EnemySpawner.EnemiesAlive.Count, Allocator.TempJob);
            TransformAccessArray EnemyAccess = new TransformAccessArray(EnemySpawner.EnemiesAliveTransform.ToArray(), 2);

            for (int i = 0; i < EnemySpawner.EnemiesAlive.Count; i++)
            {
                EnemySpeed[i] = EnemySpawner.EnemiesAlive[i].Speed;
                NodeIndexes[i] = EnemySpawner.EnemiesAlive[i].NodePoint;
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

            for (int i = 0; i < EnemySpawner.EnemiesAlive.Count; i++)
            {
                EnemySpawner.EnemiesAlive[i].NodePoint = NodeIndexes[i];

                if (EnemySpawner.EnemiesAlive[i].NodePoint == NodePos.Length)
                {
                    EnqueueEnemyToRemove(EnemySpawner.EnemiesAlive[i]);
                }
            }

            NodeIndexes.Dispose();
            NodesToUse.Dispose();
            EnemySpeed.Dispose();
            EnemyAccess.Dispose();

            yield return null;

            //tick towers
            /*foreach(TowerBehavior tower in TowersInGame)
            {
                tower.Target = Targeting.GetTarget(tower, Targeting.TargetType.First);
                tower.Tick();
            }*/
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
        [NativeDisableParallelForRestriction]
        public NativeArray<float> EnemySpeed;
        [NativeDisableParallelForRestriction]
        public NativeArray<Vector3> NodePos;
        [NativeDisableParallelForRestriction]
        public NativeArray<int> NodeIndex;
        public float deltaTime;
        public void Execute(int index, TransformAccess transform)
        {
            if (NodeIndex[index] < NodePos.Length)
            {
                Vector3 PosToMoveTo = NodePos[NodeIndex[index]];
                transform.position = Vector3.MoveTowards(transform.position, PosToMoveTo, EnemySpeed[index] * deltaTime);
                if (transform.position == PosToMoveTo)
                {
                    NodeIndex[index]++;
                }
            }
        }
    }
}

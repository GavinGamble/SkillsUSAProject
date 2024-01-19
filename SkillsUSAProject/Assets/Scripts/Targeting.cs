using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using Unity.Collections;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;

public class Targeting 
{
    public enum TargetType
    {
        First,
        Close,
        Last
    }
    public static Enemies GetTarget(TowerBehavior CurrentTower, TargetType TargetMethod)
    {
        Collider[] InRangeEnemies = Physics.OverlapSphere(CurrentTower.transform.position, CurrentTower.Range, CurrentTower.EnemiesLayer);
        NativeArray<EnemyData> EnemiesToCalc = new NativeArray<EnemyData>(InRangeEnemies.Length, Allocator.TempJob);
        NativeArray<Vector3> NodePositions = new NativeArray<Vector3>(GameController.NodePos, Allocator.TempJob);
        NativeArray<float> NodeDistances = new NativeArray<float>(GameController.NodeDistance, Allocator.TempJob);
        NativeArray<int> EnemyToIndex = new NativeArray<int>(new int[] {-1}, Allocator.TempJob);
        int EnemyIndexToReturn = -1;

        for (int i = 0; i < EnemiesToCalc.Length; i++)
        {
            Enemies CurEnemy = InRangeEnemies[i].transform.parent.GetComponent<Enemies>();
            int EnemyIndexInList = EnemySpawner.EnemiesAlive.FindIndex(x => x == CurEnemy);
            EnemiesToCalc[i] = new EnemyData(CurEnemy.transform.position, CurEnemy.NodePoint, CurEnemy.CurHealth, EnemyIndexInList);
        }


        SearchForEnemies searchForEnemiesJob = new SearchForEnemies
        {
            _EnemiesToCalc = EnemiesToCalc,
            _NodeDistances = NodeDistances,
            _NodePositions = NodePositions,
            _EnemyToIndex = EnemyToIndex,
            TargetingType = (int)TargetMethod,
            TowerPosition = CurrentTower.transform.position
        };

        switch ((int)TargetMethod)
        {
            case 0: // first
                searchForEnemiesJob.CompareValue = Mathf.Infinity;
                break;

            case 1: // close
                searchForEnemiesJob.CompareValue = Mathf.NegativeInfinity;
                break;

            case 2: //last
                goto case 0;

        }

        JobHandle dependancy = new JobHandle();
        JobHandle SeachJobHandle = searchForEnemiesJob.Schedule(EnemiesToCalc.Length, dependancy);
        SeachJobHandle.Complete();
        if (EnemyToIndex[0] != -1)
        {
            EnemiesToCalc.Dispose();
            NodePositions.Dispose();
            NodeDistances.Dispose();
            EnemyToIndex.Dispose();
            EnemyIndexToReturn = EnemiesToCalc[EnemyToIndex[0]].EnemyIndex;
            return EnemySpawner.EnemiesAlive[EnemyIndexToReturn];
        }
        

        EnemiesToCalc.Dispose();
        NodePositions.Dispose();
        NodeDistances.Dispose();
        EnemyToIndex.Dispose();
            return null;
        
    }
    struct EnemyData
    {
        public EnemyData(Vector3 position, int nodeIndex, float HP, int enemyIndex)
        {
            EnemyPosition = position;
            NodeIndex = nodeIndex;
            Health = HP;
            EnemyIndex = enemyIndex;
        }
        public Vector3 EnemyPosition;
        public int EnemyIndex;
        public int NodeIndex;
        public float Health;
    }

    struct SearchForEnemies : IJobFor
    {
        public NativeArray<EnemyData> _EnemiesToCalc;
        public NativeArray<Vector3> _NodePositions;
        public NativeArray<float> _NodeDistances;
        public NativeArray<int> _EnemyToIndex;
        public Vector3 TowerPosition;

        public int TargetingType;
        public float CompareValue;
        public void Execute(int index)
        {
            float CurEnemyDistanceToEnd = 0;
            switch (TargetingType)
            {
                case 0: //first
                     CurEnemyDistanceToEnd = GetDistanceToEnd(_EnemiesToCalc[index]);
                    if(CurEnemyDistanceToEnd < CompareValue)
                    {
                        _EnemyToIndex[0] = index;
                        CompareValue = CurEnemyDistanceToEnd;
                    }
                    break;
                case 1: // close
                     CurEnemyDistanceToEnd = GetDistanceToEnd(_EnemiesToCalc[index]);
                    if (CurEnemyDistanceToEnd > CompareValue)
                    {
                        _EnemyToIndex[0] = index;
                        CompareValue = CurEnemyDistanceToEnd;
                    }
                    break;
                case 2: //last
                    CurEnemyDistanceToEnd = Vector3.Distance(TowerPosition, _EnemiesToCalc[index].EnemyPosition);
                    if (CurEnemyDistanceToEnd < CompareValue)
                    {
                        _EnemyToIndex[0] = index;
                        CompareValue = CurEnemyDistanceToEnd;
                    }
                    break;
            }
        }

        private float GetDistanceToEnd(EnemyData EnemyToEval)
        {
            float FinalDistance = Vector3.Distance(EnemyToEval.EnemyPosition, _NodePositions[EnemyToEval.NodeIndex]);


            for(int i = EnemyToEval.NodeIndex; i < _NodeDistances.Length; i++)
            {
                FinalDistance += _NodeDistances[i];
            }
            return FinalDistance;
        }
    }
}

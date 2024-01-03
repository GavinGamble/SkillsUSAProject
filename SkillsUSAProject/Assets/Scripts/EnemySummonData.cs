using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "Create EnemySummonData")]

public class EnemySummonData : ScriptableObject
{
    public GameObject enemyPrefab;
    public int enemyID;
  
}

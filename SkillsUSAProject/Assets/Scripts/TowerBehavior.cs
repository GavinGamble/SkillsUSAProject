using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    public LayerMask EnemiesLayer;
    public Enemies Target;
    public Transform TowerPivot;

    public float Damage;
    public float Firerate;
    public float Range;
    public float Delay;

    // Start is called before the first frame update
    void Start()
    {
        Delay = 1 / Firerate;
    }

   public void Tick()
    {
        if(Target != null)
        {
            TowerPivot.transform.rotation = Quaternion.LookRotation(Target.transform.position - transform.position);
        }
    }
}

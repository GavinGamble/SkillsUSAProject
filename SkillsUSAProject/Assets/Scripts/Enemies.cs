using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public int NodePoint;
    public float CurHealth;
    public float MaxHealth;
    public float Speed;
    public int ID;
    
    public void Init()
    {
        CurHealth = MaxHealth;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float curHealth;
    public float maxHealth;
    public float speed;
    public int ID;
    
    public void Init()
    {
        curHealth = maxHealth;
    }
    
}

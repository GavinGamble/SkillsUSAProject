using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //assign waypoints here
    public GameObject[] waypoints1;
    public int towerHealth = 200;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = towerHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointBehavior : MonoBehaviour
{
    public GameObject[] waypoints;
    public int currentWP = 0;

    public float speed = 30.0f;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();   
    }

    
    // Update is called once per frame
    void Update()
    {

        gameManager.waypoints1 = waypoints;
        gameManager.currentWP = currentWP;
        


        if (Vector3.Distance(transform.position, waypoints[currentWP].transform.position) < 3.0f)
        {
            currentWP++;
        }

        if (currentWP >= waypoints.Length)
        {

            currentWP = 0;
        }

        transform.LookAt(waypoints[currentWP].transform.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}

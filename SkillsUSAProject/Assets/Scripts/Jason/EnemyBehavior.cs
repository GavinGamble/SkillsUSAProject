using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 30.0f;
    public int currentWP = 0;
    GameManager gameManager;
    

    // Start is called before the first frame update
    void Start()
    {
        //finds the gameobject in the scene called GameManager and uses the script in that gameobject. This is needed for waypoints.
        //REQUIRES THE GAMEMANAGER TO BE SPECIFICALLY NAMED "GameManager" IN EVERY SCENE.
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //this adds one to the waypoint count so when the enemy reaches the waypoint, it has to go to the next waypoint.
        if (Vector3.Distance(this.transform.position, gameManager.waypoints1[currentWP].transform.position) < 3.0f)
        {
            currentWP++;
        }
        //this can be updated later, but rn when the current waypoint value is at the max, reset the waypoint count.
        if (currentWP >= gameManager.waypoints1.Length)
        {
            currentWP = 0;
        }

        //looks at the waypoint constantly and moves the enemy forward towards the waypoint.
        transform.LookAt(gameManager.waypoints1[currentWP].transform.position);
        this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}

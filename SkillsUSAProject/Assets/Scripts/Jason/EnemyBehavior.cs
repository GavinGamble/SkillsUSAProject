using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 30.0f;
    public int currentWP = 0;
    EnemySpawner enemyspawner;
    GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        //finds the gameobject in the scene called GameManager and uses the script in that gameobject. This is needed for waypoints.
        //REQUIRES THE GAMEMANAGER TO BE SPECIFICALLY NAMED "GameManager" IN EVERY SCENE.
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        enemyspawner = GetComponent<EnemySpawner>();
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
            //UPDATE : When the reach the main tower, decrease its health by 1 and destroy the enemy.
            if (currentWP >= gameManager.waypoints1.Length)
            {
                currentWP = 0;
                gameManager.currentHealth--;
                gameManager.enemiesAlive--;
                Debug.Log(gameManager.currentHealth);
                TowerHealth.instance.setValue(gameManager.currentHealth / (float)gameManager.towerHealth);
                Destroy(gameObject);
            }


            //looks at the waypoint constantly and moves the enemy forward towards the waypoint.
            transform.LookAt(gameManager.waypoints1[currentWP].transform.position);
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
    }
}

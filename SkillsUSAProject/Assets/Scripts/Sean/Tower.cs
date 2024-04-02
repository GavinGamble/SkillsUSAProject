using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign this in the inspector
    public float range = 10f; // The range within which the tower can detect and attack enemies
    public float attackRate = 1f; // How often the tower attacks per second

    private float attackCooldown = 0f;

    // Update is called once per frame
    void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (attackCooldown <= 0f)
        {
            GameObject enemy = FindClosestEnemy();
            if (enemy != null)
            {
                Attack(enemy);
                attackCooldown = 1f / attackRate;
            }
        }
    }

    void Attack(GameObject enemy)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        if (projectileScript != null)
        {
            projectileScript.Seek(enemy.transform);
        }
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, position);
            if (distance < closestDistance && distance <= range)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        return closestEnemy;
    }
}

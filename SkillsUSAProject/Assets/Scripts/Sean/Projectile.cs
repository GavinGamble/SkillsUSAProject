using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    private Transform target;
    EnemySpawner enemySpawner;
    GameManager gameManager;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // Destroy the projectile if there is no target
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // Check if we're close enough to hit the target
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Move towards the target
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        // Optionally, make the projectile face the target
        transform.LookAt(target);
    }

    void HitTarget()
    {
        Destroy(target.gameObject); // Destroy the enemy
        Destroy(gameObject); // Destroy the projectile
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemies")) // Make sure this tag matches your enemy's tag
        {
            HitTarget();
        }
    }

    private void Start()
    {
        enemySpawner = GetComponent<EnemySpawner>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}

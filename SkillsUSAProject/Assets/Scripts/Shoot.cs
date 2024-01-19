using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float TowerDamage;
    public Enemies enemies;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("OnTriggerStay", 5);
    }
    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Enemies"))
        {
            enemies.Health(-TowerDamage);
            if(enemies.CurHealth <= 0)
            {
                other.gameObject.SetActive(false);
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
        
    }
}

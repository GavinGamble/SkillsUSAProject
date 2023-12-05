using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    Rigidbody rb;
    public GameObject bullet;
    public Transform barrelBase;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
    public void Shoot()
    {
        if(Input.GetMouseButtonDown(0)) 
        { 

            Instantiate(bullet,barrelBase.position,transform.rotation);
        }
    }
}

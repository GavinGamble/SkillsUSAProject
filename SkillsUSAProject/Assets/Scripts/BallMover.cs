using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMover : MonoBehaviour
{
    private Rigidbody rb;
    public int moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.forward * moveSpeed,ForceMode.Impulse);
        }
    }
}

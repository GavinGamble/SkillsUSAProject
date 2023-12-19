using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 movementInput;
    private Vector2 mouseInput;
    private float xRotate;

    [SerializeField] private Transform Camera;
    [SerializeField] private CharacterController characterController;
    
    [SerializeField] private float sensitivity;
    [SerializeField]private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MoveCamPos();
        RotateCam();
    }
    private void MoveCamPos()
    {
        Vector3 moveVector = transform.TransformDirection(movementInput);

        if(Input.GetKey(KeyCode.Space))
        {
            velocity.y = 1.0f;
        }
        else if(Input.GetKey(KeyCode.LeftShift))
        { 
            velocity.y = -1.0f; 
        }
        characterController.Move(moveVector * Time.deltaTime * speed);
        characterController.Move(velocity * Time.deltaTime * speed);

        velocity.y = 0f;
    }
    private void RotateCam()
    {
        xRotate -= mouseInput.y * sensitivity;
        transform.Rotate(0f, mouseInput.x * sensitivity, 0f);
        Camera.transform.localRotation = Quaternion.Euler(xRotate, 0f, 0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private Rigidbody rigidbody;
   
    [SerializeField] private KeyCode moveForwardKey;
    [SerializeField] private KeyCode moveBackKey;
    [SerializeField] private KeyCode moveLeftKey;
    [SerializeField] private KeyCode moveRightKey;
    [SerializeField] private string horiozntalName;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    public bool canMove;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        if (rigidbody == null)
        {
            Debug.LogError("Can't find Rigidbody component",rigidbody);
        }
    }


    void FixedUpdate()
    {
        if (canMove)
        {
            if (Input.GetKey(moveLeftKey) || Input.GetKey(moveRightKey))
            {
                Rotate();
            }

            if (Input.GetKey(moveForwardKey) || Input.GetKey(moveBackKey))
            {
                Move();
            }
        }
    }

    void Rotate()
    { 
       var x = Input.GetAxis(horiozntalName) * rotationSpeed;
       transform.Rotate(0,x,0);
    }
    
    

    void Move()
    {
        if (Input.GetKey(moveForwardKey))
        {
      
            rigidbody.AddForce(moveSpeed * gameObject.transform.forward, ForceMode.Impulse);
        }
        else
        {
            rigidbody.AddForce(-moveSpeed/2 * gameObject.transform.forward, ForceMode.Impulse);
        }
    }
}

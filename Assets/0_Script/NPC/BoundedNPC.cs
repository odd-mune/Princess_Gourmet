using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedNPC : Interactable
{
    private Vector3 directionVector;
    private Transform myTransform;
    public float speed;
    private Rigidbody2D myRigidbody;

    void Start()
    {
        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody2D>();
        ChangeDirection();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        myRigidbody.MovePosition(myTransform.position + directionVector * speed * Time.deltaTime);
    }

    void ChangeDirection()
    {
        int direction = Random.Range(0, 4);
        switch(direction)
        {
            case 0:
            // Walking to the right
            directionVector = Vector3.right;
                break;
            case 1:
            // Walking up
            directionVector = Vector3.up;
                break;
            case 2:
            // Walking left
            directionVector = Vector3.left;
                break;
            case 3:
            // Walking down
            directionVector = Vector3.down;
                break;
            default:
                break;
        }
    }
}

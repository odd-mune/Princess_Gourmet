using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saltSnail : Animal
{
    private Rigidbody2D myRigidbody;
    public Transform target; //이건 뭐지 ?
    public float hideRadius;
    //public float attackRadius;
    public Transform homePosition;
    public Animator anim;
    private Vector3 mCurrentRoamDirection;

    void Start()
    {
        currentState = AnimalState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if(Vector3.Distance(target.position, transform.position) <= hideRadius)
        {
            if(currentState == AnimalState.idle || currentState == AnimalState.walk
                && currentState != AnimalState.hide)
            {
                Vector3 directionAwayFromTarget = Vector3.Normalize(transform.position - target.position);
                Vector3 temp = transform.position + moveSpeed * Time.fixedDeltaTime * directionAwayFromTarget;
                myRigidbody.MovePosition(temp);
                ChangeState(AnimalState.walk);
                anim.SetBool("wakeUp", true);
            }
        }
        else if(Vector3.Distance(target.position, transform.position) > hideRadius)
        {
            anim.SetBool("wakeUp", false);

            GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            if (currentState == AnimalState.walk)
            {
                Vector3 temp = transform.position + moveSpeed * Time.fixedDeltaTime * mCurrentRoamDirection;
                myRigidbody.MovePosition(temp);
            }
        }
    }

    private void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    private void changeAnim(Vector2 direction)
    {
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }
            else if(direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }

        }
        else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if(direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if(direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }
        }

    }

    private void ChangeState(AnimalState newState)
    {
        if(currentState != newState)
        {
            currentState = newState;
            if (currentState == AnimalState.walk)
            {
                mCurrentRoamDirection.x = Random.Range(0.0f, 1.0f);
                mCurrentRoamDirection.y = Random.Range(0.0f, 1.0f);
            }
        }
    }
}

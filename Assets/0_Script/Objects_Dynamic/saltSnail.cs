using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saltSnail : Animal
{
    private Rigidbody2D myRigidbody;
    public Transform target; 
    public float hideRadius;
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
        float distance = Vector3.Distance(target.position, transform.position);
        // 숨는 범위 안으로 플레이어가 들어오면
        if(distance <= hideRadius)
        {
            float moveX = anim.GetFloat("moveX");
            float moveY = anim.GetFloat("moveY");
            bool prevWakepUp = anim.GetBool("wakeUp");
            ChangeState(AnimalState.hide);
            anim.SetBool("wakeUp", false);
            Debug.Log($"{distance} <= {hideRadius} HIDE!!! ({moveX}, {moveY}) {prevWakepUp} -> {anim.GetBool("wakeUp")}");
        }
        // 범위 밖 
        else
        {
            float moveX = anim.GetFloat("moveX");
            float moveY = anim.GetFloat("moveY");
            bool prevWakepUp = anim.GetBool("wakeUp");
            ChangeState(AnimalState.idle);
            anim.SetBool("wakeUp", true);
            Debug.Log($"{distance} > {hideRadius} RUN!!! ({moveX}, {moveY}) {prevWakepUp} -> {anim.GetBool("wakeUp")}");
        }
    }


    // 걸어다니는 것 기본 설정 
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

    // 상태 설정 
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

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
    private float mRoamTimer;
    private float mCurrentRoamTimer;
    private float mCurrentWaitTimer;

    void Start()
    {
        currentState = AnimalState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        mCurrentWaitTimer = 5.0f;
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
            anim.SetBool("isMoving", false);
            mCurrentRoamTimer = 0.0f;
            mRoamTimer = 0.0f;

            float moveX = anim.GetFloat("moveX");
            float moveY = anim.GetFloat("moveY");
            bool prevWakeUp = anim.GetBool("wakeUp");
            ChangeState(AnimalState.hide);
            anim.SetBool("wakeUp", false);
        }
        // 범위 밖 
        
        mCurrentWaitTimer -= Time.fixedDeltaTime;
        if (mCurrentWaitTimer <= 0.0f)
        {
            {
                anim.SetBool("wakeUp", true);
                mCurrentWaitTimer = 5.0f;
            }

            if (distance > hideRadius)
            {
                if (mCurrentRoamTimer <= 0.0f)
                {
                    float moveX = anim.GetFloat("moveX");
                    float moveY = anim.GetFloat("moveY");
                    bool prevWakeUp = anim.GetBool("wakeUp");
                    ChangeState(AnimalState.idle);
                    anim.SetBool("isMoving", false);
                    
                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                    {
                        bool roam = Random.value > 0.5;
                        mRoamTimer = Random.Range( 3.0f, 5.0f);
                        mCurrentRoamTimer = mRoamTimer - (0.0f - mCurrentRoamTimer);

                        if (roam)
                        {
                            ChangeState(AnimalState.walk);
                            anim.SetBool("isMoving", true);
                        }
                    }
                }
                
                if (mCurrentRoamTimer > 0.0f)
                {
                    if (currentState == AnimalState.walk && anim.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
                    {
                        Vector2 moveVelocity = new Vector2(mCurrentRoamDirection.x * moveSpeed, mCurrentRoamDirection.y * moveSpeed);
                        changeAnim(moveVelocity);
                        myRigidbody.MovePosition(transform.position + new Vector3(moveVelocity.x, moveVelocity.y, 1.0f) * Time.fixedDeltaTime);
                    }

                    mCurrentRoamTimer -= Time.fixedDeltaTime;
                }
            }
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
        SetAnimFloat(direction);
        // if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        // {
        //     if(direction.x > 0)
        //     {
        //         SetAnimFloat(Vector2.right);
        //     }
        //     else if(direction.x < 0)
        //     {
        //         SetAnimFloat(Vector2.left);
        //     }

        // }
        // else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        // {
        //     if(direction.y > 0)
        //     {
        //         SetAnimFloat(Vector2.up);
        //     }
        //     else if(direction.y < 0)
        //     {
        //         SetAnimFloat(Vector2.down);
        //     }
        // }

    }

    // 상태 설정 
    private void ChangeState(AnimalState newState)
    {
        if(currentState != newState)
        {
            currentState = newState;
            if (currentState == AnimalState.walk)
            {
                mCurrentRoamDirection = Random.insideUnitCircle.normalized;
            }
        }
    }
}

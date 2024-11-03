using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimalState
{
    idle,
    walk,
    hide, 
    wakeUp,
    stop,
    preAttack,
    attack,
}

public class Animal : PhysicalInventoryItem
{
    public AnimalState currentState;
    public Transform target;
    public float targetRadius;
    public string AnimalName;
    public float moveSpeed;


    protected Rigidbody2D myRigidbody;
    public Transform homePosition;
    public Animator anim;
    protected Vector3 mCurrentRoamDirection;
    protected float mRoamTimer;
    protected float mCurrentRoamTimer;
    protected bool mIsAbleToRoam;

    protected override void onStart()
    {
        currentState = AnimalState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();
        isPickUpable = false;
    }

    void CheckDistance()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        // 숨는 범위 안으로 플레이어가 들어오면
        if (distance <= targetRadius)
        {
            onTargetInRadius();
        }
        else
        {
            onTargetNotInRadius();
        }

        onCheckDistance();

        // 플레이어가 범위에 없어
        mIsAbleToRoam = distance > targetRadius;
    }
    
    protected virtual void onTargetInRadius() { }
    protected virtual void onTargetNotInRadius() { }
    protected virtual void onCheckDistance() { }

    protected void TransitionToWalk()
    {
        bool roam = Random.value > 0.5; // 다음에 움직일지, 가만히 있을 지?
        mRoamTimer = Random.Range(3.0f, 5.0f); // 움직/가만히 있을 시간
        mCurrentRoamTimer = mRoamTimer - (0.0f - mCurrentRoamTimer);

        // 돌아다닐거면 walk로
        if (roam)
        {
            ChangeState(AnimalState.walk);
            anim.SetBool("isMoving", true);
        }
    }

    protected void UpdateRoaming()
    {
        // 만약 움직/가만히 타이머가 끝났어
        if (mCurrentRoamTimer <= 0.0f)
        {
            onRoamingEnd();
        }

        // 움직/가만히 타이머 돌아가는 중이라면
        if (mCurrentRoamTimer > 0.0f)
        {
            // 움직여
            if (mCurrentRoamDirection != Vector3.zero)
            {
                Vector2 moveVelocity = new Vector2(mCurrentRoamDirection.x * moveSpeed, mCurrentRoamDirection.y * moveSpeed);
                changeAnim(new Vector2(mCurrentRoamDirection.x, mCurrentRoamDirection.y));
                //myRigidbody.MovePosition(transform.position + new Vector3(moveVelocity.x, moveVelocity.y, 1.0f) * Time.fixedDeltaTime);
                myRigidbody.velocity = moveVelocity;
            }

            mCurrentRoamTimer -= Time.fixedDeltaTime;
        }
    }

    protected virtual void onRoamingEnd()
    {
        // 일단 멈춰
        ChangeState(AnimalState.idle);
        anim.SetBool("isMoving", false);

        // 지금 idle 상태면
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            TransitionToWalk();
        }
    }

    void FixedUpdate()
    {
        CheckDistance();

        onFixedUpdate();

        if (mIsAbleToRoam == true)
        {
            UpdateRoaming();
        }
    }

    protected virtual void onFixedUpdate() { }

    // 걸어다니는 것 기본 설정 
    protected void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    protected void changeAnim(Vector2 direction)
    {
        SetAnimFloat(direction);
    }

    // 상태 설정 
    protected void ChangeState(AnimalState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            if (currentState == AnimalState.walk)
            {
                ChangeRoamingDirection(Random.insideUnitCircle.normalized);
            }
        }
    }

    protected void ChangeRoamingDirection(Vector2 direction)
    {
        mCurrentRoamDirection = direction;
        if (mCurrentRoamDirection.x < 0)
        {
            transform.localScale = new Vector2(1.0f, 1.0f);
        }
        else
        {
            transform.localScale = new Vector2(-1.0f, 1.0f);
        }
    }
}

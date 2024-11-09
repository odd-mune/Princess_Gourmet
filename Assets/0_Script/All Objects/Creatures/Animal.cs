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
    dying,
    death,
    stagger,
}

public class Animal : PhysicalInventoryItem
{
    public AnimalState currentState;
    public Transform target;
    public float targetRadius;
    public string AnimalName;
    public float moveSpeed;
    public float health;
    private float mCurrentHealth;


    protected Rigidbody2D myRigidbody;
    public Transform homePosition;
    public Animator anim;
    protected Vector3 mCurrentRoamDirection;
    protected float mRoamTimer;
    protected float mCurrentRoamTimer;
    protected bool mIsAbleToRoam;

    [Tooltip("피격 시 넉백 시간")]
    public float KnockbackTime;
    private bool mIsKnockingBack;

    protected override void onStart()
    {
        currentState = AnimalState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();
        isPickUpable = false;
        mCurrentHealth = health;
        mIsKnockingBack = false;
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
            if ((currentState == AnimalState.walk || currentState == AnimalState.attack) && mCurrentRoamDirection != Vector3.zero)
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
        if (GameStateManager.GetState() != GameState.IDLE)
        {
            anim.enabled = false;
            return;
        }
        anim.enabled = true;

        if (mIsKnockingBack == false)
        {
            myRigidbody.velocity = Vector2.zero;
        }

        if (currentState != AnimalState.death && currentState != AnimalState.dying)
        {
            CheckDistance();

            onFixedUpdate();

            if (mIsAbleToRoam == true)
            {
                UpdateRoaming();
            }
        }

        if (currentState == AnimalState.death)
        {
            Color prevColor = GetComponent<SpriteRenderer>().color;
            float prevAlpha = prevColor.a;
            prevColor.a = prevAlpha - Time.fixedDeltaTime; 
            GetComponent<SpriteRenderer>().color = prevColor;
            if (prevColor.a <= 0.0f)
            {
                gameObject.SetActive(false);
            }
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
            if (currentState == AnimalState.idle)
            {
                myRigidbody.velocity = Vector2.zero;
            }
            else  if (currentState == AnimalState.walk)
            {
                ChangeRoamingDirection(Random.insideUnitCircle.normalized);
            }
            else if (currentState == AnimalState.dying)
            {
                myRigidbody.velocity = Vector2.zero;
                GetComponent<BoxCollider2D>().enabled = false;
            }
            else if (currentState == AnimalState.stagger)
            {
                anim.SetBool("onHit", true);
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

    public void OnHit(float damage)
    {
        if (mCurrentHealth > 0.0f && currentState != AnimalState.dying && currentState != AnimalState.stagger)
        {
            mCurrentHealth -= damage;
            if (mCurrentHealth <= 0.0f)
            {
                OnDying();
            }
            else
            {
                Knock(KnockbackTime);
            }
        }
    }

    public void Knock(float knockTime)
    {
        mIsKnockingBack = true;
        StartCoroutine(KnockCo(knockTime));
    }

    protected virtual void postKnock(AnimalState prevState) { }

    private IEnumerator KnockCo(float knockTime)
    {
        if (myRigidbody != null && currentState != AnimalState.dying && currentState != AnimalState.death)
        {
            AnimalState prevState = currentState;
            ChangeState(AnimalState.stagger);
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            ChangeState(prevState);
            postKnock(prevState);
            //넉백을 받으면 공주가 자꾸 가만히 멈춰서서 idle 에서 walk로 고쳐봤음
            myRigidbody.velocity = Vector2.zero;
            mIsKnockingBack = false;
            anim.SetBool("onHit", false);
        }
    }

    public void OnDying()
    {
        onDying();

        ChangeState(AnimalState.dying);
        anim.SetBool("isMoving", false);
        anim.SetBool("isDying", true);
    }

    protected virtual void onDying() { }

    public void OnDeath()
    {
        ChangeState(AnimalState.death);
    }
}

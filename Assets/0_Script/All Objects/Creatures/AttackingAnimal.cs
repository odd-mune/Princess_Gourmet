using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingAnimal : Animal
{
    [Tooltip("플레이어 넉백 시간")]
    public float KnockbackTime;
    [Tooltip("공격력")]
    public float Damage;
    [Tooltip("따라가는 시간")]
    public float FollowTimer;

    [Tooltip("공격 준비 시간")]
    public float AttackPreparationTimer;
    private float mCurrentAttackPreparationTimer;

    [Tooltip("공격 간 쿨타임")]
    public float AttackCooltime;
    private float mCurrentAttackCooltime = 0.0f;

    [Tooltip("공격 시 이동속도 배율")]
    public float AttackingSpeedFactor;
    private float mDefaultSpeed;
    private float mDefaultMoveSpeed;

    protected override void onStart()
    {
        base.onStart();

        mDefaultSpeed = anim.speed;
        mDefaultMoveSpeed = moveSpeed;
    }

    protected override void onTargetInRadius()
    {
        if (mCurrentAttackCooltime <= 0.0f)
        {
            bool prepareForAttack = false;

            switch (currentState)
            {
                case AnimalState.idle:
                    prepareForAttack = true;
                    break;
                case AnimalState.walk:
                    {
                        anim.SetBool("isMoving", false);
                        myRigidbody.velocity = Vector2.zero;
                        prepareForAttack = true;
                    }
                    break;
                case AnimalState.preAttack:
                    break;
                case AnimalState.attack:
                    mCurrentRoamTimer = FollowTimer;
                    break;
                case AnimalState.hide:
                // intentional fallthrough
                case AnimalState.stop:
                // intentional fallthrough
                case AnimalState.wakeUp:
                // intentional fallthrough
                default:
                    Debug.LogError($"Invalid animal state {currentState}");
                    Debug.Break();
                    break;
            }

            if (prepareForAttack)
            {
                // 움직이기 타이머 X
                mCurrentRoamTimer = 0.0f;
                mRoamTimer = 0.0f;

                // Attack state로 transition 하도록
                ChangeState(AnimalState.preAttack);
                anim.SetBool("preAttack", true);

                mCurrentAttackPreparationTimer = AttackPreparationTimer;

                mIsAbleToRoam = false;
            }
        }
    }

    protected override void onTargetNotInRadius()
    {
        if (currentState == AnimalState.preAttack)
        {
            ChangeState(AnimalState.idle);
            anim.SetBool("attack", false);
            anim.SetBool("preAttack", false);
            mCurrentRoamDirection = Vector3.zero;
            anim.speed = mDefaultSpeed;
            moveSpeed = mDefaultMoveSpeed;

            mCurrentAttackPreparationTimer = 0.0f;
        }
    }

    protected override void onFixedUpdate() 
    {
        if (currentState == AnimalState.preAttack)
        {
            mIsAbleToRoam = false;
            mCurrentAttackPreparationTimer -= Time.fixedDeltaTime;
            ChangeRoamingDirection((target.position - transform.position).normalized);
            if (mCurrentAttackPreparationTimer <= 0.0f)
            {
                mCurrentAttackPreparationTimer = 0.0f;
                OnAttackPreparationEnd();
            }
        }

        if (currentState == AnimalState.attack)
        {
            ChangeRoamingDirection((target.position - transform.position).normalized);
            mIsAbleToRoam = true;
        }

        if (mCurrentAttackCooltime > 0.0f)
        {
            mCurrentAttackCooltime -= Time.fixedDeltaTime;
        }
    }

    protected override void onRoamingEnd()
    {
        if (currentState == AnimalState.attack)
        {
            mCurrentAttackCooltime = AttackCooltime;
        }

        mCurrentRoamTimer = 0.0f;
        mCurrentRoamDirection = Vector3.zero;
        base.onRoamingEnd();

        anim.SetBool("attack", false);
        anim.speed = mDefaultSpeed;
        moveSpeed = mDefaultMoveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform == target)
        {
            PlayerManager playerManager = target.GetComponent<PlayerManager>();
            playerManager.Knock(KnockbackTime, Damage);

            onRoamingEnd();
        }
    }

    public void OnAttackPreparationEnd()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= targetRadius)
        {
            ChangeState(AnimalState.attack);
            mCurrentRoamTimer = FollowTimer;
            anim.SetBool("attack", true);
            anim.SetBool("preAttack", false);
            anim.speed = mDefaultSpeed * AttackingSpeedFactor;
            moveSpeed *= AttackingSpeedFactor;
        }
        else
        {
            onTargetNotInRadius();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class saltSnail : PickUpableAnimal
{
    //private Rigidbody2D myRigidbody;
    //public Transform target; 
    //public float hideRadius;
    //public Transform homePosition;
    //public Animator anim;
    //private Vector3 mCurrentRoamDirection;
    //private float mRoamTimer;
    //private float mCurrentRoamTimer;
    private float mCurrentWaitTimer;
    private bool mIsHidden;

    private FlickeringLight mFlickeringLight;

    protected override void onStart()
    {
        base.onStart();

        mCurrentWaitTimer = 5.0f;
        mIsHidden = true;

        mFlickeringLight = GetComponentInChildren<FlickeringLight>();
    }

    protected override void onRegenerateTimerOff()
    {
        isPickUpable = mIsHidden;
        anim.SetBool("isPickUpable", true);
    }

    protected override void onFixedUpdate()
    {
        base.onFixedUpdate();

        if (mCheckToday.IsDay() == true)
        {
            mFlickeringLight.lightToControl.intensity = 0.0f;
        }
        else
        {
            if (anim.GetBool("isPickUpable") == true)
            {
                mFlickeringLight.UpdateIntensity();
                mFlickeringLight.UpdateRadius();
            }
            else
            {
                mFlickeringLight.lightToControl.intensity = 0.0f;
            }
        }
    }

    protected override void onTargetInRadius()
    {
        // 움직임 X]
        anim.SetBool("isMoving", false);
        if (currentState != AnimalState.hide)
        {
            myRigidbody.velocity = Vector2.zero;
        }

        // 움직이기 타이머 X
        mCurrentRoamTimer = 0.0f;
        mRoamTimer = 0.0f;

        // Hiding state로 transition 하도록
        ChangeState(AnimalState.hide);
        anim.SetBool("wakeUp", false);
    }

    protected override void onCheckDistance()
    {
        // Pick up 가능 여부 확인 - Hidden state인지?
        mIsHidden = anim.GetCurrentAnimatorStateInfo(0).IsName("Hidden") || anim.GetCurrentAnimatorStateInfo(0).IsName("NsaltSnail_Stop");

        // Hidden 타이머
        mCurrentWaitTimer -= Time.fixedDeltaTime;

        // Hidden 타이머 끝나면
        //  아직 플레이어가 범위에 있으면 숨고
        //  플레이어 범위에 없으면 idle하든, 움직이든!
        if (mCurrentWaitTimer <= 0.0f)
        {
            // 일단 일어나고 timer 5초로 초기화
            anim.SetBool("wakeUp", true);
            mCurrentWaitTimer = 5.0f;
        }
    }
    protected override bool checkIfPickUpable()
    {
        return mIsHidden == true && isPickUpable == true;
    }
}

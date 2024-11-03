using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class saltSnail : Animal
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
    public float saltRegenerateTimer = 5.0f;
    private float mCurrentSaltRegenerateTimer;
    private bool mIsHidden;

    private FlickeringLight mFlickeringLight;

    protected override void onStart()
    {
        base.onStart();

        //currentState = AnimalState.idle;
        //myRigidbody = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        //target = GameObject.FindWithTag("Player").transform;
        mCurrentWaitTimer = 5.0f;
        mCurrentSaltRegenerateTimer = saltRegenerateTimer;
        isPickUpable = false;
        mIsHidden = true;
        anim.SetBool("isPickUpable", isPickUpable);

        mFlickeringLight = GetComponentInChildren<FlickeringLight>();
    }

    protected override void onFixedUpdate()
    {
        if (mCurrentSaltRegenerateTimer > 0.0f)
        {
            mCurrentSaltRegenerateTimer -= Time.deltaTime;
        }
        else
        {
            isPickUpable = mIsHidden;
            anim.SetBool("isPickUpable", true);
        }

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

    public override bool PickUp()
    {
        if (mIsHidden == true && isPickUpable == true)
        {
            isPickUpable = false;
            anim.SetBool("isPickUpable", false);
            mCurrentSaltRegenerateTimer = saltRegenerateTimer;
            return base.PickUp();  // 인벤토리에 넣기
        }

        return false;
    }
}

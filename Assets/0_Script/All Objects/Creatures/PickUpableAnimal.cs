using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpableAnimal : Animal
{
    public float regenerateTimer = 5.0f;
    private float mCurrentRegenerateTimer;

    protected override void onStart()
    {
        base.onStart();

        mCurrentRegenerateTimer = regenerateTimer;
        isPickUpable = false;
        anim.SetBool("isPickUpable", isPickUpable);
    }

    protected override void onFixedUpdate()
    {
        if (mCurrentRegenerateTimer > 0.0f)
        {
            mCurrentRegenerateTimer -= Time.deltaTime;
        }
        else
        {
            onRegenerateTimerOff();
        }
    }

    protected virtual void onRegenerateTimerOff()
    {
        isPickUpable = true;
        anim.SetBool("isPickUpable", true);
    }

    protected virtual bool checkIfPickUpable()
    {
        return isPickUpable;
    }

    public override bool PickUp()
    {
        if (checkIfPickUpable() == true)
        {
            isPickUpable = false;
            anim.SetBool("isPickUpable", false);
            mCurrentRegenerateTimer = regenerateTimer;
            return base.PickUp();  // 인벤토리에 넣기
        }

        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growable : PhysicalInventoryItem
{
    [System.Serializable]
    public struct StageInfo
    {
        public Sprite sprite;
        public float secondsInThisStage;
        public bool isPickUpable;
        public bool runAnimation;
    };
    
    private float seconds;
    private float mElapsedSeconds;
    public List<StageInfo> stages;
    private int mCurrentStageIndex;
 
    // Start is called before the first frame update
    void Start()
    {
        seconds = 0;
        mCurrentStageIndex = 0;
        setStage(mCurrentStageIndex);
    }
 
    // Update is called once per frame
    void FixedUpdate()
    {
        if (mElapsedSeconds < CheckToday.elapsedSeconds)
        {
            seconds += CheckToday.elapsedSeconds - mElapsedSeconds;
            mElapsedSeconds = CheckToday.elapsedSeconds;
            growing_up();
        }
    }

    public override void PickUp()
    {
        if (mIsPickUpable == true)
        {
            base.PickUp();
            seconds = 0.0f;
            mCurrentStageIndex = 0;
            setStage(mCurrentStageIndex);
        }
    }
 
    void growing_up()
    {
        StageInfo currentStageInfo = stages[mCurrentStageIndex];
        if (currentStageInfo.isPickUpable == false && currentStageInfo.secondsInThisStage < seconds)
        {
            seconds -= currentStageInfo.secondsInThisStage;
            mCurrentStageIndex = (mCurrentStageIndex + 1) % stages.Count;
            setStage(mCurrentStageIndex);
        }
    }

    private void setStage(int stageIndex)
    {
        StageInfo newStageInfo = stages[stageIndex];
        GetComponent<SpriteRenderer>().sprite = newStageInfo.sprite;
        mIsPickUpable = newStageInfo.isPickUpable;

        Animator animatorOrNull = GetComponent<Animator>();
        if (animatorOrNull != null)
        {
            animatorOrNull.enabled = newStageInfo.runAnimation;
            Debug.Log($"{animatorOrNull.enabled}");
        }
    }
}
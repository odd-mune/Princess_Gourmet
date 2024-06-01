using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGrowable : PhysicalInventoryItem 
{
    //growth cycle (cooltime)
    public int numStages;
    public List<int> secondsPerStage;
    public List<string> spriteNamePerStage;
    private int currentStageIndex;
    public int checkToday;
    
    // Update is called once per frame
    void Update()
    {
        today = checkToday.Today;
        IGrowable(today);
    }

    private void //시간에 따른 무언가 
    {

    }

}

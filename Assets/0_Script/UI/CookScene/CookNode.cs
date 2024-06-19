using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeadowGames.UINodeConnect4;

public class CookNode : Node
{
    public CookManager cookManager;

    public void OnEnable()
    {
        UICSystemManager.UICEvents.StartListening(UICEventType.OnPointerDown, OnPointerDown);
    }

    public void OnDisable()
    {
        UICSystemManager.UICEvents.StopListening(UICEventType.OnPointerDown, OnPointerDown);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(IElement element)
    {
        if (element == this)
        {
            base.OnPointerDown();

            cookManager.Solve();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseCookManager : IPauseManager
{
    public CraftingManager craftingManager;

    //Start is called before the first frame update
    void Start()
    {
        ButtonName = "cook";
        craftingManager.enabled = false;
    }

    protected override void onChange(bool isActive)
    {
        if (isActive)
        {
            craftingManager.enabled = true;
        }
        else
        {
            craftingManager.OnClose(false);
            craftingManager.enabled = false;
        }
    }
}

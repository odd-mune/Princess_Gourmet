using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseCookManager : IPauseManager
{
    public CraftingManager craftingManager;
    [Tooltip("공주 요리 씬")]
    public CookPanel2 CookingScene;
    [Tooltip("요리 버튼")]
    public Button CookButton;

    //Start is called before the first frame update
    void Start()
    {
        ButtonName = "cook";
        craftingManager.enabled = false;
        CookButton.onClick.AddListener(craftingManager.OnCook);
    }

    protected override void onChange(bool isActive)
    {
        if (isActive)
        {
            craftingManager.enabled = true;
        }
        else
        {
            craftingManager.OnClose(false, true);
            craftingManager.enabled = false;
        }
    }
}

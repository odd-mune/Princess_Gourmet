using MeadowGames.UINodeConnect4;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseCookManager : IPauseManager
{
    public CookManager cookManager;
    public UICSystemManager uicSystemManager;

    //Start is called before the first frame update
    void Start()
    {
        ButtonName = "cook";
    }
}
